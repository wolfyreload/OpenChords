using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
                if (value > _set.songList.Count())
                    value = _set.songList.Count - 1;
                if (value < 0) value = 0;
                _songIndex = value;
            }

        }
        
        public comSongDisplay()
        {
            InitializeComponent();
        }


        public void LoadSet(Set set, DisplayAndPrintSettings settings)
        {
            _set = set;
            _displaySettings = settings;
            _songIndex = 0;
        }

        public void drawSong()
        {
            flowSongSegments.Controls.Clear();
            var currentSong = _set.songList[SongIndex];
            var flowVerses = getNewVerseCollection();
            foreach (SongVerse verse in currentSong.getSongVerses())
            {
            
                var pnlVerse = new UserControls.comSongVerse(verse, _displaySettings);
                flowVerses.Controls.Add(pnlVerse);
                if (VerseOutsideOfPane(pnlVerse))
                {
                    flowVerses.Controls.Remove(pnlVerse);
                    flowSongSegments.Controls.Add(flowVerses);
                    flowVerses = getNewVerseCollection();
                }
            }
            flowSongSegments.Controls.Add(flowVerses);
            
        }

        private FlowLayoutPanel getNewVerseCollection()
        {
            var flowVerses = new FlowLayoutPanel();
            flowVerses.AutoSize = true;
            flowVerses.FlowDirection = FlowDirection.TopDown;
            return flowVerses;
        }

        private bool VerseOutsideOfPane(Control con)
        {
            return con.Location.Y + con.Height > this.Height;
        }

        public void moveToNextSlideOrSong()
        {
            //var currentPos = flowSong.HorizontalScroll.Value;
            //var max = flowSong.HorizontalScroll.Maximum;
            //var pos = flowSong.HorizontalScroll.Value + this.Width;

            //if (currentPos != max)
            //{
            //    if (pos > max) pos = max;
            //    flowSong.HorizontalScroll.Value = pos;
            //    return;
            //}
            //else
            //{
            //    SongIndex++;
            //    drawSong();
            //    return;
            //}

        }

        public void moveToPreviousSlideOrSong()
        {
            //var currentPos = flowSong.HorizontalScroll.Value;
            //var min = flowSong.HorizontalScroll.Minimum;
            //var max = flowSong.HorizontalScroll.Maximum;
            //var pos = flowSong.HorizontalScroll.Value - this.Width;

            //if (currentPos != min)
            //{
            //    if (pos < min) pos = min;
            //    flowSong.HorizontalScroll.Value = pos;
            //    return;
            //}
            //else
            //{
            //    SongIndex--;
            //    drawSong();
            //    return;
            //}
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
    }
}
