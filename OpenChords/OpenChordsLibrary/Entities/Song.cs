/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 05:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenChords.Functions;
using OpenChords.IO;
using OpenChords.Settings;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OpenChords.Entities
{
	/// <summary>
	/// Description of Song.
	/// </summary>

    [XmlRoot("song")]
	public class Song
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		
		public string title;
		public string author;
		public string key;

        public string capo;
        public string preferFlats;


        [XmlIgnore]
        public bool PreferFlats
        {
            get
            {
                var value = false;
                if (!string.IsNullOrEmpty(preferFlats))
                    bool.TryParse(preferFlats, out value);
                return value;
            }
            set
            {
                preferFlats = value.ToString();
            }
        }
        
        [XmlIgnore]
        public int Capo
        {
            get
            {
                var value = 0;
                if (!string.IsNullOrEmpty(capo))
                    int.TryParse(capo, out value);
                return value;
            }
            set
            {
                capo = value.ToString();
            }
        }

		public string presentation;
		
		public string lyrics;
        
        
		public string notes;
        
        public string ccli;
		
		public Song()
		{
			title = "";
			author = "";
			key = "";
			capo = "0";
			presentation = "";
			
			lyrics = "";
		    PreferFlats = false;

		}

        
		
		
		public static Song loadSong(string SongName)
		{
			Song song = XmlReaderWriter.readSong(ExtAppsAndDir.songsFolder + SongName);
            song.notes = Note.loadNotes(SongName).notes;
            return song;			
			
		}
		
		public void saveSong()
		{
            
			
			if (this.title != "")
			{
                var noteClass = Note.loadNotes(this.title);
				noteClass.notes = notes;
				
                //remove the notes when saving the song
				notes = "";
				XmlReaderWriter.writeSong(ExtAppsAndDir.songsFolder + this.title, this);
                //add the notes back
                notes = noteClass.notes;

				noteClass.saveNotes();
			}
		}
		
		public void deleteSong()
		{
			if (this.title != "")
			{
				FileFolderFunctions.deleteFile(ExtAppsAndDir.songsFolder + this.title);
				FileFolderFunctions.deleteFile(ExtAppsAndDir.notesFolder + this.title);
			}
			
		}
		
		public void revertToSaved()
		{
			Song oldSave = loadSong(title);
            this.title = oldSave.title;
            this.notes = oldSave.notes;
            this.author = oldSave.author;
            this.capo = oldSave.capo;
            this.ccli = oldSave.ccli;
            this.key = oldSave.key;
            this.lyrics = oldSave.lyrics;
            this.presentation = oldSave.presentation;
            
            
            var notesClass = Note.loadNotes(title);
            this.notes = notesClass.notes;
		}
		
		public void transposeKeyUp()
		{
			SongProcessor.transposeKeyUp(this);
			this.key = SongProcessor.transposeChord(this.key, this.PreferFlats).TrimEnd();
		}
		
		public void transposeKeyDown()
		{
			for (int i = 0; i < 11; i++)
			{
				transposeKeyUp();
			}
		}
		
		public void capoUp()
		{
			for (int i = 0; i < 11; i++)
			{
				capoDown();
			}
			
		}
		
		public void capoDown()
		{
			SongProcessor.transposeKeyUp(this);
			int tempcapo = 12 + this.Capo - 1;
			this.Capo = tempcapo % 12;
		}
		
		public void fixFormatting()
		{
			SongProcessor.fixFormatting(this);
		}

        public void fixNoteOrdering()
        {
            SongProcessor.fixNoteOrdering(this);
        }

        public void fixLyricsOrdering()
        {
            SongProcessor.fixLyricsOrdering(this);
            SongProcessor.fixFormatting(this);
        }
		

		
		public bool isMp3Available()
		{

            string songName = ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return true;
			else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return true;

            return false;

		}

        public string getMp3Filename()
        {
            string songName = ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return songName + ".mp3";
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return songName + ".mp4";
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return songName + ".avi";
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return songName + ".ogg";
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return songName + ".flv";
            else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return songName + ".mkv";
			
            return null;

        }

        public List<SongVerse> getSongVerses()
        {
            return SongVerse.getSongVerses(this);
        }

        public void displayPdf(DisplayAndPrintSettingsType settingsType)
        {
            //get the filemanager for the filesystem
            string fileManager = ExtAppsAndDir.fileManager;

            var pdfPath = Export.ExportToPdf.exportSong(this, settingsType);

            pdfPath = ExtAppsAndDir.printFolder + pdfPath;

            //try run the file with the default application
            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(pdfPath);
            else
                System.Diagnostics.Process.Start(fileManager, pdfPath);


        }
		
	}
}
