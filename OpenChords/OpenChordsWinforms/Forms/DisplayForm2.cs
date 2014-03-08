using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenChords.Forms
{
    public partial class DisplayForm2 : Form
    {
        public DisplayForm2()
        {
            InitializeComponent();
        }

        private Set _set;
        private DisplayAndPrintSettings _displaySettings;
        private int _songIndex = 0;


        public DisplayForm2(Set set, DisplayAndPrintSettings displaySettings)
        {
            InitializeComponent();
            _set = set;
            _displaySettings = displaySettings;
            _songIndex = 0;
            loadSong();
        }

        public DisplayForm2(Song song, DisplayAndPrintSettings displaySettings)
        {
            InitializeComponent();
            _set = new Set();
            _set.addSongToSet(song);
            _set.loadAllSongs();
            _displaySettings = displaySettings;
            _songIndex = 0;
            loadSong();
        }

        private void loadSong()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            var currentSong = _set.songList[_songIndex];

            foreach (SongVerse verse in currentSong.getSongVerses())
            {
                var pnlVerse = new UserControls.comSongVerse(verse, _displaySettings);
                flowSong.Controls.Add(pnlVerse);
            }
            this.KeyUp += DisplayForm2_KeyUp;
        }

        private void DisplayForm2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void DisplayForm2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
