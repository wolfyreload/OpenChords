/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 08:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using OpenChords.Entities;
using OpenChords.Functions;



namespace OpenChords.Forms
{
	
	
	
	/// <summary>
	/// Description of DisplayForm.
	/// </summary>
	public partial class DisplayForm : Form
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool songChanged { get; set; }

        public void saveSong()
        {
            if (songChanged)
                currentSong.saveSong();
            songChanged = false;
        }

	    public DisplayForm(Song song, DisplayAndPrintSettings displaySettings)
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			currentSet = new Set(); //if this is not here it crashes
			currentSong = song;
            currentSet.addSongToSet(song);
            currentSet.reloadSet();

            setupDisplayWindow(displaySettings);
			
			//initializeFonts();
			//TopMost = true;
		}
		
		
		
		public DisplayForm(Set songSet, DisplayAndPrintSettings displaySettings)
		{
			InitializeComponent();

			currentSet = songSet;
			//currentSong = new Song();

            setupDisplayWindow(displaySettings);
	
			
			//initializeFonts();
		}

        private void setupDisplayWindow(DisplayAndPrintSettings displaySettings)
        {
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            if (!IO.FileFolderFunctions.IsRunningInVisualStudio())
                this.TopMost = true;
            this.displaySettings = displaySettings;
            this.BackColor = displaySettings.BackgroundColor;
            this.ForeColor = displaySettings.LyricsFormat.FontColor;
            listSongs.BackColor = displaySettings.BackgroundColor;
            listSongs.ForeColor = displaySettings.LyricsFormat.FontColor;

            //pnlGeneralNotes.Visible = displaySettings.ShowGeneralNotes.Value;


            this.Invalidate();
        }
		
		
		
		
		void DisplayFormPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			graphicsObj = this.CreateGraphics();
            graphicsObj.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			
			
			if (setIndex >=0 && setIndex < currentSet.songSetSize)
				//currentSong.loadSong(currentSet.songNames[setIndex]);
				currentSong = currentSet.songList[setIndex];
			
			
			
			//fix width and height
			//displaySettings.pageHeight = this.Height - 10;
			//displaySettings.pageWidth = this.Width;
			
			//drawSongBox();   //debug
			initializeFonts();
			
			
			
			drawSong();
			
		}
		
		void DisplayFormKeyUp(object sender, KeyEventArgs e)
		{
            
            if (e.KeyCode.ToString() == "Escape")
                this.Close();
			if (e.KeyCode.ToString() == "Up" || e.KeyCode.ToString() == "Left")
			{
				gotoPreviousPage();
			}
			else if (e.KeyCode.ToString() == "Down" || e.KeyCode.ToString() == "Right" || e.KeyCode.ToString() == "Space")
			{
				gotoNextPage();
			}
			else if (e.KeyCode.ToString() == "PageUp")
			{
				gotoPreviousSong();
			}
			else if (e.KeyCode.ToString() == "Next")
			{
				gotoNextSong();
			}
            else if (e.KeyCode == Keys.S)
            {
                SwitchBetweenSharpsAndFlats();
            }
			else if (e.KeyValue >= 49 && e.KeyValue <= 58) //define numbers 1-9
			{
				gotoSong(e.KeyValue - 49);
			}
			else if (e.KeyCode == Keys.L) //songs in set list
			{
                showSongList();
			}
			else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ShiftOemplus")
			{
                IncreaseCapo();
			}
			else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ShiftOemMinus")
			{
                DecreaseCapo();
			}
			else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlOemplus")
			{
                IncreaseKeyBySemiTone();
			}
			else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlOemMinus")
			{
                DecreaseKeyBySemiTone();
			}
			else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
			{
                IncreaseFontSize();
			}
			else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
			{
                DecreaseFontSize();
			}
            else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlN") // show notes
            {
                ShowHideNotes();
            }
            else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlD") //pane display
            {
                ShowHideDualScreenPanels();
            }
            //else if (e.KeyCode == Keys.G)
            //{
            //    pnlGeneralNotes.Visible = !pnlGeneralNotes.Visible;
            //}
            else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlC")
            {
                ShowHideChords();
            }
            else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlW")
            {
                ShowHideLyrics();
            }
            else if (e.KeyCode == Keys.M)
            {
                ShowHideContextMenu();
            }
            else if (e.KeyCode == Keys.F5)
            {
                RefreshCurrentSongAndSet();
            }

			
			//else if (e.Modifiers + e.KeyCode.ToString() == "Next")
			
			//MessageBox.Show(e.KeyValue.ToString());
			//MessageBox.Show(e.KeyData.ToString());
			//MessageBox.Show(e.Modifiers.ToString() + e.KeyCode.ToString());

			
		}

        /// <summary>
        /// reloads the current set and songs in the set
        /// </summary>
        private void RefreshCurrentSongAndSet()
        {
            currentSet = currentSet.reloadSet();
            //initialiseSongIndicesList();
            Invalidate();

        }

        private void ShowHideContextMenu()
        {
            OpenChordsMenuStrip.Visible = !OpenChordsMenuStrip.Visible;
        }

        private void ShowHideLyrics()
        {
            displaySettings.ShowLyrics = !displaySettings.ShowLyrics;
            Invalidate();
        }

        private void ShowHideChords()
        {
            displaySettings.ShowChords = !displaySettings.ShowChords;
            Invalidate();
        }

        private void ShowHideDualScreenPanels()
        {
            displaySettings.DualColumns = !displaySettings.DualColumns;
            initializeFonts();
            Invalidate();
        }

        private void ShowHideNotes()
        {
            displaySettings.ShowNotes = !displaySettings.ShowNotes;
            initializeFonts();
            Invalidate();
        }

        private void DecreaseFontSize()
        {
            displaySettings.desceaseSizes();
            initializeFonts();
            Invalidate();
        }

        private void IncreaseFontSize()
        {
            displaySettings.increaseSizes();
            initializeFonts();
            Invalidate();
        }

        private void DecreaseKeyBySemiTone()
        {
            currentSong.transposeKeyDown();
            songChanged = true;
            Invalidate(); //force redraw
            saveSong();
        }

        private void IncreaseKeyBySemiTone()
        {
            currentSong.transposeKeyUp();
            songChanged = true;
            Invalidate(); //force redraw
            saveSong();
        }

        private void DecreaseCapo()
        {
            currentSong.capoDown();
            songChanged = true;
            Invalidate(); //force redraw
            saveSong();
        }

        private void IncreaseCapo()
        {
            currentSong.capoUp();
            songChanged = true;
            Invalidate(); //force redraw
            saveSong();
        }

        private void SwitchBetweenSharpsAndFlats()
        {
            currentSong.PreferFlats = !currentSong.PreferFlats;
            currentSong.transposeKeyUp();
            currentSong.transposeKeyDown();
            songChanged = true;
            Invalidate();
            saveSong();
        }

        private void showSongList()
        {
            fillSongList();
            listSongs.Visible = true;
            if (listSongs.Items.Count > 0)
                listSongs.SelectedIndex = setIndex;
            listSongs.Focus();
        }
		
		
		
		
		
		void DisplayFormMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				gotoPreviousPage();
				
			}
			else if (e.Button == MouseButtons.Right)
			{
				gotoNextPage();
			}
			else if (e.Button == MouseButtons.Middle)
			{
                ShowHideContextMenu();
			}
            //else if (e.Delta > 0)
            //{
            //    IncreaseFontSize();
            //}
            //else if (e.Delta < 0)
            //{
            //    DecreaseFontSize();
            //}
            
		}
		
		void fillSongList()
		{
			listSongs.Items.Clear();
			listSongs.Items.AddRange(currentSet.songNames.ToArray());
		}
		
		
		void DisplayFormLoad(object sender, EventArgs e)
		{
			initialiseSongIndicesList();
			//currentSet.loadAllSongs();
            this.Icon = Properties.Resources.guitar;

            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DisplayFormKeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DisplayFormMouseClick);
		}
		
		void ListSongsKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.L || e.KeyCode == Keys.Escape)
			{
				listSongs.Visible = false;
				this.Focus();
				Invalidate();
			}
			else if (e.KeyCode == Keys.Return)
			{
				if (listSongs.SelectedIndex != -1)
				{
					setIndex = listSongs.SelectedIndex;
					initialiseSongIndicesList();
					listSongs.Visible = false;
					this.Focus();
					Invalidate();
				}
				
			}


        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void increaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncreaseFontSize();
        }

        private void decreaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DecreaseFontSize();
        }

        private void increaseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncreaseKeyBySemiTone();
        }

        private void decreaseKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DecreaseKeyBySemiTone();
        }

        private void increaseCapoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncreaseCapo();
        }

        private void decreaseCapoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DecreaseCapo();
        }

        private void showHideChordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideChords();
        }

        private void showHideLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideLyrics();
        }

        private void showHideNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideNotes();
        }

        private void showHideSongListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSongList();
        }

        private void showHideDualPanelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHideDualScreenPanels();
        }

        private void nextSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotoNextSong();
        }

        private void previousSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotoPreviousSong();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotoNextPage();
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotoPreviousPage();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshCurrentSongAndSet();
        }

        private void OpenChordsMenuStrip_Opened(object sender, EventArgs e)
        {
            OpenChordsMenuStrip.Items[0].Select();
        }

	}
}
