using System;
using System.Windows.Forms;
using OpenChords.Entities;
using OpenChords.Functions;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenChords.Forms
{
	public partial class EditorForm : Form
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private OpenFileDialog openFileDialogSelectOpenSongBackground = new OpenFileDialog()
            {
                 DefaultExt = "jpeg | *.jpg, *.jpeg"
            };
		private void TextFieldTextChanged(object sender, EventArgs e)
		{
            if (!disableChangesMadeDetection)
			    changesMade = true;
		}

        private void comboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!disableChangesMadeDetection)
                changesMade = true;
        }
		
		private List<string> songList = new  List<string>(); 
		
		public EditorForm()
		{
			InitializeComponent();
			
			this.DoubleBuffered=true;
			SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);

            this.Icon = Properties.Resources.guitar;
            this.newToolStripMenuItem.Image = Properties.Resources.New;
            this.exploreToolStripMenuItem.Image = Properties.Resources.Explore;
            this.refreshToolStripMenuItem.Image = Properties.Resources.Reload;
            this.saveToolStripMenuItem.Image = Properties.Resources.Save;
            this.deleteToolStripMenuItem.Image = Properties.Resources.Delete;
            this.revertToSavedToolStripMenuItem.Image = Properties.Resources.Revert;
            this.exitToolStripMenuItem.Image = Properties.Resources.Exit;
            this.FixFormatingtoolStripMenuItem.Image = Properties.Resources.Repair;
            this.preferencesToolStripMenuItem.Image = Properties.Resources.Settings;
            this.presentSongToolStripMenuItem.Image = Properties.Resources.PresentSong;
            this.presentSetToolStripMenuItem.Image = Properties.Resources.Present;
            this.openSongToolStripMenuItem.Image = Properties.Resources.OpenSong;
            this.pdfToolStripMenuItem.Image = Properties.Resources.pdf;
            this.fileSyncUtilityToolStripMenuItem.Image = Properties.Resources.Sync;
            this.manualToolStripMenuItem.Image = Properties.Resources.Help;
            this.aboutOpenChordsToolStripMenuItem.Image = Properties.Resources.About;
            this.imgUp.Image = Properties.Resources.Up;
            this.imgDown.Image = Properties.Resources.Down;
            this.BtnPresentSet.Image = Properties.Resources.Present;
            this.BtnClear.Image = Properties.Resources.Delete;
            this.BtnSearch.Image = Properties.Resources.Search;
            this.BtnCapoDown.Image = Properties.Resources.Down;
            this.BtnCapoUp.Image = Properties.Resources.Up;
            this.BtnTransposeDown.Image = Properties.Resources.Down;
            this.BtnTransposeUp.Image = Properties.Resources.Up;
            this.BtnFixFormatting.Image = Properties.Resources.Repair;
            this.btnPlay.Image = Properties.Resources.Play;
            this.btnPresentSong.Image = Properties.Resources.PresentSong;

            cmbSig.DataSource = Song.TimeSignatureOptions();
            cmbTempo.DataSource = Song.TempoOptions();

            refreshAll();
			
			
			
		}
		

		
		void ListSongsSelectedIndexChanged(object sender, EventArgs e)
		{
			if (changesMade == true)
				confirmSave();
			
			int index = listSongs.SelectedIndex;
			if (index >= 0 && index < listSongs.Items.Count)
			{
				string songName = listSongs.Items[index].ToString();
				currentSong = Song.loadSong(songName);
			}
			
			updateTextFieldsInGui();



			if (currentSong.isMp3Available())
				btnPlay.Visible = true;
			else
                btnPlay.Visible = false;
			
			changesMade = false;

		}
		
		void BtnSaveClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			save();
		}
		
		void BtnExplorerClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            string filename = String.Format("\"{0}\"", OpenChords.Settings.ExtAppsAndDir.songsFolder + currentSong.title);
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;
			
			if (string.IsNullOrEmpty(fileManager))
				System.Diagnostics.Process.Start("Explorer", "/select, " + filename);
			else
                System.Diagnostics.Process.Start(fileManager, OpenChords.Settings.ExtAppsAndDir.songsFolder);
			    
		}
		
		void BtnNewClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            newSong();
		}

        private void newSong()
        {
            currentSong = new Song();
            updateTextFieldsInGui();


            changesMade = false;

            txtTitle.Focus();
        }
		
		void BtnRevertClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			currentSong.revertToSaved();
			updateTextFieldsInGui();



			changesMade = false;
		}
		
		void BtnSearchClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			search();
		}
		
		void TxtSearchClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			txtSearch.SelectAll();
		}
		
		void TxtSearchEnterPressed(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.ToString() == "Return")
				search();
			else if (e.KeyCode.ToString() == "Down")
			{
				listSongs.Focus();
				if (listSongs.Items.Count > 0)
					listSongs.SelectedIndex = 0;
			}
			else if (e.KeyCode.ToString() == "Up")
			{
				listSongs.Focus();
				if ((listSongs.Items.Count > 0))
					listSongs.SelectedIndex = listSongs.Items.Count - 1;
			}
			
			
		}
		
		void txtSearchTextChanged(object sender, EventArgs e)
		{
			fillSongList();	
			
		}
		
		void exportCurrentSongToA4PDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.CurrentSong, DisplayAndPrintSettingsType.PrintSettings);
		}

        private void exportCurrentSetToA4PDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.CurrentSet, DisplayAndPrintSettingsType.PrintSettings);
        }

        
        private void exportAllSongsToA4PDFToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.AllSongs, DisplayAndPrintSettingsType.PrintSettings);
        }

        private void exportCurrentSongToTabletPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.CurrentSong, DisplayAndPrintSettingsType.TabletSettings);
        }

        
        private void exportCurrentSetToTabletPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.CurrentSet, DisplayAndPrintSettingsType.TabletSettings);
        }

       
        private void exportAllSongsToTabletPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportToPdf(OpenChords.Config.Enumerations.DocumentType.AllSongs, DisplayAndPrintSettingsType.TabletSettings);
        }

   
        


        void exportToPdf(OpenChords.Config.Enumerations.DocumentType doc, DisplayAndPrintSettingsType printSettings)
        {
            //get the filemanager for the filesystem
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;
            string filename = null;

            //get the filename
            if (doc == Config.Enumerations.DocumentType.CurrentSong)
                filename = Export.ExportToPdf.exportSong(currentSong, printSettings);
            else if (doc == Config.Enumerations.DocumentType.CurrentSet)
                filename = Export.ExportToPdf.exportSet(currentSet, printSettings);
            else
            {
                var songList = Song.listOfAllSongs();
                foreach (var stringSong in songList)
                {
                    var song = Song.loadSong(stringSong);
                    Export.ExportToPdf.exportSong(song, printSettings);
                }


            }
            if (!string.IsNullOrEmpty(filename))
            {
                filename = String.Format("\"{0}\"", OpenChords.Settings.ExtAppsAndDir.printFolder + filename);

                //try run the file with the default application
                if (string.IsNullOrEmpty(fileManager))
                    System.Diagnostics.Process.Start(filename);
                else
                    System.Diagnostics.Process.Start(fileManager, filename);
            }

        }
        
        void  BtndeleteClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			confirmDelete();
		}

        
		
		void BtnLaunchDropBox()
		{
            if (IO.FileFolderFunctions.isFilePresent(OpenChords.Settings.ExtAppsAndDir.FileSyncUtility))
                System.Diagnostics.Process.Start(OpenChords.Settings.ExtAppsAndDir.FileSyncUtility);
			
			
		}
		
        //void BtnPrintSettingsClick(object sender, EventArgs e)
        //{
        //    //System.Diagnostics.Process.Start("notepad", ExtAppsAndDir.printSettingsFilename);
        //    DisplaySettingsForm temp = new DisplaySettingsForm(currentSong, printSettings);
			
        //    temp.ShowDialog();
        //}
		
        //void BtnDisplaySettingsClick(object sender, EventArgs e)
        //{
        //    new DisplaySettingsForm(currentSong, displaySettings).ShowDialog();
        //}
		
		void BtnPresentSongClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            presentSong();
		}

        private void presentSong()
        {
            confirmSave();
            updateSelectedSong();
            var displaySettings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings);
            saveState();
            new Forms.DisplayForm2(currentSong, displaySettings).ShowDialog();
        }

		void presentSet()
		{
            confirmSave();
            var displaySettings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings);
			saveState();
			if (currentSet.songNames.Count > 0)
			{
				currentSet = currentSet.saveAndreloadSet();
				new Forms.DisplayForm2(currentSet, displaySettings).ShowDialog();
			
				
			}
		}
		
		void BtnPresentSetClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			presentSet ();
		}
		
		void BtnTransposeUpClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			updateSelectedSong();
			currentSong.transposeKeyUp();
			updateTextFieldsInGui();
			txtLyrics.Focus();
			txtLyrics.Select(0,0);
		}
		
		void BtnTransposeDownClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			updateSelectedSong();
			currentSong.transposeKeyDown();
			updateTextFieldsInGui();
			txtLyrics.Focus();
			txtLyrics.Select(0,0);
		}
		
		void BtnCapoUpClick(object sender, System.EventArgs e)
        {
            logger.Info("Clicked");
			updateSelectedSong();
			currentSong.capoUp();
			updateTextFieldsInGui();
			txtLyrics.Focus();
			txtLyrics.Select(0,0);
		}
		
		void BtnCapoDownClick(object sender, System.EventArgs e)
        {
            logger.Info("Clicked");
			updateSelectedSong();
			currentSong.capoDown();
			updateTextFieldsInGui();
			txtLyrics.Focus();
			txtLyrics.Select(0,0);
		}

		
		private void breakLine()
		{
			updateSelectedSong();
			int caret = txtLyrics.SelectionStart;
			int newCaret =  Functions.SongProcessor.BreakSongLine(txtLyrics, caret);
            currentSong.lyrics = txtLyrics.Text;
			updateTextFieldsInGui();
			
			txtLyrics.SelectionStart = newCaret;
			txtLyrics.ScrollToCaret();

			
		}
		
		void TextFieldKeyUp(object sender, KeyEventArgs e)
		{
            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            //{
            //    TextBox source = (TextBox)sender;
            //    source.SelectAll();
            //}
			if (e.KeyCode == Keys.Pause)
			{
				breakLine();
			}

            //user wants to undo
            var carot = txtLyrics.SelectionStart;
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z)
            {
                // Skip to the valid Undo Items
                while (txtLyrics.UndoActionName != "Typing" && txtLyrics.CanUndo)
                {
                    txtLyrics.Undo();
                }

                // Undo Typing
                if (txtLyrics.CanUndo) txtLyrics.Undo();
                txtLyrics.SelectionStart = carot;

                formatLyrics();
            }

			//moving save to a global hotkey
			//else if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlS")
			//{
			//	save();
			//}
			
			
			//MessageBox.Show(sender.ToString() + " " + e.Modifiers.ToString()+ " " + e.KeyCode.ToString());
		}
		
		
		
		
		//options for adding a song to the set
		void ListSongsDoubleClick(object sender, System.EventArgs e)
        {
            logger.Info("Clicked");
			currentSet.addSongToSet(currentSong);
			updateSetsListBox();
			updateSetNumber();
		}
		
		void ListSongsKeyUp(object sender, KeyEventArgs e)
		{
            

            if (e.KeyCode == Keys.Enter)
			{
                var listBox = (ListBox)sender;
                var selectedSongName = (string)listBox.SelectedItem;
                var selectedSong = Song.loadSong(selectedSongName);

                currentSet.addSongToSet(selectedSong);
				updateSetsListBox();
				updateSetNumber();
			}
            //dont capture when control is pressed (this is useed for hotkeys)
            else if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Tab)
            {
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                var listBox = (ListBox)sender;
                var selectedSongName = (string)listBox.SelectedItem;
                var selectedSong = Song.loadSong(selectedSongName);

                currentSet.removeSongFromSet(selectedSong);
                updateSetsListBox();
                updateSetNumber();
            }
            //anything else that is typed need to get typed into the search box
            else
            {
                startSongSearch(e);
            }	
		}

        private void startSongSearch(KeyEventArgs e)
        {
            txtSearch.Focus();
            if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)) 
            {
                txtSearch.Text = e.KeyCode.ToString();
                txtSearch.Select(1, 1);
            }
            else
                txtSearch.Text = "";
        }
		
		void BtnAddClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			currentSet.addSongToSet(currentSong);
			updateSetsListBox();
			updateSetNumber();
		}
		
		//options for deleting a song
		void ListSetKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.ToString() == "Delete")
			{
                var list = (ListBox)sender;
                var index = list.SelectedIndex;
				deleteSetItem(index);
			}
			
			else if (e.KeyCode.ToString() == Keys.OemMinus.ToString())
			{
                moveSetItemUp();
			}
			
			else if (e.KeyCode.ToString() == Keys.Oemplus.ToString())
			{
                moveSetItemDown(); 
			}
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Tab)
            {
                return;
            }
            else
            {
                startSongSearch(e);
            }
		}

        private void moveSetItemUp()
        {
            currentSet.moveSongUp();
            updateSetsListBox();
        }

        private void moveSetItemDown()
        {
            currentSet.moveSongDown();
            updateSetsListBox();
        }


		
		//deletes the selected song from the set
		void deleteSetItem (int index)
		{
			currentSet.removeSongFromSet(index);
			updateSetsListBox();
			updateSetNumber();
		}
	
		
		//options for clearing the song set list
		void BtnClearClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			String text = "Clear the Set?";
			DialogResult result =
				MessageBox.Show(this,
				                text,
				                "Clear?",
				                MessageBoxButtons.YesNo,
				                MessageBoxIcon.Question,
				                MessageBoxDefaultButton.Button1);

			if (result == DialogResult.Yes)
			{
				currentSet.clearSongSet();
				updateSetsListBox();
				updateSetNumber();
			}
		}
		
		
		void ListSetSelectedIndexChanged(object sender, EventArgs e)
		{
			if (changesMade == true)
				confirmSave();
			
			int index = listSet.SelectedIndex;
            if (index >= 0 && index < listSet.Items.Count)
			{
				string songName = listSet.Items[index].ToString();
				currentSong = Song.loadSong(songName);
				currentSet.indexOfCurrentSong = index;
			}
			
			updateTextFieldsInGui();
            //if (currentSong.isMp3Available())
            //    btnPlay.Enabled = true;
            //else
            //    btnPlay.Enabled = false;
			

			changesMade = false;
		}
		
		
		/// <summary>
		/// process key presses and handle the hot keys
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.F11:
                    presentSong();
					return true;
				case Keys.F12:
					presentSet();
					return true;
                case Keys.Control | Keys.F12:
                    exportToPdf(Config.Enumerations.DocumentType.CurrentSet, DisplayAndPrintSettingsType.TabletSettings);
                    return true;
                case Keys.F5:
                    refreshAll();
                    return true;
				case Keys.Control | Keys.F:
                    fixFormatting();
					return true;
				case Keys.Control | Keys.S:
					save();
					return true;
				case Keys.Control | Keys.R:
					randomizer();
					return true;
				case Keys.Control | Keys.P:
                    exportToPdf(Config.Enumerations.DocumentType.CurrentSong, DisplayAndPrintSettingsType.PrintSettings);
					return true;
				case Keys.Control | Keys.Alt | Keys.Shift | Keys.F:
					fixUpScript();
					return true;
				case Keys.Control | Keys.O:
					exportSetToOpenSong();
					return true;
                case Keys.Control | Keys.N:
                    newSong();
                    return true;
					
										
					
					
					
					
					
			}
			return false;
		}

        private void fixFormatting()
        {
            updateSelectedSong();
            currentSong.fixFormatting();
            updateTextFieldsInGui();
        }

        private void fixNoteAndLyricsOrdering()
        {
            updateSelectedSong();
            currentSong.fixNoteOrdering();
            currentSong.fixLyricsOrdering();
            updateTextFieldsInGui();
        }
		
		void fixUpScript()
		{
			for (int i = 0; i < listSongs.Items.Count; i++)
			{
				listSongs.SelectedIndex = i;
                currentSong.fixNoteOrdering();
                currentSong.fixLyricsOrdering();
				currentSong.saveSong();
				
			}
			
		}
		
		
		
		void EditorFormKeyUp(object sender, KeyEventArgs e)
		{
			
		}


		void BtnMoveUpClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            moveSetItemUp();
		}
		
		void BtnMoveDownClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            moveSetItemDown();
			
		}
		
		void BtnFixFormattingClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            fixFormatting();
		}
		
		void CmboSetsSelectedIndexChanged(object sender, EventArgs e)
		{
			currentSet.saveSet();
			int index = cmboSets.SelectedIndex;
			if (index >= 0 && index < cmboSets.Items.Count)
			{
				currentSet = Set.loadSet(cmboSets.Items[index].ToString());
				updateSetsListBox();
				updateSetNumber();
			}
		}
		
		void EditorFormLoad(object sender, EventArgs e)
		{
            this.Icon = Properties.Resources.guitar;
            addToolTips();

            setSplitterDistances();

		}

        private void setSplitterDistances()
        {
            splitMainForm.SplitterDistance = 250;
            splitSetsAndSongs.SplitterDistance = 155;
            splitLyricsAndNotes.SplitterDistance = splitLyricsAndNotes.Width / 100 * 80;
        }

        private void addToolTips()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.UseAnimation = true;
            toolTip1.IsBalloon = true;
            toolTip1.InitialDelay = 500;
            
            ToolTip toolTip2 = new ToolTip();
            toolTip2.ShowAlways = true;
            toolTip2.UseAnimation = true;
            toolTip2.IsBalloon = true;
            toolTip2.InitialDelay = 1500;

            //main gui
            toolTip1.SetToolTip(BtnClear, "Clears all items in the set");
            toolTip1.SetToolTip(BtnPresentSet, "Presents all songs in the set to full screen display");
            toolTip1.SetToolTip(btnPresentSong, "Presents the selected song to full screen display");
            toolTip1.SetToolTip(btnPlay, "Play attached media file");
            toolTip1.SetToolTip(BtnFixFormatting, "Attempts to fix the formatting of the lyrics of the song and its notes");
            toolTip1.SetToolTip(BtnTransposeUp, "Transpose song up by a semitone");
            toolTip1.SetToolTip(BtnTransposeDown, "Transpose song down by a semitone");
            toolTip1.SetToolTip(BtnCapoUp, "Increase capo by one");
            toolTip1.SetToolTip(BtnCapoDown, "Decrease capo by one");
            toolTip1.SetToolTip(BtnSearch, "Filter song list");
            toolTip1.SetToolTip(imgUp, "Move selected song up");
            toolTip1.SetToolTip(imgDown, "Move selected song down");

            //menus
            newToolStripMenuItem.ToolTipText = "Create a new song";
            exploreToolStripMenuItem.ToolTipText = "Open song list in windows explorer";
            refreshToolStripMenuItem.ToolTipText = "Refresh everything";
            saveToolStripMenuItem.ToolTipText = "Save the currently selected song";
            deleteToolStripMenuItem.ToolTipText = "Delete the currently selected song";
            revertToSavedToolStripMenuItem.ToolTipText = "Reverted the currently selected song to its last saved state";
            exitToolStripMenuItem.ToolTipText = "Exit OpenChords";
            exportCurrentSongToA4PDFToolStripMenuItem.ToolTipText = "Export currently selected song to a PDF file";

            FixFormatingtoolStripMenuItem.ToolTipText = "Attempts to fix the formatting of the lyrics of the song and its notes";

            presentSetToolStripMenuItem.ToolTipText = "Presents all songs in the set to full screen display";
            presentSongToolStripMenuItem.ToolTipText = "Presents the selected song to full screen display";


        }
		
		
		void EditorForm_FormClosing(object sender, EventArgs e)
		{
            exitApp();
		}

        private void exitApp()
        {
            currentSet.saveSet();
            saveState();
            Application.ExitThread();
           
        }
		
		private void exportSetToOpenSong()
		{
			currentSet.saveSet();
			saveState();

            currentSet.exportSetAndSongsToOpenSong();
           
			
		}



		void BtnBreakClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
			breakLine();
		}
		
		void BtnPlayClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            if (currentSong.isMp3Available())
            {
                string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;
			    string filename = "\"" + currentSong.getMp3Filename() + "\"";
			    if (string.IsNullOrEmpty(fileManager))
					System.Diagnostics.Process.Start(filename);
				else
					System.Diagnostics.Process.Start(fileManager, filename);
			    this.Focus();
            }
		}
		
		void updateSetNumber()
		{
			//grpSet.Text = "Set [" + currentSet.getSongSetSize() + "]";
			lblSongCount.Text = currentSet.getSongSetSize().ToString();
			
		}
		
		
        
        
        
        void ManualToolStripMenuItemClick(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            string fileManager = OpenChords.Settings.ExtAppsAndDir.fileManager;
            string filename = OpenChords.Settings.ExtAppsAndDir.manual;

            if (string.IsNullOrEmpty(filename))
                return;

            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(filename);
            else
                System.Diagnostics.Process.Start(fileManager, filename);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void txtLyrics_Leave(object sender, EventArgs e)
        {
            if (changesMade)
            {
                disableChangesMadeDetection = true;
                formatLyrics();
                disableChangesMadeDetection = false;
            }
        }

        private void txtNotes_Leave(object sender, EventArgs e)
        {
            if (changesMade)
            {
                disableChangesMadeDetection = true;
                formatNotes();
                disableChangesMadeDetection = false;
            }
        }
       

        private void formatLyrics()
        {
			RichTextBox rtf = new RichTextBox();
            rtf.Font = txtLyrics.Font;

            string[] songText;
            if (String.IsNullOrEmpty(txtLyrics.Text))
                songText = Functions.SongProcessor.multiLineToStringArray(currentSong.lyrics, false);
            else
                songText = Functions.SongProcessor.multiLineToStringArray(txtLyrics.Text, false);

            var carot = txtLyrics.SelectionStart;			
            int lineCounter = 0;
            foreach (string line in songText)
            {

                if (line.Length > 0)
                {
                    string debug = rtf.SelectedText;
                    char firstChar = line[0];
                    if (firstChar == ' ') //lyrics line
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Regular);
                    else if (firstChar == '.') //chords line
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Bold);
                    else if (firstChar == '[') //section label
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Italic);
                    else
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Regular);
                }
                rtf.AppendText(line + "\n");
                lineCounter++;
            }

            txtLyrics.Rtf = rtf.Rtf;
            //txtLyrics.Select(carot, 0);
            //txtLyrics.ScrollToCaret();
			
			
			
        }

        private void formatNotes()
        {
            RichTextBox rtf = new RichTextBox();
            rtf.Font = txtNotes.Font;

            string[] songText;
            if (String.IsNullOrEmpty(txtNotes.Text))
                songText = Functions.SongProcessor.multiLineToStringArray(currentSong.notes, false);
            else
                songText = Functions.SongProcessor.multiLineToStringArray(txtNotes.Text, false);

            var carot = txtNotes.SelectionStart;
            int lineCounter = 0;
            foreach (string line in songText)
            {

                if (line.Length > 0)
                {
                    string debug = rtf.SelectedText;
                    char firstChar = line[0];
                    if (firstChar == ' ') //notes line
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Bold);
                    else if (firstChar == '[') //section label
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Italic);
                    else
                        rtf.SelectionFont = new Font(rtf.Font, FontStyle.Regular);
                }
                rtf.AppendText(line + "\n");
                lineCounter++;
            }

            txtNotes.Rtf = rtf.Rtf;
            //txtNotes.Select(carot, 0);
            //txtNotes.ScrollToCaret();
        }

        private void txtNotes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNotes.SelectedText = " ";
            else if (e.KeyValue == 219) // '[' character
            {
                txtNotes.SelectionStart--;
                txtNotes.SelectionLength++;
                txtNotes.SelectedText = "\n \n[";
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z)
            {
                //user wants to undo
                var carot = txtNotes.SelectionStart;

                // Skip to the valid Undo Items
                while (txtNotes.UndoActionName != "Typing" && txtNotes.CanUndo)
                {
                    txtNotes.Undo();
                }

                // Undo Typing
                if (txtNotes.CanUndo) txtNotes.Undo();
                txtNotes.SelectionStart = carot;

                formatNotes();
            }

            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exitApp();
        }

        private void cmboSets_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
        }

        private void cmboSets_KeyUp(object sender, KeyEventArgs e)
        {
            var setName = cmboSets.Text;
            var setList = new List<string>(IO.FileFolderFunctions.getDirectoryListing(OpenChords.Settings.ExtAppsAndDir.setsFolder));

            var found = setList.Where(s => s.ToUpper() == setName.ToUpper()).FirstOrDefault();

            if (e.KeyCode == Keys.Enter)
            {
                
                if (!String.IsNullOrEmpty(found))
                    cmboSets.Text = found;
                else
                    confirmNewSet();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (!String.IsNullOrEmpty(found))
                    confirmDeleteSet();

            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            Form settings = new Forms.Settings();
            settings.ShowDialog();
            //reload settings
            OpenChords.Settings.ExtAppsAndDir.refreshFileAndFolderSettings();
        }

        private void FixFormatingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            fixFormatting();
        }


        private void fileSyncUtilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            System.Diagnostics.Process.Start(OpenChords.Settings.ExtAppsAndDir.FileSyncUtility);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            refreshAll();   
        }

        /// <summary>
        /// resets all items in the UI
        /// </summary>
        private void refreshAll()
        {
            fillSetCmboBox();
            
            int songIndex = listSongs.SelectedIndex;

            loadSongList();

            listSongs.SelectedIndex = songIndex;

            txtSearch.Text = "";

            updateSetNumber();

            loadState();

            loadFonts(this.Controls);

            //disable the export to opensong if opensong is not available
            if (!IO.FileFolderFunctions.isFilePresent(OpenChords.Settings.ExtAppsAndDir.openSongApp))
            {
                openSongToolStripMenuItem.Enabled = false;
                splitContainerNotesImage.Panel2Collapsed = true;
            }
            //disable sync utility if it isn't pointing to a real file
            if (!IO.FileFolderFunctions.isFilePresent(OpenChords.Settings.ExtAppsAndDir.FileSyncUtility))
                fileSyncUtilityToolStripMenuItem.Enabled = false;

            if (currentSong.isMp3Available())
                btnPlay.Visible = true;
            else
                btnPlay.Visible = false;

        }

        private void aboutOpenChordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            (new OpenChordsAboutBox()).ShowDialog();
        }

      



        private void rdoFlats_CheckedChanged(object sender, EventArgs e)
        {
            updateSelectedSong();
            currentSong.transposeKeyUp();
            currentSong.transposeKeyDown();
            updateTextFieldsInGui();

            TextFieldTextChanged(sender, e);
        }



        void BtnLaunchOpenSongClick(object sender, EventArgs e)
        {

        }
		

        private void exportCurrentSetToOpenSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            exportSetToOpenSong();
            Export.ExportToOpenSong.launchOpenSong();
        }

        private void exportAllSongsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            Export.ExportToOpenSong.exportAllSongsToOpenSong();
            Export.ExportToOpenSong.launchOpenSong();
        }

        private void exportAllSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            Export.ExportToOpenSong.exportAllSetsToOpenSong();
            Export.ExportToOpenSong.launchOpenSong();
        }

        private void launchOpenSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Clicked");
            Export.ExportToOpenSong.launchOpenSong();
        }

        private void picBackgroundImage_Click(object sender, EventArgs e)
        {
            openFileDialogSelectOpenSongBackground.InitialDirectory = OpenChords.Settings.ExtAppsAndDir.opensongBackgroundsFolder;
            var result = openFileDialogSelectOpenSongBackground.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var filename = openFileDialogSelectOpenSongBackground.FileName.Replace(OpenChords.Settings.ExtAppsAndDir.opensongBackgroundsFolder, "");
                currentSong.OpenSongImageFileName = filename;
                picBackgroundImage.Image = currentSong.getSongImage();
                changesMade = true;
            }
        }

        private void timerOrderChanged_Tick(object sender, EventArgs e)
        {
            timerOrderChanged.Enabled = false;
            txtOrder.SelectionLength = 0;
            var oldTextPosition = txtOrder.SelectionStart;
            //only reorder if the order changes
            if (txtOrder.Text != currentSong.presentation)
                fixNoteAndLyricsOrdering();

            txtOrder.SelectionStart = oldTextPosition;

        }

        private void txtOrder_TextChanged(object sender, EventArgs e)
        {
            timerOrderChanged.Stop();
            timerOrderChanged.Start();
        }

    




  

        


     

	}
}
