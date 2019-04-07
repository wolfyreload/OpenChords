using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Diagnostics;
using System.Collections.Generic;
using OpenChords.CrossPlatform.Helpers;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SetListPanel : Panel
    {
        protected ListBox lbSongs = new ListBox();
        protected ComboBox cmbSets = new ComboBox();
        protected GroupBox gbSets;

        private bool SetChanged;
        public Set CurrentSet { get; private set; }
        public event EventHandler<Song> SongChanged;

        private ShortcutSettings shortcutKeys;


        public SetListPanel()
        {
            shortcutKeys = Entities.ShortcutSettings.LoadSettings();

            Content = gbSets = new GroupBox()
            {
                Text = "Sets",
                Content = new TableLayout()
                {
                    Rows =
                    {   cmbSets,
                        lbSongs,
                    }
                }
            };

            var commandDeleteFromSet = MenuHelper.GetCommand("Delete song from set", Graphics.ImageDelete, shortcutKeys.DeleteSong);
            commandDeleteFromSet.Executed += (s, e) => deleteSongFromSet();
            var commandMoveSongUp = MenuHelper.GetCommand("Move song up", Graphics.ImagMoveUp, shortcutKeys.MoveSongUpInSet);
            commandMoveSongUp.Executed += (s, e) => moveSongUp();
            var commandMoveSongDown = MenuHelper.GetCommand("Move song down", Graphics.ImageMoveDown, shortcutKeys.MoveSongDownInSet);
            commandMoveSongDown.Executed += (s, e) => moveSongDown();
            var commandSelectRandomSong = MenuHelper.GetCommand("Select random song", Graphics.ImageRandom, shortcutKeys.SelectRandomSong);
            commandSelectRandomSong.Executed += (s, e) => selectRandomSong();

            var menu = new ContextMenu()
            {
                Items = { commandMoveSongUp, commandDeleteFromSet, commandMoveSongDown, commandSelectRandomSong}
            };
        
            lbSongs.ContextMenu = menu;


            cmbSets.ReadOnly = true;


            lbSongs.KeyUp += lbSongs_KeyUp;
            CurrentSet = new Set();

            cmbSets.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
            lbSongs.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
        }

        private void selectRandomSong()
        {
            var rand = new Random();
            var selectedIndex = rand.Next(lbSongs.Items.Count);
            lbSongs.SelectedIndex = selectedIndex;
        }

        void lbSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (MenuHelper.CompareShortcuts(e, shortcutKeys.DeleteSong))
            {
                deleteSongFromSet();
                e.Handled = true;
            }
            else if (MenuHelper.CompareShortcuts(e, shortcutKeys.MoveSongUpInSet))
            {
                moveSongUp();
                e.Handled = true;
            }
            else if (MenuHelper.CompareShortcuts(e, shortcutKeys.MoveSongDownInSet))
            {
                moveSongDown();
                e.Handled = true;
            }
            else if (MenuHelper.CompareShortcuts(e, shortcutKeys.SelectRandomSong))
            {
                selectRandomSong();
                e.Handled = true;
            }
            
        }


        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedIndex < 0) return;
            CurrentSet.indexOfCurrentSong = lbSongs.SelectedIndex;
            Song song = getSelectedSong();
            OpenChords.Functions.WebServer.CurrentSong = song;
            if (SongChanged != null)
                SongChanged(this, song);
        }

        private Song getSelectedSong()
        {
            Song song = CurrentSet.songList[CurrentSet.indexOfCurrentSong];
            return song;
        }

        void cmbSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to set '{0}' before switching to a new set?", CurrentSet.setName))
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

        public void refreshSongList()
        {
            var songs = CurrentSet.songList;
            ListItemCollection items = new ListItemCollection();
            foreach (var song in songs)
            {
                items.Add(song.SongTitleIncludingSubFolder);
            }
            
            //update the number of songs in set
            if (items.Count > 0)
                gbSets.Text = string.Format("Sets ({0})", items.Count);
            else
                gbSets.Text = "Sets";

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
                Helpers.PopupMessages.ShowErrorMessage(this, "Please select a set first");
                return;
            }

            if (CurrentSet.getSongSetSize() == 0)
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Please add one or more songs to the set");
                return;
            }


            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to set '{0}' before presenting?", CurrentSet.setName))
                saveSet();
            else
                revertSet();
            CurrentSet.reloadSet();
            var frmPresent = new frmPresent(CurrentSet, DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings), lbSongs.SelectedIndex);
            saveCurrentSongInSet();
            frmPresent.Show();
        }

        private void saveCurrentSongInSet()
        {
            if (lbSongs.SelectedIndex < 0) return;
            Song song = getSelectedSong();
            if (SongChanged != null)
            {
                SongChanged(this, song);
            }
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
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to set '{0}' before Exporting to html?", CurrentSet.setName))
                saveSet();
            else
                revertSet();
            string html = CurrentSet.getHtml(DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings));
            string fileName = string.Format("{0}/{1}.html", Settings.GlobalApplicationSettings.PrintFolder, CurrentSet.setName);
            File.WriteAllText(fileName, html);
        }

        internal void AddSongToSet(Song e)
        {
            if (string.IsNullOrEmpty(CurrentSet.setName))
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Please select a set first");
                return;
            }
            int positionToAddSong = CurrentSet.indexOfCurrentSong + 1;
            CurrentSet.addSongToSet(positionToAddSong, e);
            this.refreshSongList();
            SetChanged = true;
        }

        private void deleteSongFromSet()
        {
            if (lbSongs.SelectedIndex < 0) return;
            var oldIndex = lbSongs.SelectedIndex;
            CurrentSet.removeSongFromSet(lbSongs.SelectedIndex);
            this.refreshSongList();
            if (oldIndex > CurrentSet.songList.Count - 1)
                oldIndex = CurrentSet.songList.Count - 1;
            lbSongs.SelectedIndex = oldIndex;
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
                Helpers.PopupMessages.ShowInformationMessage(this, "Set '{0}' saved", CurrentSet.setName);
            }
        }

        internal void revertSet()
        {
            if (SetChanged)
            {
                CurrentSet.revertSet();
                this.refreshSongList();
                SetChanged = false;
                Helpers.PopupMessages.ShowInformationMessage(this, "Set '{0}' reverted", CurrentSet.setName);
            }
        }

        internal void ExportToHtml(DisplayAndPrintSettings settings)
        {
            string destination = CurrentSet.ExportToHtml(settings);
            Process.Start(destination);
        }

        internal void saveSetBeforeClosing()
        {
            if (SetChanged && Helpers.PopupMessages.ShowConfirmationMessage(this, "Save changes to set '{0}' before closing OpenChords?", CurrentSet.setName))
                saveSet();
        }

        internal void ExportToOpenSong()
        {
            if (!Settings.GlobalApplicationSettings.IsOpenSongDataFolderConfigured)
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Opensong data folder is not configured");
                return;
            }

            //export the results
            CurrentSet.exportSetAndSongsToOpenSong();

            //launch OpenSong
            OpenChords.Export.ExportToOpenSong.launchOpenSong();


        }



        internal void deleteSet()
        {
            if (string.IsNullOrEmpty(CurrentSet.setName))
            {
                Helpers.PopupMessages.ShowErrorMessage(this, "Please select a set first");
                return;
            }

            if (Helpers.PopupMessages.ShowConfirmationMessage(this, "Delete set '{0}'?", CurrentSet.setName))
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
            UiHelper.ShowInFileManager(songPath);

        }

        internal void ExportToTextFile()
        {
            //get the filemanager for the filesystem
            string fileManager = OpenChords.Settings.GlobalApplicationSettings.fileManager;
            string filename = null;

            //get the filename
            filename = Export.ExportToTextFile.exportSetSongList(CurrentSet);
            System.Diagnostics.Process.Start(filename);
        }
    }
}
