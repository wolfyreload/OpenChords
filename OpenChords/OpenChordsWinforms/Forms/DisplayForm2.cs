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

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = displaySettings.BackgroundColor;

            addSongListToMenu(set.songList);

            loadForm();
        }

        private void addSongListToMenu(List<Song> list)
        {
            foreach (var song in list)
            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Text = song.title;
                item.Click += item_Click;
                songListToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            comSongDisplay1.changeToSong(item.Text);
        }

        public DisplayForm2(Song song, DisplayAndPrintSettings displaySettings)
        {
            InitializeComponent();
            var set = new Set();
            set.addSongToSet(song);
            set.loadAllSongs();

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = displaySettings.BackgroundColor;
            
            comSongDisplay1.LoadSet(set, displaySettings);
            songListToolStripMenuItem.Visible = false;
            loadForm();
        }

        private void loadForm()
        {
            
            comSongDisplay1.drawSong();
            this.KeyUp += DisplayForm2_KeyUp;
        }

        

        private void DisplayForm2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Oemplus)
                comSongDisplay1.increaseKey();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.OemMinus)
                comSongDisplay1.decreaseKey();
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Oemplus)
                comSongDisplay1.increaseKey();
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.OemMinus)
                comSongDisplay1.decreaseKey();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.W)
                comSongDisplay1.toggleWords();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                comSongDisplay1.toggleChords();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
                comSongDisplay1.toggleNotes();
            else if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
                comSongDisplay1.increaseFontSize();
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                comSongDisplay1.decreaseFontSize();
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
                comSongDisplay1.moveToNextSlideOrSong();
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                comSongDisplay1.moveToPreviousSlideOrSong();
            else if (e.KeyCode == Keys.PageUp)
                comSongDisplay1.previousSong();
            else if (e.KeyCode == Keys.PageDown)
                comSongDisplay1.nextSong();
            else if (e.KeyCode == Keys.L)
                showSongList();

            

        }

  
        private void showSongList()
        {
            songListToolStripMenuItem.ShowDropDown();
            var currentSong = comSongDisplay1.getCurrentSong();
            for (int i=0; i < songListToolStripMenuItem.DropDownItems.Count; i++)
            {
                ToolStripItem item = (ToolStripItem)songListToolStripMenuItem.DropDownItems[i];
                if (item.Text == currentSong)
                    item.Select();
            }
        }
        
        private void DisplayForm2_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.drawSong();
        }

        private void increaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.increaseFontSize();
        }

        private void decreaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.decreaseFontSize();
        }

        private void increaseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.increaseKey();
        }

        private void decreaseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.decreaseKey();
        }

        private void increaseCapoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.increaseCapo();
        }

        private void decreaseCapoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.decreaseCapo();
        }

        private void showHideChordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.toggleChords();
        }

        private void showHideLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.toggleWords();
        }

        private void showHideNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.toggleNotes();
        }

        private void nextSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.nextSong();
        }

        private void previousSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.previousSong();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.moveToNextSlideOrSong();
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comSongDisplay1.moveToPreviousSlideOrSong();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  
    }
}
