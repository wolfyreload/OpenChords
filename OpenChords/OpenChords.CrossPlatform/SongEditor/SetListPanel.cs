using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Diagnostics;
using System.Collections.Generic;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SetListPanel : Panel
    {
        protected ListBox lbSongs = new ListBox();
        protected ComboBox cmbSets = new ComboBox();

        private bool SetChanged;
        public Set CurrentSet { get; private set; }
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

            var commandDeleteFromSet = new Command() { MenuText = "Delete song from set", Shortcut = Application.Instance.CommonModifier | Keys.Delete, Image = Graphics.ImageDelete };
            commandDeleteFromSet.Executed += (s, e) => deleteSongFromSet();
            var commandMoveSongUp = new Command() { MenuText = "Move song up", Shortcut = Application.Instance.CommonModifier | Keys.Up, Image = Graphics.ImagMoveUp };
            commandMoveSongUp.Executed += (s, e) => moveSongUp();
            var commandMoveSongDown = new Command() { MenuText = "Move song down", Shortcut = Application.Instance.CommonModifier | Keys.Down, Image = Graphics.ImageMoveDown };
            commandMoveSongDown.Executed += (s, e) => moveSongDown();

            var menu = new ContextMenu()
            {
                Items = { commandMoveSongUp, commandDeleteFromSet, commandMoveSongDown }
            };
            lbSongs.ContextMenu = menu;


            cmbSets.ReadOnly = true;


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
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage("Save changes to set '{0}' before switching to a new set?", CurrentSet.setName))
                saveSet();

            loadSongsInSet();
        }

        private void loadSongsInSet()
        {
            if (cmbSets.SelectedIndex < 0) return;
            var sets = Set.listOfAllSets();
            var selectedSet = sets[cmbSets.SelectedIndex];
            CurrentSet = Set.loadSet(selectedSet);
            Functions.WebServer.CurrentSet = CurrentSet;
            refreshSongList();
            SetChanged = false;
        }

        private void refreshSongList()
        {
            var songs = CurrentSet.songList;
            ListItemCollection items = new ListItemCollection();
            foreach (var song in songs)
            {
                items.Add(song.SongFileName);
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
            if (string.IsNullOrEmpty(CurrentSet.setName))
            {
                Helpers.PopupMessages.ShowErrorMessage("Please select a set first");
                return;
            }

            if (CurrentSet.getSongSetSize() == 0)
            {
                Helpers.PopupMessages.ShowErrorMessage("Please add one or more songs to the set");
                return;
            }


            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage("Save changes to set '{0}' before presenting?", CurrentSet.setName))
                saveSet();
            else
                revertSet();
            CurrentSet.reloadSet();
            new frmPresent(CurrentSet, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings)).Show();
        }

        private void loadSetState()
        {
            string saveState = IO.SettingsReaderWriter.readSessionState().Trim();
            cmbSets.SelectedValue = saveState;
            if (cmbSets.SelectedIndex >= 0)
                loadSongsInSet();
        }

        internal void changeToSet(Set set)
        {
            cmbSets.SelectedValue = set.setName;
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
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage("Save changes to set '{0}' before Exporting to html?", CurrentSet.setName))
                saveSet();
            else
                revertSet();
            string html = CurrentSet.getHtml(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
            string fileName = string.Format("{0}/{1}.html", Settings.ExtAppsAndDir.printFolder, CurrentSet.setName);
            File.WriteAllText(fileName, html);
        }

        internal void AddSongToSet(Song e)
        {
            if (string.IsNullOrEmpty(CurrentSet.setName))
            {
                Helpers.PopupMessages.ShowErrorMessage("Please select a set first");
                return;
            }
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
            if (cmbSets.SelectedIndex >= 0 && SetChanged)
            {
                CurrentSet.saveSet();
                SetChanged = false;
                Helpers.PopupMessages.ShowInformationMessage("Set '{0}' saved", CurrentSet.setName);
            }
        }

        internal void revertSet()
        {
            if (SetChanged)
            {
                CurrentSet.revertSet();
                this.refreshSongList();
                SetChanged = false;
                Helpers.PopupMessages.ShowInformationMessage("Set '{0}' reverted", CurrentSet.setName);
            }
        }

        internal void ExportToHtml(DisplayAndPrintSettings settings)
        {
            string destination = CurrentSet.ExportToHtml(settings);
            Process.Start(destination);
        }

        internal void saveSetBeforeClosing()
        {
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage("Save changes to set '{0}' before closing OpenChords?", CurrentSet.setName))
                saveSet();
        }

        internal void ExportToOpenSong()
        {
            if (!Settings.ExtAppsAndDir.IsOpenSongDataFolderConfigured)
            {
                Helpers.PopupMessages.ShowErrorMessage("Opensong data folder is not configured");
                return;
            }

            //export the results
            CurrentSet.exportSetAndSongsToOpenSong();
            //open opensong set folder
            Process.Start(Settings.ExtAppsAndDir.openSongSetFolder);

            //run opensong
            if (Settings.ExtAppsAndDir.IsOpenSongExecutableConfigured)
                Process.Start(Settings.ExtAppsAndDir.openSongApp);      
        }



        internal void deleteSet()
        {
            if (string.IsNullOrEmpty(CurrentSet.setName))
            {
                Helpers.PopupMessages.ShowErrorMessage("Please select a set first");
                return;
            }

            if (Helpers.PopupMessages.ShowConfirmationMessage("Delete set '{0}'?", CurrentSet.setName))
            {
                Set.DeleteSet(CurrentSet);
                refreshPanel();
                lbSongs.DataStore = new List<string>();
            }
        }

        internal void newSet()
        {
            var formNewSet = new frmNewSet();
            formNewSet.ShowModal();
            if (formNewSet.SetName != "")
            {
                Set newSet = Set.NewSet(formNewSet.SetName);
                refreshPanel();
                changeToSet(newSet);
            }
        }

        public void showSetInExplorer()
        {
            var songPath = CurrentSet.getFullPath();
            showInExplorer(songPath);

        }

        private void showInExplorer(string songPath)
        {
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;

            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start("Explorer", "/select, " + songPath);
            else
                System.Diagnostics.Process.Start(fileManager, OpenChords.Settings.ExtAppsAndDir.songsFolder);
        }

        internal void ExportToTextFile()
        {
            //get the filemanager for the filesystem
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;
            string filename = null;

            //get the filename
            filename = Export.ExportToTextFile.exportSetSongList(CurrentSet);
            System.Diagnostics.Process.Start(filename);
        }
    }
}
