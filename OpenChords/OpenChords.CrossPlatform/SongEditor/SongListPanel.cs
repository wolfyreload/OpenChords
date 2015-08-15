using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SongListPanel : Panel
    {
        protected ListBox lbSongs = new ListBox();
        protected TextBox txtSearch = new TextBox();

        public event EventHandler<Song> SongChanged;
        public event EventHandler<Song> SongDeleting;
        public event EventHandler<Song> AddSongToSet;
        private string[] _fullSongList;

        public SongListPanel()
        {
            txtSearch.PlaceholderText = "search for a song";
            Content = new TableLayout()
            {
                Rows = 
                {
                    new TableRow(lbSongs) { ScaleHeight = true },
                    txtSearch,
                }
            };

            lbSongs.Width = Helpers.getScreenPercentageInPixels(15, this);

            var commandAddToSet = new Command() { MenuText = "Add to set", Shortcut = Application.Instance.CommonModifier | Keys.Enter };
            commandAddToSet.Executed += (s, e) => addSongToSet();
            var commandDeleteSong = new Command() { MenuText = "Delete song", Shortcut = Application.Instance.CommonModifier | Keys.Delete };
            commandDeleteSong.Executed += (s, e) => deleteSong();


            var menu = new ContextMenu()
            {
                Items = { commandAddToSet, commandDeleteSong }
            };
            lbSongs.ContextMenu = menu;            
            txtSearch.Focus();
        }

        private void deleteSong()
        {
            if (lbSongs.SelectedIndex < 0) return;
            string songName = lbSongs.SelectedValue.ToString();
            Song selectedSong = Song.loadSong(songName);
            if (SongDeleting != null)
                SongDeleting(this, selectedSong);
        }

        private void addSongToSet()
        {
            if (lbSongs.SelectedIndex < 0) return;
            if (AddSongToSet != null)
                AddSongToSet(this, Song.loadSong(lbSongs.SelectedValue.ToString()));
        }

        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedIndex < 0) return;
            string songName = lbSongs.SelectedValue.ToString();
            Song selectedSong = Song.loadSong(songName);
            OpenChords.Functions.WebServer.CurrentSong = selectedSong;
            if (SongChanged != null)
                SongChanged(this, selectedSong);
        }

        public void refreshPanel()
        {
            lbSongs.SelectedIndexChanged -= lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.KeyUp -= txtSearch_KeyUp;
            lbSongs.MouseDoubleClick -= lbSongs_MouseDoubleClick;
            lbSongs.KeyUp -= lbSongs_KeyUp;

            _fullSongList = Song.listOfAllSongs().ToArray();

            setListItems(_fullSongList);

            lbSongs.SelectedIndexChanged += lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyUp += txtSearch_KeyUp;
            lbSongs.MouseDoubleClick += lbSongs_MouseDoubleClick;
            lbSongs.KeyUp += lbSongs_KeyUp;
          
        }

        void lbSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Key == Keys.Enter)
            {
                addSongToSet();
                e.Handled = true;
            }
            else if (e.Control && e.Key == Keys.Delete)
            {
                deleteSong();
                e.Handled = true;
            }

        }

        void lbSongs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //disable double click to add song because its too easy to double click by accident
            //if (e.Buttons == MouseButtons.Primary)
            //    addSongToSet();
        }

        void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Up)
            {
                lbSongs.SelectedIndex = lbSongs.Items.Count - 1;
                lbSongs.Focus();
            }
            else if (e.Key == Keys.Down)
            {
                lbSongs.SelectedIndex = 0;
                lbSongs.Focus();
            }
        }

        private void setListItems(string[] stringSongs)
        {
	
            ListItemCollection listItems = new ListItemCollection();
            foreach (string stringsong in stringSongs)
            {
                listItems.Add(stringsong);
            }
            lbSongs.DataStore = listItems;


        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
			lbSongs.SelectedIndexChanged -= lbSongs_SelectedIndexChanged;

			string search = txtSearch.Text.ToUpper();
            string[] searchItems = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var filteredList = _fullSongList.AsQueryable();
            foreach (string item in searchItems)
            {
                filteredList = filteredList.Where(c => c.ToUpper().Contains(item));
            }
            setListItems(filteredList.ToArray());

			lbSongs.SelectedIndexChanged += lbSongs_SelectedIndexChanged;

        }


        public void showSongInExplorer()
        {
            if (lbSongs.SelectedIndex < 0) return;
            string songName = lbSongs.SelectedValue.ToString();
            Song selectedSong = Song.loadSong(songName);
            showInExplorer(selectedSong.getFullPath());
        }

        private void showInExplorer(string songPath)
        {
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;

            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start("Explorer", "/select, " + songPath);
            else
                System.Diagnostics.Process.Start(fileManager, OpenChords.Settings.ExtAppsAndDir.songsFolder);
        }
    }
}
