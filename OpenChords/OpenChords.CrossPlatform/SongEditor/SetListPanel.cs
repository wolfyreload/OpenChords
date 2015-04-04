using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SetListPanel : Panel
    {
        protected ListBox lbSongs = new ListBox();
        protected ComboBox cmbSets = new ComboBox();

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
            cmbSets.AutoComplete = true;
            CurrentSet = new Set();
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
            loadSongsInSet();
        }

        private void loadSongsInSet()
        {
            if (cmbSets.SelectedIndex < 0) return;
            var sets = Set.listOfAllSets();
            var selectedSet = sets[cmbSets.SelectedIndex];
            CurrentSet = Set.loadSet(selectedSet);
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
            new frmPresent(CurrentSet, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings)).Show();
        }

        private void loadSetState()
        {
            string saveState = IO.SettingsReaderWriter.readSessionState().Trim();
            cmbSets.SelectedValue = saveState;
            if (cmbSets.SelectedIndex > 0)
                loadSongsInSet();
        }

        public void saveSetState()
        {
            if (cmbSets.SelectedIndex >= 0)
                IO.SettingsReaderWriter.writeSessionState(cmbSets.SelectedValue.ToString());
            
        }

        internal void ExportToHtml()
        {
            string html = CurrentSet.getHtml(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
            string fileName = string.Format("{0}/{1}.html", Settings.ExtAppsAndDir.printFolder, CurrentSet.setName);
            File.WriteAllText(fileName, html);
        }
    }
}
