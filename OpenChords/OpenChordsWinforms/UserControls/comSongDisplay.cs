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
        private int SongIndex
        {
            get { return _songIndex; }
            set
            {
                if (value >= _set.songList.Count())
                    value = _set.songList.Count - 1;
                if (value < 0) value = 0;
                _songIndex = value;
            }

        }
        
        public comSongDisplay()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            titleFormatter = new SongElementFormat("Courier New", 10, Color.Blue, true);
            orderFormatter = new SongElementFormat("Courier New", 10, Color.Blue, true);
            _displaySettings = new DisplayAndPrintSettings();
        }

        private SongElementFormat titleFormatter;
        private SongElementFormat orderFormatter;

        


        public void LoadSet(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _displaySettings = settings;
            _songIndex = 0;
            this.BackColor = settings.BackgroundColor;
            titleFormatter = _displaySettings.TitleFormat;
            orderFormatter = _displaySettings.Order1Format;
        }


        private string _songTitle = "";
        private string _songOrder = "";
        public void drawSong()
        {
            flowSongSegments.Controls.Clear();
            var currentSong = _set.songList[SongIndex];

            _songTitle = currentSong.generateLongTitle();
            _songOrder = currentSong.presentation;

            foreach (SongVerse verse in currentSong.getSongVerses())
            {
                var pnlVerse = new UserControls.comSongVerse(verse, _displaySettings);
                flowSongSegments.Controls.Add(pnlVerse); 
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphicsObj = e.Graphics;

            var y = 10;
            graphicsObj.DrawString(_songTitle, titleFormatter.Font, titleFormatter.Brush, _displaySettings.leftPageMargin, y);
            y += (int)titleFormatter.FontSize+5;
            graphicsObj.DrawString(_songOrder, orderFormatter.Font, orderFormatter.Brush, _displaySettings.leftPageMargin, y);
            


            base.OnPaint(e);
        }


        private bool VerseVisible(Control con)
        {
            return con.Location.X + con.Width < flowSongSegments.Width;
        }

        public void moveToNextSlideOrSong()
        {            
            //find the first control that is not visible
            int nextIndex = 0;
            for (int i = 0; i < flowSongSegments.Controls.Count; i++)
            {
                var verse = flowSongSegments.Controls[i];
                if (!VerseVisible(verse))
                {
                    nextIndex = i;
                    break;
                }
            }

            //hide all the other controls that were currently visible
            for (int i = 0; i < nextIndex; i++)
            {
                flowSongSegments.Controls[i].Visible = false;
            }

            if (nextIndex == 0)
            {
                SongIndex++;
                drawSong();
                return;
            }
        }

        public void moveToPreviousSlideOrSong()
        {
            int indexOfFirstVisibleControl = 0;
            for (int i = 0; i < flowSongSegments.Controls.Count; i++)
            {
                if (flowSongSegments.Controls[i].Visible == true)
                {
                    indexOfFirstVisibleControl = i;
                    break;
                }
            }
            var firstVisisbleControl = flowSongSegments.Controls[indexOfFirstVisibleControl];

            for (int i=indexOfFirstVisibleControl; i>=0; i--)
            {
                var verse = flowSongSegments.Controls[i];
                verse.Visible = true;
                if (!VerseVisible(firstVisisbleControl))
                    break;
            }

            if (indexOfFirstVisibleControl == 0)
            {
                SongIndex--;
                drawSong();
                return;
            }
        }

        public void increaseFontSize()
        {
            _displaySettings.increaseSizes();
            drawSong();
        }

        public void decreaseFontSize()
        {
            _displaySettings.desceaseSizes();
            drawSong();
        }

        internal void increaseKey()
        {
            var currentSong = _set.songList[SongIndex];
            currentSong.transposeKeyUp();
            drawSong();
        }

        internal void decreaseKey()
        {
            var currentSong = _set.songList[SongIndex];
            currentSong.transposeKeyDown();
            drawSong();
        }

        internal void increaseCapo()
        {
            var currentSong = _set.songList[SongIndex];
            currentSong.capoUp();
            drawSong();
        }

        internal void decreaseCapo()
        {
            var currentSong = _set.songList[SongIndex];
            currentSong.capoDown();
            drawSong();
        }

        internal void toggleWords()
        {
            _displaySettings.ShowLyrics = !_displaySettings.ShowLyrics;
            drawSong();
        }

        internal void toggleChords()
        {
            _displaySettings.ShowChords = !_displaySettings.ShowChords;
            drawSong();
        }

        internal void toggleNotes()
        {
            _displaySettings.ShowNotes = !_displaySettings.ShowNotes;
            drawSong();
        }

        internal void nextSong()
        {
            SongIndex++;
            drawSong();
        }

        internal void previousSong()
        {
            SongIndex--;
            drawSong();
        }

        /// <summary>
        /// finds the song in the set and switches to the song
        /// </summary>
        /// <param name="p"></param>
        internal void changeToSong(string p)
        {
            var index = _set.songNames.IndexOf(p);
            SongIndex = index;
            drawSong();
        }

        public string getCurrentSong()
        {
            var songName = _set.songNames[SongIndex];
            return songName;
        }
    }
}
