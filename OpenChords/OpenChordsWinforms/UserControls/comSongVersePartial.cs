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
    public partial class comSongVersePartial : comSongVerse 
    {
        public comSongVersePartial() : base()
        {
        }

        public comSongVersePartial(SongVerse verse, DisplayAndPrintSettings displaySettings)
            : base(AddPtoText(verse), displaySettings)
        {
        }

        private static SongVerse AddPtoText(SongVerse verse)
        {
            var originalLyrics = " Press space to continue...";
            var newVerse = new SongVerse(verse.Header, originalLyrics);
            return newVerse;
        }
    }
}
