using OpenChords.Entities;
using OpenChords.UserControls;
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

        


        public DisplayForm2(Set set, DisplayAndPrintSettings displaySettings)
        {
            InitializeComponent();
            comSongDisplay1.LoadSet(set, displaySettings);
            loadForm();
        }

        public DisplayForm2(Song song, DisplayAndPrintSettings displaySettings)
        {
            InitializeComponent();
            var set = new Set();
            set.addSongToSet(song);
            set.loadAllSongs();
            comSongDisplay1.LoadSet(set, displaySettings);
            loadForm();
        }

        private void loadForm()
        {
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            comSongDisplay1.drawSong();
            this.KeyDown += DisplayForm2_KeyDown;
        }

        

        private void DisplayForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
                comSongDisplay1.increaseFontSize();
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                comSongDisplay1.decreaseFontSize();
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
                comSongDisplay1.moveToNextSlideOrSong();
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                comSongDisplay1.moveToPreviousSlideOrSong();

        }

        
        
        private void DisplayForm2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            comSongDisplay1.moveToNextSlideOrSong();
        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            comSongDisplay1.moveToPreviousSlideOrSong();
        }
    }
}
