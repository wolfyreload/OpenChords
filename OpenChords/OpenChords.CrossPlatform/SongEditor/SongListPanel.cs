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
        protected GridView gridSongs = new GridView();
        protected TextBox txtSearch = new TextBox();
        protected GroupBox gbSongs;

        public event EventHandler<Song> SongChanged;
        public event EventHandler<Song> SongDeleting;
        public event EventHandler<Song> AddSongToSet;
        private List<Song> _fullSongList;
        private string _orderByColumn;
        private bool _isAscendingOrder = true;

        public SongListPanel()
        {
            txtSearch.PlaceholderText = "search for a song";
            Content = gbSongs = new GroupBox()
            {
                Text = "Songs",
                Content = new TableLayout()
                {
                    Rows =
                    {
                        new TableRow(gridSongs) { ScaleHeight = true },
                        txtSearch,
                    }
                }
            };

            var commandAddToSet = new Command() { MenuText = "Add to set", Shortcut = Application.Instance.CommonModifier | Keys.Enter, Image = Graphics.ImageAddToSet };
            commandAddToSet.Executed += (s, e) => addSongToSet();
            var commandDeleteSong = new Command() { MenuText = "Delete song", Shortcut = Application.Instance.CommonModifier | Keys.Delete, Image = Graphics.ImageDelete };
            commandDeleteSong.Executed += (s, e) => deleteSong();
            var menuItemSongRandom = new Command { MenuText = "Select random song", Shortcut = Application.Instance.CommonModifier | Keys.M, Image = Graphics.ImageRandom };
            menuItemSongRandom.Executed += (s, e) => selectRandomSong();

            //setup the gridview
            gridSongs.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Song, string>(r => r.title) },
                HeaderText = "Title",
                AutoSize = false,
                Width = 350
            });
            gridSongs.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Song, string>(r => r.hymn_number) },
                HeaderText = "Reference",
            });
            gridSongs.Columns.Add(new GridColumn
            {
                DataCell = new TextBoxCell { Binding = Binding.Property<Song, string>(r => r.author) },
                HeaderText = "Author",
            });

            var menu = new ContextMenu()
            {
                Items = { commandAddToSet, commandDeleteSong, menuItemSongRandom }
            };
            gridSongs.ContextMenu = menu;
            _fullSongList = new List<Song>();     
            txtSearch.Focus();
            gridSongs.ColumnHeaderClick += GridSongs_ColumnHeaderClick;

        }

        private void GridSongs_ColumnHeaderClick(object sender, GridColumnEventArgs e)
        {
            var gridSongList = (List<Song>)gridSongs.DataStore;
        
            //determine sort order
            if (e.Column.HeaderText == _orderByColumn)
                _isAscendingOrder = !_isAscendingOrder;
            else
                _isAscendingOrder = true;
            _orderByColumn = e.Column.HeaderText;


            //find the column that was clicked
            Func<Song, string> columnOrderFuction = null;
            if (_orderByColumn == "Title")
                columnOrderFuction = s => s.title;
            else if (_orderByColumn == "Reference")
                columnOrderFuction = s => padNumericPortion(s.hymn_number);
            else if (_orderByColumn == "Author")
                columnOrderFuction = s => s.author;

            //sort the grid
            if (_isAscendingOrder)
                gridSongList = gridSongList.OrderBy(columnOrderFuction).ToList();
            else
                gridSongList = gridSongList.OrderByDescending(columnOrderFuction).ToList();

            gridSongs.DataStore = gridSongList;
        }

        //pad any numbers in the string so we can sort by them
        private string padNumericPortion(string stringText)
        {
            if (stringText == null) return stringText;

            var match = System.Text.RegularExpressions.Regex.Match(stringText, @"(\d+)");
            var numberText = match.Value;
            int temp;
            if (int.TryParse(numberText, out temp))
                return int.Parse(numberText).ToString("D6");
            else
                return stringText;
        }

        private void deleteSong()
        {
            if (gridSongs.SelectedItem == null) return;
            Song selectedSong = (Song)gridSongs.SelectedItem;
            if (SongDeleting != null)
                SongDeleting(this, selectedSong);
        }

        private void addSongToSet()
        {
            if (gridSongs.SelectedItem == null) return;
            if (AddSongToSet != null)
                AddSongToSet(this, (Song)gridSongs.SelectedItem);
        }

        void lbSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridSongs.SelectedItem == null) return;
            Song selectedSong = (Song)gridSongs.SelectedItem;
            OpenChords.Functions.WebServer.CurrentSong = selectedSong;
            if (SongChanged != null)
                SongChanged(this, selectedSong);
        }

        public void refreshPanel()
        {
            gridSongs.SelectionChanged -= lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.KeyUp -= txtSearch_KeyUp;
            gridSongs.MouseDoubleClick -= lbSongs_MouseDoubleClick;
            gridSongs.KeyUp -= lbSongs_KeyUp;

            _fullSongList.Clear();
 
            string[] songNameList = Song.listOfAllSongs().ToArray();
            foreach (var songName in songNameList)
            {
                _fullSongList.Add(Song.loadSong(songName));
            }
            filterSongs();
       
            gridSongs.SelectionChanged += lbSongs_SelectedIndexChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyUp += txtSearch_KeyUp;
            gridSongs.MouseDoubleClick += lbSongs_MouseDoubleClick;
            gridSongs.KeyUp += lbSongs_KeyUp;
          
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
            else if (e.Control && e.Key == Keys.M)
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
                gridSongs.SelectRow(gridSongs.DataStore.Count() - 1);
                gridSongs.Focus();
            }
            else if (e.Key == Keys.Down)
            {
                gridSongs.SelectRow(0);
                gridSongs.Focus();
            }
        }

        private void setListItems(List<Song> songs)
        {
            gridSongs.DataStore = songs;
            gbSongs.Text = string.Format("Songs ({0})", songs.Count);
        }

        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gridSongs.SelectionChanged -= lbSongs_SelectedIndexChanged;

            filterSongs();

            gridSongs.SelectionChanged += lbSongs_SelectedIndexChanged;

        }

        private void filterSongs()
        {
            string search = txtSearch.Text.ToUpper();
            string[] searchItems = GetAllPhrasesSearchItems(search);
            var filteredList = _fullSongList.AsQueryable();
            foreach (string item in searchItems)
            {
                //smart filter
                filteredList = filteredList.Where(c => c.title.ToUpper().Contains(item) //filter on title
                                                     || c.author.ToUpper().Contains(item) //filter on author
                                                     || c.getJustLyrics().ToUpper().Contains(item) //filter on lyrics
                                                     || c.hymn_number.ToUpper().Contains(item) //filter on reference
                                                     || c.ccli.ToUpper().Contains(item) //filter on ccli
                                                     || c.SongFileName.ToUpper().Contains(item)); //filter on filename
            }
            setListItems(filteredList.ToList());
        }

        //split search string into search phrases
        private static string[] GetAllPhrasesSearchItems(string search)
        {
            List<string> results = new List<string>();

            //first let us split the out all the terms between quotes using regex
            string[] phrases = System.Text.RegularExpressions.Regex.Split(search, "(\".*?\")");

            foreach (string phrase in phrases)
            {
                //if we have a space between the words then its a phrase
                if (phrase.Trim().Contains('"'))
                {
                    //remove the quotes and add the phrase
                    results.Add(phrase.Replace("\"", ""));
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
            gridSongs.SelectRow(selectedIndex);
        }

        public void showSongInExplorer()
        {
            if (gridSongs.SelectedItem == null) return;
            Song selectedSong = (Song)gridSongs.SelectedItem;
            showInExplorer(selectedSong.getFullPath());
        }

        private void showInExplorer(string songPath)
        {
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;

            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start("Explorer", string.Format("/select, \"{0}\"", songPath));
            else
                System.Diagnostics.Process.Start(fileManager, OpenChords.Settings.ExtAppsAndDir.SongsFolder);
        }
    }
}
