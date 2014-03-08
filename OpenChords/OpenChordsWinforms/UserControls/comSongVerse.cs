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
    public partial class comSongVerse : Panel
    {
        public comSongVerse()
        {
            InitializeComponent();
        }

        private SongVerse _verse;
        private DisplayAndPrintSettings _displaySettings;
        private Graphics graphics;
        SongElementFormat lyricsFormattor;
        SongElementFormat chordsFormattor;
        SongElementFormat notesFormattor;
        SongElementFormat headingFormattor;

        private Size _lyricsSize;
        private Size _headingSize;
        private Size _notesSize;

        float heightPosition = 0.0F;

        public comSongVerse(SongVerse verse, DisplayAndPrintSettings displaySettings)
        {
            _verse = verse;
            _displaySettings = displaySettings;
            lyricsFormattor = displaySettings.LyricsFormat;
            chordsFormattor = displaySettings.ChordFormat;
            notesFormattor = displaySettings.NoteFormat;
            headingFormattor = displaySettings.HeadingsFormat;
            this.BackColor = displaySettings.BackgroundColor;
            Padding = new Padding(0);
            Margin = new Padding(3);

            _headingSize = calculateSize(headingFormattor, _verse.FullHeaderName);
            _notesSize = calculateSize(notesFormattor, _verse.Notes);
            _lyricsSize = calculateSize(lyricsFormattor, _verse.Lyrics);

            Width = _notesSize.Width + _lyricsSize.Width + 30;
            Height = _lyricsSize.Height + _headingSize.Height;
          
         }

        
        private Size calculateSize(SongElementFormat formatter, string text)
        {
            text = text.TrimEnd('\r', '\n');
            Size size = TextRenderer.MeasureText(text, formatter.Font);
            size.Width += 10;
            return size;
        }

        private Size calculateSize(SongElementFormat formatter, List<string> lines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                var trim = line.TrimEnd();
                if (trim.Length > 0)
                sb.AppendLine(trim);
            }
            return calculateSize(formatter, sb.ToString());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            graphics = e.Graphics;

            

            drawHeading();
            drawNotes();
            drawVerse();
        }

        private void drawHeading()
        {
            graphics.DrawString(_verse.FullHeaderName, headingFormattor.Font, headingFormattor.Brush, 0, heightPosition);
            heightPosition += headingFormattor.FontSize;
        }

        private void drawNotes()
        {
            graphics.DrawString(_verse.Notes, notesFormattor.Font, notesFormattor.Brush, _lyricsSize.Width+20, heightPosition);
        }

        


        void drawVerse()
        {
            
            var lyrics = _verse.Lyrics;
            var IsChord = _verse.IsChord;
            for (int j = 0; j < _verse.Lyrics.Count(); j++)
            {
                var isChordLine = IsChord[j];
                var lyricLine = lyrics[j].TrimEnd();
                if (isChordLine && (_displaySettings.ShowChords ?? false))
                {

                    graphics.DrawString(lyricLine,
                                        chordsFormattor.Font,
                                        chordsFormattor.Brush,
                                        0,
                                        heightPosition);
                }
                else if (!isChordLine && (_displaySettings.ShowLyrics ?? false))
                {
                    graphics.DrawString(lyricLine,
                                        lyricsFormattor.Font,
                                        lyricsFormattor.Brush,
                                        0,
                                        heightPosition);
                }
                heightPosition += lyricsFormattor.FontSize;

            }
        }

        
    }
}
