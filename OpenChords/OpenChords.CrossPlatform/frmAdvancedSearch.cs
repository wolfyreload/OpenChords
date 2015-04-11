using Eto.Forms;
using OpenChords.CrossPlatform.SongEditor;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    public class frmAdvancedSearch : Form
    {
        private static List<Song> __Songs;
        private static List<Song> Songs
        {
            get
            {
                //if all songs have not been loaded into memory load them
                if (__Songs == null)
                {
                    __Songs = new List<Song>();
                    foreach (string songName in Song.listOfAllSongs())
                    {
                        __Songs.Add(Song.loadSong(songName));
                    }
                }
                return __Songs;
            }
        }

        protected TextBox txtTitle;
        protected TextBox txtAuthor;
        protected TextBox txtKey;
        protected TextBox txtLyrics;
        protected TextBox txtNotes;
        protected ListBox lbSongs;
        protected SongMetadataPanel ucSongMetadataPanel;
        protected Splitter splitterSearchSearchResults;

        public event EventHandler<Song> AddSongToSet;


        private enum SearchType
        {
            Exact,
            Contains,
            AllWords
        }

        public frmAdvancedSearch()
        {
            Title = "Advanced Search";
            Icon = Graphics.Icon;
            Width = 1024;
            Height = 768;

            Content = splitterSearchSearchResults = new Splitter()
            {
                Position = Helpers.getScreenPercentageInPixels(20, this),
                Panel1 = new TableLayout()
                {
                    Rows = 
                    {
                        new GroupBox()
                        {
                            Text = "Search Criteria",
                            Content = new TableLayout()
                            {
                                Style = "padded-table",
                                Rows = 
                                {
                                    new TableRow(new Label() { Text = "Title"}, txtTitle = new TextBox()),
                                    new TableRow(new Label() { Text = "Author"}, txtAuthor = new TextBox()),
                                    new TableRow(new Label() { Text = "Key"}, txtKey = new TextBox()),
                                    new TableRow(new Label() { Text = "Lyrics"}, txtLyrics = new TextBox()),
                                    new TableRow(new Label() { Text = "Notes"}, txtNotes = new TextBox()),
                                }
                            }
                        },
                        new GroupBox() 
                        {
                            Text = "Matching Songs",
                            Content = lbSongs = new ListBox()
                        }
                    }
                },
                Panel2 = new GroupBox()
                {
                    Text = "Preview Song",
                    Content = ucSongMetadataPanel = new SongMetadataPanel()
                }
            };

            //context menu items
            var commandAddToSet = new Command() { MenuText = "Add to set", Shortcut = Application.Instance.CommonModifier | Keys.Enter };
            commandAddToSet.Executed += (s, e) => addSongToSet();
            var menu = new ContextMenu()
            {
                Items = { commandAddToSet }
            };
            lbSongs.ContextMenu = menu;    

            txtTitle.TextChanged += Filter_TextChanged;
            txtAuthor.TextChanged += Filter_TextChanged;
            txtKey.TextChanged += Filter_TextChanged;
            txtLyrics.TextChanged += Filter_TextChanged;
            txtNotes.TextChanged += Filter_TextChanged;

            txtTitle.KeyUp += filter_KeyUp;
            txtAuthor.KeyUp += filter_KeyUp;
            txtKey.KeyUp += filter_KeyUp;
            txtLyrics.KeyUp += filter_KeyUp;
            txtNotes.KeyUp += filter_KeyUp;
            
            lbSongs.SelectedIndexChanged += lsSongs_SelectedIndexChanged;
            ucSongMetadataPanel.SetReadOnlyMode();
            
            lbSongs.KeyUp += lbSongs_KeyUp;
            loadSongs();
            
        }

        void lsSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSongs.SelectedIndex == -1)
            {
                ucSongMetadataPanel.CurrentSong = null;
                ucSongMetadataPanel.refreshPanel();
                return;
            }
            ListItem selectedItem = (ListItem)lbSongs.SelectedValue;
            ucSongMetadataPanel.CurrentSong = (Song)selectedItem.Tag;
            ucSongMetadataPanel.refreshPanel();
        }

        void filter_KeyUp(object sender, KeyEventArgs e)
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

        private void loadSongs()
        {
            var currentlySelectedSong = ucSongMetadataPanel.CurrentSong;
            bool currentSongIsInSearchList = false;
            ListItemCollection items = new ListItemCollection();
            foreach (Song song in Songs)
            {
                if (matchFilters(song))
                {
                    ListItem item = new ListItem()
                    {
                        Text = song.title,
                        Key = song.title,
                        Tag = song
                    };
                    if (song == currentlySelectedSong)
                        currentSongIsInSearchList = true;
                    items.Add(item);
                }
            }

            lbSongs.DataStore = items;
            //if the currently selected song is not in the search list select the first song in the list
            if (lbSongs.Items.Count() > 0 && !currentSongIsInSearchList)
                lbSongs.SelectedIndex = -1;
            
                
        }

        private bool matchFilters(Song song)
        {
            bool matches = true;
            if (!string.IsNullOrEmpty(txtTitle.Text))
                matches &= isMatch(matchText: txtTitle.Text, songElementText: song.title, searchType: SearchType.AllWords);
            if (!string.IsNullOrEmpty(txtAuthor.Text))
                matches &= isMatch(matchText: txtAuthor.Text, songElementText: song.author, searchType: SearchType.AllWords);
            if (!string.IsNullOrEmpty(txtKey.Text))
                matches &= isMatch(matchText: txtKey.Text, songElementText: song.key, searchType: SearchType.Exact);
            if (!string.IsNullOrEmpty(txtLyrics.Text))
                matches &= isMatch(matchText: txtLyrics.Text, songElementText: song.lyrics, searchType: SearchType.AllWords);
            if (!string.IsNullOrEmpty(txtNotes.Text))
                matches &= isMatch(matchText: txtNotes.Text, songElementText: song.notes, searchType: SearchType.AllWords);

            return matches;
        }

        private bool isMatch(string matchText, string songElementText, SearchType searchType)
        {
            if (searchType == SearchType.Exact)
                return songElementText.ToUpper() == matchText.ToUpper();
            else if (searchType == SearchType.Contains)
                return songElementText.ToUpper().Contains(matchText.ToUpper());
            else
            {
                bool isMatch = true;
                string[] items = matchText.ToUpper().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in items)
                {
                    isMatch &= songElementText.ToUpper().Contains(item);
                }
                return isMatch;
            }
        }

        private void Filter_TextChanged(object sender, EventArgs e)
        {
            loadSongs();
        }

        private void addSongToSet()
        {
            if (lbSongs.SelectedIndex < 0) return;
            if (AddSongToSet != null)
                AddSongToSet(this, Song.loadSong(lbSongs.SelectedValue.ToString()));
        }

        void lbSongs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Key == Keys.Enter)
                addSongToSet();
        }


    }
}
