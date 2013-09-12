/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/13
 * Time: 12:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;

using OpenChords.IO;


namespace OpenChords.Entities
{
	/// <summary>
	/// Description of Set
	/// </summary>
	public class Set
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public string setName;
		
		
		public List<string> songNames;
		public List<Song> songList;
		public int indexOfCurrentSong;
		
		public int songSetSize;
		
		public bool changeMade {get; set;}
		
		public Set()
		{
			songNames = new List<string>();
			songList = new List<Song>();
			indexOfCurrentSong = -1;
			songSetSize = 0;
			
			changeMade = false;
		}
		
		public void addSongToSet(Song song)
		{
            addSongToSet(song.title);
		}
		
		public void addSongToSet(string songTitle)
		{
			songNames.Add(songTitle);
			indexOfCurrentSong = songNames.LastIndexOf(songTitle);
			songSetSize++;
			
			changeMade = true;
		}

		public void removeSongFromSet(Song song)
		{
			if (songNames.Contains(song.title))
			{
                //reverse the songlist
                songNames.Reverse();
				songNames.Remove(song.title);
                //put it the right way around again
                songNames.Reverse();

				if (indexOfCurrentSong == songSetSize-1)
					indexOfCurrentSong--;
				
				songSetSize--;
				
				changeMade = true;
			}
		}

        public void removeSongFromSet(int index)
        {
            if (songNames.Count > index && index != -1)
            {
                songNames.RemoveAt(index);
                
                if (indexOfCurrentSong == songSetSize - 1)
                    indexOfCurrentSong--;

                songSetSize--;

                changeMade = true;
            }
        }

		public void clearSongSet()
		{
			songNames.Clear();
			indexOfCurrentSong = -1;
			songSetSize = 0;
			
			changeMade = true;
		}
		
		public void moveSongUp()
		{
			int index = indexOfCurrentSong;
			if (index <= 0)
				return;
			
			//store the song name
			string tempName = songNames[index];
			
			//remove the song
			songNames.RemoveAt(index);
			
			songNames.Insert(index-1,tempName);
			
			indexOfCurrentSong = index-1;
			
			changeMade = true;
			
		}
		
		public void moveSongDown()
		{
			int index = indexOfCurrentSong;
			if (index >= songSetSize-1)
				return;
			
			//store the song name
			string tempName = songNames[index];
			
			//remove the song
			songNames.RemoveAt(index);
			
			songNames.Insert(index+1,tempName);
			
			indexOfCurrentSong = index+1;
			
			changeMade = true;
		}
		
		
		public static Set loadSet(string setName)
		{
			Set set = SettingsReaderWriter.readSet(Settings.ExtAppsAndDir.setsFolder + setName);
            set.setName = setName;
			set.changeMade = false;
            set.loadAllSongs();

            return set;
		}
		
		public void saveSet()
		{
			if (this.setName != "" && changeMade == true)
				SettingsReaderWriter.writeSet(Settings.ExtAppsAndDir.setsFolder + this.setName, this);
		}
		
        /// <summary>
        /// save and reload the set, returning the reloaded set
        /// </summary>
        /// <returns></returns>
		public Set saveAndreloadSet()
		{
            if (setName != null)
            {
                saveSet();
                return loadSet(setName);
            }
            else
                throw new Exception("set name not valid, cannot save set");
		}

        /// <summary>
        /// reload and return the newly reloaded set
        /// </summary>
        /// <returns></returns>
        public Set reloadSet()
        {
            if (setName != null)
            {
                return loadSet(setName);
            }
            else
            {
                this.loadAllSongs();
                return this;
            }
        }
		
		public void exportSetAndSongsToOpenSong()
		{
            Export.ExportToOpenSong.exportSetAndSongsToOpenSong(this);
		}
		
		private void loadAllSongs()
		{
			songList.Clear();
			foreach (string name in songNames)
			{
				Song temp = Song.loadSong(name);
				songList.Add(temp);	
			}
		}
		
		public int getSongSetSize()
		{
			return songSetSize;
	
		}

        public void displayPdf(DisplayAndPrintSettingsType settingsType)
        {
            //get the filemanager for the filesystem
            string fileManager =Settings.ExtAppsAndDir.fileManager;

            var pdfPath = Export.ExportToPdf.exportSet(this, settingsType);

            pdfPath =Settings.ExtAppsAndDir.printFolder + pdfPath;

            //try run the file with the default application
            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(pdfPath);
            else
                System.Diagnostics.Process.Start(fileManager, pdfPath);

        }
    }
}
