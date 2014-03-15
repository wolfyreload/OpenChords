using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenChords.UserControls
{
    public partial class comSongDisplay : UserControl
    {
        private Set _set;
        private DisplayAndPrintSettings _displaySettings;

        private int _songIndex;
        private int _maxSongIndex;
        private int _currentScreenIndex;
        private int _maxScreenIndex;
        
        public comSongDisplay()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            titleFormatter = new SongElementFormat("Courier New", 10, Color.Blue, true);
            order1Formatter = new SongElementFormat("Courier New", 10, Color.Blue, true);
            order2Formatter = new SongElementFormat("Courier New", 10, Color.Blue, true);
            _displaySettings = new DisplayAndPrintSettings();
        }

        private SongElementFormat titleFormatter;
        private SongElementFormat order1Formatter;

        


        public void LoadSet(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _displaySettings = settings;
            _songIndex = 0;
            _maxSongIndex = _set.songList.Count - 1;
            this.BackColor = settings.BackgroundColor;
            titleFormatter = _displaySettings.TitleFormat;
            order1Formatter = _displaySettings.Order1Format;
            order2Formatter = _displaySettings.Order2Format;
        }


        private string _songTitle = "";
        private string _songOrder = "";

        private FlowLayoutPanel createNewScreen()
        {
            var flow = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                Visible = false,
                Width = pnlLyrics.Width,
                Height = pnlLyrics.Height
            };
            pnlLyrics.Controls.Add(flow);
            return flow;
        }

        private bool songRendered = false;
        private SongElementFormat order2Formatter;
        public void renderSongInMemory()
        {
            pnlLyrics.Controls.Clear();
            
            var currentSong = _set.songList[_songIndex];

            _songTitle = currentSong.generateLongTitle();
            _songOrder = currentSong.presentation;

            var screen = createNewScreen();
            var songVerses = currentSong.getSongVerses();
            var versesOnScreenCounter = 0;
            for (int i = 0; i < songVerses.Count; i++)
            {
                var songVerse = songVerses[i];
                var renderedVerse = new UserControls.comSongVerse(songVerse, _displaySettings);
                screen.Controls.Add(renderedVerse);
                if (!VerseVisible(renderedVerse))
                {
                    screen.Controls.RemoveAt(versesOnScreenCounter);
                    screen = createNewScreen();
                    screen.Controls.Add(renderedVerse);
                    versesOnScreenCounter = 0;
                }
                versesOnScreenCounter++;
            }
            _maxScreenIndex = pnlLyrics.Controls.Count - 1;
            songRendered = true;
        }
        
        public void drawSong()
        {
            if (!songRendered)
                renderSongInMemory();

            if (_currentScreenIndex <= _maxScreenIndex)
                pnlLyrics.Controls[_currentScreenIndex].Visible = true;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphicsObj = e.Graphics;

            var y = 10;
            graphicsObj.DrawString(_songTitle, titleFormatter.Font, titleFormatter.Brush, _displaySettings.leftPageMargin, y);
            y += (int)titleFormatter.FontSize+5;
            paintOrder(graphicsObj, y);
           
            base.OnPaint(e);
        }

        private void paintOrder(Graphics graphicsObj, int y)
        {
            if (pnlLyrics.Controls.Count == 0) return;
            if (_songOrder.Trim() == "") return;

            var orderElements = _songOrder.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            float x = (float)_displaySettings.leftPageMargin;

            var index = 0;
            for (int i = 0; i < pnlLyrics.Controls.Count; i++)
            {
                var screen = pnlLyrics.Controls[i];
                var visible = screen.Visible;
                for (int j = 0; j < screen.Controls.Count; j++)
                {
                    
                    if (visible)
                    {
                        graphicsObj.DrawString(orderElements[index], order2Formatter.Font, order2Formatter.Brush, x, y);
                        x += graphicsObj.MeasureString(orderElements[index], order2Formatter.Font).Width+10;
                    }
                    else
                    {
                        graphicsObj.DrawString(orderElements[index], order2Formatter.Font, order1Formatter.Brush, x, y);
                        x += graphicsObj.MeasureString(orderElements[index], order1Formatter.Font).Width+10;
                    }    
                    index++;
                }
            }


                
        }


        private bool VerseVisible(Control con)
        {
            return con.Location.X + con.Width < con.Parent.Width;
        }

        public void moveToNextSlideOrSong()
        {            
            var nextScreenIndex = _currentScreenIndex + 1;

            if (nextScreenIndex <= _maxScreenIndex)
            {
                pnlLyrics.Controls[_currentScreenIndex].Visible = false;
                pnlLyrics.Controls[nextScreenIndex].Visible = true;
                _currentScreenIndex++;
                this.Invalidate();
            }
            else
            {
                nextSong();
            }
        }

        public void moveToPreviousSlideOrSong()
        {
            var nextScreenIndex = _currentScreenIndex - 1;

            if (_maxSongIndex >= nextScreenIndex && nextScreenIndex >= 0)
            {
                pnlLyrics.Controls[_currentScreenIndex].Visible = false;
                pnlLyrics.Controls[nextScreenIndex].Visible = true;
                _currentScreenIndex--;
                this.Invalidate();
            }
            else
            {
                previousSong();
            }
        }

        public void increaseFontSize()
        {
            _displaySettings.increaseSizes();
            songRendered = false;
            drawSong();
        }

        public void decreaseFontSize()
        {
            _displaySettings.desceaseSizes();
            songRendered = false;
            drawSong();
        }

        internal void increaseKey()
        {
            var currentSong = _set.songList[_songIndex];
            currentSong.transposeKeyUp();
            currentSong.saveSong();
            songRendered = false;
            drawSong();
        }

        internal void decreaseKey()
        {
            var currentSong = _set.songList[_songIndex];
            currentSong.transposeKeyDown();
            currentSong.saveSong();
            songRendered = false;
            drawSong();
        }

        internal void increaseCapo()
        {
            var currentSong = _set.songList[_songIndex];
            currentSong.capoUp();
            currentSong.saveSong();
            songRendered = false;
            drawSong();
        }

        internal void decreaseCapo()
        {
            var currentSong = _set.songList[_songIndex];
            currentSong.capoDown();
            currentSong.saveSong();
            songRendered = false;
            drawSong();
        }

        internal void toggleWords()
        {
            _displaySettings.ShowLyrics = !_displaySettings.ShowLyrics;
            songRendered = false;
            drawSong();
        }

        internal void toggleChords()
        {
            _displaySettings.ShowChords = !_displaySettings.ShowChords;
            songRendered = false;
            drawSong();
        }

        internal void toggleNotes()
        {
            _displaySettings.ShowNotes = !_displaySettings.ShowNotes;
            songRendered = false;
            drawSong();
        }

        internal void toggleSharpsAndFlats()
        {
            var currentSong = _set.songList[_songIndex];
            currentSong.PreferFlats = !currentSong.PreferFlats;
            currentSong.transposeKeyUp();
            currentSong.transposeKeyDown();
            currentSong.saveSong();
            songRendered = false;
            drawSong();
            
        }

        internal void nextSong()
        {
            var nextSongIndex = _songIndex + 1;
            if (nextSongIndex <= _maxSongIndex)
            {
                songRendered = false;
                _songIndex++;
                _currentScreenIndex = 0;
                drawSong();
            }
        }

        internal void previousSong()
        {
            var nextSongIndex = _songIndex - 1;
            if (nextSongIndex >= 0)
            {
                _songIndex--;
                renderSongInMemory();
                _currentScreenIndex = _maxScreenIndex;
                drawSong();
            }
        }

        /// <summary>
        /// finds the song in the set and switches to the song
        /// </summary>
        /// <param name="p"></param>
        internal void changeToSong(string p)
        {
            var index = _set.songNames.IndexOf(p);
            _songIndex = index;
            songRendered = false;
            drawSong();
        }

        /// <summary>
        /// finds the song in the set and switches to the song
        /// </summary>
        /// <param name="p"></param>
        internal void changeToSong(int p)
        {
            if (0 <= p && p <= _maxSongIndex)
            {
                _songIndex = p;
                songRendered = false;
                drawSong();
            }
        }

        public string getCurrentSong()
        {
            var songName = _set.songNames[_songIndex];
            return songName;
        }

        internal void refresh()
        {
            _set.loadAllSongs();
            songRendered = false;
        }

        internal int getCurrentSongIndex()
        {
            return _songIndex;
        }
    }
}
