using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;
using OpenChords.Entities;
using System.Collections.Generic;
using System.Linq;
using OpenChords.CrossPlatform.Helpers;

namespace OpenChords.CrossPlatform.SongEditor
{
    public class SongListPanel : Panel
    {
        protected ListBox gridSongs = new ListBox();
        protected TextBox txtSearch = new TextBox();
        protected GroupBox gbSongs;

        public event EventHandler<Song> SongChanged;
        public event EventHandler<Song> SongDeleting;
        public event EventHandler<Song> AddSongToSet;
        private List<Song> _fullSongList;
        private string _orderByColumn;
        private bool _isAscendingOrder = true;
        private ShortcutSettings shortcutKeys;


        public SongListPanel()
        {
            shortcutKeys = Entities.ShortcutSettings.LoadSettings();

            txtSearch.PlaceholderText = "search for a song";
            Content = gbSongs = new GroupBox()
            {
                Text = "Songs",
                Content = new TableLayout()
                {
                    Rows =
                    {
                        txtSearch,
                        new TableRow(gridSongs) { ScaleHeight = true },
                    }
                }
            };

            var commandAddToSet = MenuHelper.GetCommand("Add to set", Graphics.ImageAddToSet, shortcutKeys.AddSongToSet);
            commandAddToSet.Executed += (s, e) => addSongToSet();
            var commandDeleteSong = MenuHelper.GetCommand("Delete song", Graphics.ImageDelete, shortcutKeys.DeleteSong);
            commandDeleteSong.Executed += (s, e) => deleteSong();
            var menuItemSongRandom = MenuHelper.GetCommand("Select random song", Graphics.ImageRandom, shortcutKeys.SelectRandomSong);
            menuItemSongRandom.Executed += (s, e) => selectRandomSong();

            //setup the gridview
            var menu = new ContextMenu()
            {
                Items = { commandAddToSet, commandDeleteSong, menuItemSongRandom }
            };
            gridSongs.ContextMenu = menu;
            _fullSongList = new List<Song>();     
            gridSongs.MouseDoubleClick += GridSongs_MouseDoubleClick;

            txtSearch.Font = Helpers.FontHelper.GetFont(UserInterfaceSettings.Instance.TextboxFormat);
        }
        
        private void deleteSong()
        {
            if (gridSongs.SelectedIndex < 0) return;
            Song selectedSong = getSelectedSong(gridSongs);
            if (SongDeleting != null)
                SongDeleting(this, selectedSong);
        }

        private Song getSelectedSong(ListBox grid)
        {
            string songName = grid.SelectedKey;
            return _fullSongList.First(s => s.SongFileName == songName);
        }

        private void addSongToSet()
        {
            if (gridSongs.SelectedIndex < 0) return;
            Song selectedSong = getSelectedSong(gridSongs);
            if (AddSongToSet != null)
                AddSongToSet(this, selectedSong);
        }

