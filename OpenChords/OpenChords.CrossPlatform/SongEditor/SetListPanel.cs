using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Diagnostics;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SetListPanel : Panel
    {
        protected ListBox lbSongs = new ListBox();
        protected ComboBox cmbSets = new ComboBox();

        private bool SetChanged;
        private Set CurrentSet;
        public event EventHandler<Song> SongChanged;

        public SetListPanel()
        {
            Content = new TableLayout()
            {
                Rows = 
                    {   cmbSets,
                        lbSongs,
                        
                    }
            };

            var commandDeleteFromSet = new Command() { MenuText = "delete song from set", Shortcut = Application.Instance.CommonModifier | Keys.Delete };
            commandDeleteFromSet.Executed += (s, e) => deleteSongFromSet();
            var commandMoveSongUp = new Command() { MenuText = "move song up", Shortcut = Application.Instance.CommonModifier | Keys.Up };
            commandMoveSongUp.Executed += (s, e) => moveSongUp();
            var commandMoveSongDown = new Command() { MenuText = "move song down", Shortcut = Application.Instance.CommonModifier | Keys.Down };
            commandMoveSongDown.Executed += (s, e) => moveSongDown();

            var menu = new ContextMenu()
            {
                Items = { commandMoveSongUp, commandDeleteFromSet, commandMoveSongDown }
            };
            lbSongs.ContextMenu = menu;


            cmbSets.AutoComplete = true;


            lbSongs.KeyUp += lbSongs_KeyUp;
            CurrentSet = new Set();
        }

        void lbSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Key == Keys.Delete)
                deleteSongFromSet();
            else if (e.Control && e.Key == Keys.Up)
                moveSongUp();
            else if (e.Control && e.Key == Keys.Down)
                moveSongDown();
            e.Handled = true;
        }


        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedIndex < 0) return;
            var songName = lbSongs.SelectedValue.ToString();
            var song = Song.loadSong(songName);
            if (SongChanged != null)
                SongChanged(this, song);
        }

        void cmbSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SetChanged && showConfirmation("Save changes to set '{0}' before switching to a new set?"))
                saveSet();

            loadSongsInSet();
        }

        private void loadSongsInSet()
        {
            if (cmbSets.SelectedIndex < 0) return;
            var sets = Set.listOfAllSets();
            var selectedSet = sets[cmbSets.SelectedIndex];
            CurrentSet = Set.loadSet(selectedSet);
            refreshSongList();
            SetChanged = false;
        }

        private void refreshSongList()
        {
            var songs = CurrentSet.songList;
            ListItemCollection items = new ListItemCollection();
            foreach (var song in songs)
            {
                items.Add(song.title);
            }
            lbSongs.DataStore = items;
        }

        public void refreshPanel()
        {
            cmbSets.SelectedIndexChanged -= cmbSets_SelectedIndexChanged;
            lbSongs.SelectedIndexChanged -= lbSongs_SelectedIndexChanged;

            cmbSets.DataStore = Set.listOfAllSets();
            loadSetState();

            cmbSets.SelectedIndexChanged += cmbSets_SelectedIndexChanged;
            lbSongs.SelectedIndexChanged += lbSongs_SelectedIndexChanged;
        }


        internal void PresentSet()
        {
            if (SetChanged && showConfirmation("Save changes to set '{0}' before presenting?"))
                saveSet();
            else
                revertSet();
            new frmPresent(CurrentSet, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings)).Show();
        }

        private void loadSetState()
        {
            string saveState = IO.SettingsReaderWriter.readSessionState().Trim();
            cmbSets.SelectedValue = saveState;
            if (cmbSets.SelectedIndex >= 0)
                loadSongsInSet();
        }

        public void saveSetState()
        {
            if (cmbSets.SelectedIndex >= 0)
                IO.SettingsReaderWriter.writeSessionState(cmbSets.SelectedValue.ToString());

        }

        internal void ExportToHtml()
        {
            if (SetChanged && showConfirmation("Save changes to set '{0}' before Exporting to html?"))
                saveSet();
            else
                revertSet();
            string html = CurrentSet.getHtml(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
            string fileName = string.Format("{0}/{1}.html", Settings.ExtAppsAndDir.printFolder, CurrentSet.setName);
            File.WriteAllText(fileName, html);
        }

        internal void AddSongToSet(Song e)
        {
            CurrentSet.addSongToSet(e);
            this.refreshSongList();
            SetChanged = true;
        }

        private void deleteSongFromSet()
        {
            if (lbSongs.SelectedIndex < 0) return;
            CurrentSet.removeSongFromSet(lbSongs.SelectedIndex);
            this.refreshSongList();
            lbSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            SetChanged = true;
        }

        private void moveSongUp()
        {
            if (lbSongs.SelectedIndex < 0) return;
            CurrentSet.indexOfCurrentSong = lbSongs.SelectedIndex;
            CurrentSet.moveSongUp();
            this.refreshSongList();
            lbSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            SetChanged = true;
        }

        private void moveSongDown()
        {
            if (lbSongs.SelectedIndex < 0) return;
            CurrentSet.indexOfCurrentSong = lbSongs.SelectedIndex;
            CurrentSet.moveSongDown();
            this.refreshSongList();
            lbSongs.SelectedIndex = CurrentSet.indexOfCurrentSong;
            SetChanged = true;
        }

        internal void saveSet()
        {
            if (cmbSets.SelectedIndex > 0 && SetChanged)
            {
                CurrentSet.saveSet();
                SetChanged = false;
                MessageBox.Show("Set saved", "", MessageBoxType.Information);
            }
        }

        internal void revertSet()
        {
            if (SetChanged)
            {
                CurrentSet.revertSet();
                this.refreshSongList();
                SetChanged = false;
                MessageBox.Show("Set reverted", "", MessageBoxType.Information);
            }
        }

        private bool showConfirmation(string message = "Are you sure?")
        {
            message = message.Replace("{0}", CurrentSet.setName);
            DialogResult result = MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxType.Question);
            return result == DialogResult.Yes;
        }

        internal void ExportToHtml(DisplayAndPrintSettings settings)
        {
            string destination = CurrentSet.ExportToHtml(settings);
            Process.Start(destination);
        }

        internal void saveSetBeforeClosing()
        {
            if (SetChanged && showConfirmation("Save changes to set '{0}' before closing OpenChords?"))
                saveSet();
        }
    }
}
