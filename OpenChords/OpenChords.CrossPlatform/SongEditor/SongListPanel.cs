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

            lbSongs.Width = Helpers.getScreenPercentageInPixels(15);

            txtSearch.Focus();
        }

        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedIndex < 0) return;
            string songName = lbSongs.SelectedValue.ToString();
            Song selectedSong = Song.loadSong(songName);
            if (SongChanged != null)
                SongChanged(this, selectedSong);
        }

        public void refreshPanel()
        {
            lbSongs.SelectedIndexChanged -= lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged -= txtSearch_TextChanged;

            _fullSongList = Song.listOfAllSongs().ToArray();

            setListItems(_fullSongList);

            lbSongs.SelectedIndexChanged += lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyUp += txtSearch_KeyUp;

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

    }
}