        private void GridSongs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addSongToSet();
        }

        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridSongs.SelectedIndex < 0) return;
            Song selectedSong = getSelectedSong(gridSongs);
            OpenChords.Functions.WebServer.CurrentSong = selectedSong;
            if (SongChanged != null)
                SongChanged(this, selectedSong);
        }

        public void refreshPanel()
        {
            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.KeyUp -= txtSearch_KeyUp;
            gridSongs.MouseDoubleClick -= lbSongs_MouseDoubleClick;
            gridSongs.KeyUp -= lbSongs_KeyUp;
            gridSongs.SelectedIndexChanged -= lbSongs_SelectedIndexChanged;

            _fullSongList.Clear();
 
            string[] songNameList = Song.listOfAllSongs().ToArray();
            foreach (var songName in songNameList)
            {
                _fullSongList.Add(Song.loadSong(songName));
            }
            filterAndSortSongs();
       
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyUp += txtSearch_KeyUp;
            gridSongs.MouseDoubleClick += lbSongs_MouseDoubleClick;
            gridSongs.KeyUp += lbSongs_KeyUp;
            gridSongs.SelectedIndexChanged += lbSongs_SelectedIndexChanged;   
        }

        void lbSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (MenuHelper.CompareShortcuts(e, shortcutKeys.AddSongToSet))
            {
                addSongToSet();
                e.Handled = true;
            }
            else if (MenuHelper.CompareShortcuts(e, shortcutKeys.DeleteSong))
            {
                deleteSong();
                e.Handled = true;
            }
            else if (MenuHelper.CompareShortcuts(e, shortcutKeys.SelectRandomSong))
            {
                selectRandomSong();
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
                gridSongs.SelectedIndex = gridSongs.DataStore.Count() - 1;
                gridSongs.Focus();
            }
            else if (e.Key == Keys.Down)
            {
                gridSongs.SelectedIndex = 0;
                gridSongs.Focus();
            }
            else if (e.Key == Keys.Enter)
            {
                filterAndSortSongs(fullTextSearch: true);
            }
        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            filterAndSortSongs();
        }

        private void filterAndSortSongs(bool fullTextSearch = false)
        {
            string search = txtSearch.Text.ToUpper();
            string[] searchItems = getAllPhrasesSearchItems(search);
            var filteredList = _fullSongList.AsQueryable();
            foreach (string item in searchItems)
            {
                //if we using a full text search we search on lyrics
                if (fullTextSearch)
                {
                    filteredList = filteredList.Where(c => c.title.ToUpper().Contains(item) //filter on title
                                               || c.aka.ToUpper().Contains(item) //filter on alternative title
                                               || c.author.ToUpper().Contains(item) //filter on author
                                               || c.hymn_number.ToUpper().Contains(item) //filter on reference
                                               || c.ccli.ToUpper().Contains(item) //filter on ccli
                                               || c.SongFileName.ToUpper().Contains(item) //filter on filename
                                               || c.SongSubFolder.ToUpper().Contains(item) //filter on sub folder
                                               || c.getJustLyrics().Contains(item)); //filter on lyrics

                }
                else
                {
                    filteredList = filteredList.Where(c => c.title.ToUpper().Contains(item) //filter on title
                                              || c.aka.ToUpper().Contains(item) //filter on alternative title
                                              || c.author.ToUpper().Contains(item) //filter on author
                                              || c.hymn_number.ToUpper().Contains(item) //filter on reference
                                              || c.ccli.ToUpper().Contains(item) //filter on ccli
                                              || c.SongFileName.ToUpper().Contains(item) //filter on filename
                                              || c.SongSubFolder.ToUpper().Contains(item)); //filter on sub folder
    
                }
            }

            //assign the songs to the grid
            ListItemCollection items = new ListItemCollection();
            foreach (var filteredItem in filteredList)
            {
                items.Add(filteredItem.SongFileName);
            }
            gridSongs.DataStore = items;

            //count the number of songs available when filtered
            gbSongs.Text = string.Format("Songs ({0})", filteredList.Count());
        }

        //split search string into search phrases
        private static string[] getAllPhrasesSearchItems(string search)
        {
            List<string> results = new List<string>();

            //first let us split the out all the terms between quotes using regex
            string[] phrases = System.Text.RegularExpressions.Regex.Split(search, "(\".*?\")");

            foreach (string phrase in phrases)
            {
                //if we have a space between the words then its a phrase
                if (phrase.Trim().Contains('"'))
                {
                    string justCharsAndDigits = new string(phrase.ToCharArray().Where(c => char.IsLetterOrDigit(c)).ToArray());
                    results.Add(justCharsAndDigits);
                }
                else //we need to split on spaces because its not a quoted phrase
                {
                    string[] searchWords = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    results.AddRange(searchWords);
                }
            }

            return results.ToArray();
        }

        internal void selectRandomSong()
        {
            var rand = new Random();
            var selectedIndex = rand.Next(gridSongs.DataStore.Count());
            gridSongs.SelectedIndex = selectedIndex;
        }

        public void showSongInExplorer()
        {
            if (gridSongs.SelectedIndex < 0) return;
            Song selectedSong = getSelectedSong(gridSongs); ;
            UiHelper.ShowInFileManager(selectedSong.getFullPath());
        }
    }
}
