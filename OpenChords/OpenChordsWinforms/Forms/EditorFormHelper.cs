/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 12/3/2010
 * Time: 4:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Linq;

using OpenChords.Entities;
using OpenChords.Functions;

using System.Drawing;
using OpenChords.Config;

namespace OpenChords.Forms
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	partial class EditorForm
	{
		private Entities.Song currentSong = new Song();
		private Set currentSet = new Set();
		private bool changesMade = false;
        private bool disableChangesMadeDetection = false;
		
		//private DisplayAndPrintSettings printSettings;
		//private DisplayAndPrintSettings displaySettings;
        //private DisplayAndPrintSettings tabletSettings;
		
		/// <summary>
		/// updates the text fields in the GUI
		/// </summary>
		private void updateTextFieldsInGui()
		{
			txtTitle.Text = currentSong.title;
			txtAuthor.Text = currentSong.author;
			txtKey.Text = currentSong.key;
			txtCapo.Text = currentSong.Capo.ToString();
			txtOrder.Text = currentSong.presentation;
			
			txtLyrics.Text = currentSong.lyrics;
			txtNotes.Text = currentSong.notes;
            txtRef.Text = currentSong.hymn_number;
            txtccli.Text = currentSong.ccli;
            cmbTempo.Text = currentSong.tempo;
            cmbSig.Text = currentSong.time_sig;
		    if (currentSong.PreferFlats)
		        rdoFlats.Checked = true;
		    else
		        rdoSharps.Checked = true;

            getBackgroundImage();

            formatLyrics();
            formatNotes();
		}

        private void getBackgroundImage()
        {
            picBackgroundImage.Image = currentSong.getSongImage();
        }
		
		/// <summary>
		/// updates the sets list box
		/// </summary>
		private void updateSetsListBox()
		{
			listSet.Items.Clear();
			listSet.Items.AddRange(currentSet.songNames.ToArray());
			int index = currentSet.indexOfCurrentSong;
			if (index > -1 && index < currentSet.songSetSize)
				listSet.SelectedIndex = currentSet.indexOfCurrentSong;
		}
		
		/// <summary>
		/// Adds a random song to the set
		/// </summary>
		private void randomizer()
		{
			Random rand = new Random();
			int randomSongIndex = 0;
			string songName = "";
			int counter = 0;

            randomSongIndex = rand.Next(0, listSongs.Items.Count - 1);
		    listSongs.SelectedIndex = randomSongIndex;
            
		}
		
		
		/// <summary>
		/// updates the fields in the selected song using the text fields in the GUI
		/// </summary>
		private void updateSelectedSong()
		{
			currentSong.title = txtTitle.Text;
			currentSong.author = txtAuthor.Text;
			currentSong.key = txtKey.Text;
			currentSong.Capo = int.Parse(txtCapo.Text);
			currentSong.presentation = txtOrder.Text;
			
			currentSong.lyrics = txtLyrics.Text;
			currentSong.notes = txtNotes.Text;
            currentSong.hymn_number = txtRef.Text;
            currentSong.ccli = txtccli.Text;
            currentSong.tempo = cmbTempo.Text;
            currentSong.time_sig = cmbSig.Text;


		    if (rdoFlats.Checked)
		        currentSong.PreferFlats = true;
		    else
		        currentSong.PreferFlats = false;

		}
		
		private void fillSongList()
		{
			listSongs.Items.Clear();
			
			var itemsToShow = songList.AsQueryable();
			
			if (!string.IsNullOrEmpty(txtSearch.Text))
				itemsToShow = itemsToShow.Where(s => s.Replace(",","").ToUpper().Contains(txtSearch.Text.ToUpper()));
			
			listSongs.Items.AddRange(itemsToShow.ToArray());
		}
		
		private void loadSongList()
		{
			
			songList = IO.FileFolderFunctions.getDirectoryListingAsList(OpenChords.Settings.ExtAppsAndDir.songsFolder);
			fillSongList();
			
		}
		
		private void fillSetCmboBox()
		{
			cmboSets.Items.Clear();
			string[] setList =
                IO.FileFolderFunctions.getDirectoryListing(OpenChords.Settings.ExtAppsAndDir.setsFolder);
			cmboSets.Items.AddRange(setList);
		}
		
		private void loadState()
		{
			//load selected person from previous state
			try
			{
			string saveState = IO.SettingsReaderWriter.readSessionState();
			// FIXME:
			saveState = saveState.Replace("\r","");
			saveState = saveState.Replace("\n","");
			int index = cmboSets.Items.IndexOf(saveState);
			cmboSets.SelectedIndex = index;
			}
			catch (Exception ex)
			{
                logger.Error("Error loading state", ex);
			}
		}
		
		private void saveState()
		{
			try
			{
				IO.SettingsReaderWriter.writeSessionState(cmboSets.SelectedItem.ToString());	
			}
			catch (Exception ex)
			{
                logger.Error("Error saving state", ex);
				
				
			}
				
			
		}
		
		private void confirmSave()
		{
			String title = currentSong.title;

			if (changesMade == true && title != "")
			{
				String text = "Save the song \"" + currentSong.title + "\"?";
				DialogResult result =
					MessageBox.Show(this,
					                text,
					                "Save?",
					                MessageBoxButtons.YesNo,
					                MessageBoxIcon.Question,
					                MessageBoxDefaultButton.Button1);

				if (result == DialogResult.Yes)
				{
					save();
				}
			}
		}
		
		private void confirmDelete()
		{
			String title = currentSong.title;

			if (title != "")
			{
				String text = "Delete the song \"" + currentSong.title + "\"?";
				DialogResult result =
					MessageBox.Show(this,
					                text,
					                "Delete?",
					                MessageBoxButtons.YesNo,
					                MessageBoxIcon.Warning,
					                MessageBoxDefaultButton.Button1);

				if (result == DialogResult.Yes)
				{
					currentSong.deleteSong();
					loadSongList();
				}
			}
			
		}

        private void confirmNewSet()
        {
            String title = cmboSets.Text;

            if (title != "")
            {
                
                title = title.Substring(0,1).ToUpper() + title.Substring(1).ToLower();
                String text = String.Format("Create a new set called \"{0}\"", title);
                DialogResult result =
                    MessageBox.Show(this,
                                    text,
                                    "New set?",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    //create a blank file
                    IO.FileReaderWriter.writeToFile(OpenChords.Settings.ExtAppsAndDir.setsFolder + title, "");

                    fillSetCmboBox();

                    cmboSets.SelectedValue = title;
                }
            }

        }

        private void confirmDeleteSet()
        {
            String title = cmboSets.Text;

            if (title != "")
            {
                title = title.Substring(0, 1).ToUpper() + title.Substring(1).ToLower();
                String text = String.Format("Delete set called \"{0}\"", title);
                DialogResult result =
                    MessageBox.Show(this,
                                    text,
                                    "Delete set?",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    //create a blank file
                    System.IO.File.Delete(OpenChords.Settings.ExtAppsAndDir.setsFolder + title);

                    fillSetCmboBox();
                  
                    if (cmboSets.Items.Count > 0)
                        cmboSets.SelectedIndex = 0;
                }
            }

        }
		
        //void loadSettingsFiles()
        //{
        //    if (IO.FileFolderFunctions.isFilePresent(ExtAppsAndDir.displaySettingsFileName))
        //        displaySettings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings);
        //    else
        //        displaySettings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.DisplaySettings);
			
        //    if (IO.FileFolderFunctions.isFilePresent(ExtAppsAndDir.printSettingsFilename))
        //        printSettings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings);
        //    else
        //        printSettings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.PrintSettings);

        //    if (IO.FileFolderFunctions.isFilePresent(ExtAppsAndDir.tabletSettingsFilename))
        //        tabletSettings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings);
        //    else
        //        tabletSettings = new DisplayAndPrintSettings(DisplayAndPrintSettingsType.TabletSettings);


        //}
		
		void loadFonts(Control.ControlCollection controls)
		{
            foreach (Control ctrl in controls)
            {
                //if (ctrl is Button)
                //    ctrl.Font = new System.Drawing.Font(EditorFont.buttonFontName,
                //                                        EditorFont.buttonFontSize,
                //                                        EditorFont.buttonFontStyle);
                if (ctrl is TextBox && ctrl.Name != "txtKey" && ctrl.Name != "txtCapo")
                {
                    TextBox box = (TextBox)ctrl;
                    if (box.Multiline)
                        box.Font = new System.Drawing.Font(EditorFont.textboxFontName,
                                                           EditorFont.textboxFontSize,
                                                           EditorFont.textboxFontStyle);
                    else
                        box.Font = new System.Drawing.Font(EditorFont.textfieldFontName,
                                                           EditorFont.textfieldFontSize,
                                                           EditorFont.textfieldFontStyle);
                }
                else if (ctrl is ListBox)
                    ctrl.Font = new System.Drawing.Font(EditorFont.listboxFontName,
                                                        EditorFont.listboxFontSize,
                                                        EditorFont.listboxFontStyle);
                //else if (ctrl is Label)
                //    ctrl.Font = new System.Drawing.Font(EditorFont.labelFontName,
                //                                        EditorFont.labelFontSize,
                //                                        EditorFont.listboxFontStyle);
                else if (ctrl is GroupBox)
                {
                    ctrl.Font = new System.Drawing.Font(EditorFont.groupboxFontName,
                                                        EditorFont.groupboxFontSize,
                                                        EditorFont.groupboxFontStyle);
                    loadFonts(ctrl.Controls);
                }
                else if (ctrl.Controls.Count > 0)
                {
                    loadFonts(ctrl.Controls);
                }

            }

		}
		
		private void updateSetListbox()
		{
			listSet.Items.Clear();
			listSet.Items.AddRange(currentSet.songNames.ToArray());
			
		}
		
		private void save()
		{
			if (changesMade == true)
			{
				updateSelectedSong();
				currentSong.saveSong();
			}
			
			if (!listSongs.Items.Contains(currentSong.title))
				loadSongList();

			changesMade = false;
		}
		

		
		private void search()
		{
			listSongs.Items.Clear();
			string[] songList = SongSearch.search(txtSearch.Text);
			listSongs.Items.AddRange(songList);
			txtSearch.SelectAll();
		}
		
		private void addSongToSet()
		{
			currentSet.addSongToSet(currentSong);
			updateSetListbox();
		}
		
		private void removeSongFromSet()
		{
			currentSet.removeSongFromSet(currentSong);
			updateSetListbox();
		}
	}
}
