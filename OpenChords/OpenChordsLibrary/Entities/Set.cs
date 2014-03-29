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
using System.Xml.Serialization;
using System.Linq;

namespace OpenChords.Entities
{

    [Serializable]
    [XmlRoot("slidegroup")]
    public class XmlSetSong
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("type")]
        public string type;
        [XmlAttribute("presentation")]
        public string presentation;
        [XmlAttribute("path")]
        public string path; 

        public XmlSetSong() {
            type = "song";
            path="OpenChords/";
            presentation = "";
        }

        public XmlSetSong(string songName)
        {
            name = songName;
            type = "song";
            path = "OpenChords/";
            presentation = "";
        }
    }

    [Serializable]
    public class XmlSetSongCollection
    {
        [XmlElement("slide_group")]
        public List<XmlSetSong> setSongs;

        public XmlSetSongCollection()
        {
            setSongs = new List<XmlSetSong>();
        }

        public XmlSetSongCollection(List<Song> songs)
        {
            setSongs = new List<XmlSetSong>();
            foreach (Song song in songs)
            {
                XmlSetSong temp = new XmlSetSong(song.title);
                setSongs.Add(temp);
            }
        }

        internal List<string> listOfSongNames()
        {
            return setSongs.Select(s => s.name).ToList();
        }
    }


    //public class SongCollection
    //{
    //    public void Add(List<Song> songs, XmlSetSong song)
    //    {
    //        setSongs.Add(song);
    //    }

    //    public int LastIndexOf(XmlSetSong song)
    //    {
    //        return setSongs.LastIndexOf(song);
    //    }

    //    public void Remove(string songTitle)
    //    {
    //        var song = setSongs.FirstOrDefault(t => t.name == songTitle);
    //        if (song != null)
    //            setSongs.Remove(song);
    //    }

    //    public void Remove(int index)
    //    {
    //        setSongs.RemoveAt(index);
    //    }

    //    public void MoveUp(int index)
    //    {
    //        var tempSetSong = setSongs[index];
    //        //remove the song
    //        setSongs.RemoveAt(index);

    //        setSongs.Insert(index - 1, tempSetSong);
    //    }

    //    public void MoveDown(int index)
    //    {
    //        var tempSetSong = setSongs[index];
    //        //remove the song
    //        setSongs.RemoveAt(index);

    //        setSongs.Insert(index + 1, tempSetSong);
    //    }

    //    internal List<string> listOfSon0gNames()
    //    {
    //        return setSongs.Select(s => s.name).ToList();
    //    }

    //    internal void Insert(int position, XmlSetSong slide)
    //    {
    //        setSongs.Insert(position, slide);
    //    }
    //}

	/// <summary>
	/// Description of Set
	/// </summary>
    /// 
    [Serializable]
	public class Set
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	    [XmlAttribute("name")]
        public string setName;
		[XmlElement("slide_groups")]
        public XmlSetSongCollection xmlSetSongCollection;
        [XmlIgnore]
        public int indexOfCurrentSong;
        [XmlIgnore]
		public int songSetSize;
        [XmlIgnore]
		public bool changeMade {get; set;}

        private List<Song> _songList = null;
        [XmlIgnore]
        public List<Song> songList { get
            {
                if (_songList == null)
                    loadAllSongs();
                return _songList;
            }
            set
            {
                _songList = value;
            }
        }

		
		public Set()
		{
            xmlSetSongCollection = new XmlSetSongCollection();
			indexOfCurrentSong = -1;
			songSetSize = 0;
			
			changeMade = false;
		}
		
		public void addSongToSet(Song song)
		{
            songList.Add(song);
            indexOfCurrentSong = songList.LastIndexOf(song);
            songSetSize++;
            changeMade = true;
		}
		
		public void addSongToSet(string songTitle)
		{
            var song = Song.loadSong(songTitle);
            addSongToSet(song);
		}

        public void addSongToSet(int position, Song song)
        {
            songList.Insert(position, song);
            indexOfCurrentSong = songList.LastIndexOf(song);
            songSetSize++;
            changeMade = true;
        }
		public void removeSongFromSet(Song song)
		{
            var index = songList.LastIndexOf(song);
            removeSongFromSet(index);
		}

        public void removeSongFromSet(int index)
        {
            if (index != -1)
            {
                songList.RemoveAt(index);
                
                if (indexOfCurrentSong == songSetSize - 1)
                    indexOfCurrentSong--;

                songSetSize--;

                changeMade = true;
            }
        }

		public void clearSongSet()
		{
            songList = new List<Song>();
			indexOfCurrentSong = -1;
			songSetSize = 0;
			
			changeMade = true;
		}
		
		public void moveSongUp()
		{
			int index = indexOfCurrentSong;
			if (index <= 0)
				return;

            var item = songList[index];
            songList.RemoveAt(index);
            index--;
            songList.Insert(index, item);
            indexOfCurrentSong = index;
			changeMade = true;
			
		}
		
		public void moveSongDown()
		{
			int index = indexOfCurrentSong;
			if (index >= songSetSize-1)
				return;

            var item = songList[index];
            songList.RemoveAt(index);
            index++;
            songList.Insert(index, item);
            indexOfCurrentSong = index;
            changeMade = true;
		}


        /// <summary>
        /// returns a list of all sets in the system
        /// </summary>
        /// <returns></returns>
        public static List<string> listOfAllSets()
        {
            return IO.FileFolderFunctions.getDirectoryListingAsList(Settings.ExtAppsAndDir.setsFolder);
        }

		public static Set loadSet(string setName)
		{
			Set set = SettingsReaderWriter.readSet(Settings.ExtAppsAndDir.setsFolder + setName);
            set.setName = setName;
			set.changeMade = false;
            set.indexOfCurrentSong = set.songList.Count - 1;
            set.songSetSize = set.songList.Count;
            return set;
		}

        public void saveSet()
        {
            if (this.setName != "" && changeMade == true)
            {
                xmlSetSongCollection = new XmlSetSongCollection(songList);
                SettingsReaderWriter.writeSet(Settings.ExtAppsAndDir.setsFolder + this.setName, this);
            }
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
		
		public void loadAllSongs()
		{
            if (_songList == null) _songList = new List<Song>();
            _songList.Clear();
			foreach (string songName in xmlSetSongCollection.listOfSongNames())
			{
				Song temp = Song.loadSong(songName);
                _songList.Add(temp);	
			}
		}
		
		public int getSongSetSize()
		{
			return songSetSize;
	
		}

        /// <summary>
        /// returns the set in html format
        /// </summary>
        /// <returns></returns>
        public List<SongHtml> getHtml(DisplayAndPrintSettings settings)
        {
            Export.ExportToHtml htmlExporter = new Export.ExportToHtml(this, settings);
            var result = htmlExporter.GenerateHtml();
            return result;
        }

        /// <summary>
        /// create the pdf and return the path to the pdf
        /// </summary>
        /// <param name="settingsType"></param>
        /// <returns></returns>
        public string getPdfPath(DisplayAndPrintSettingsType settingsType)
        {
            string pdfPath;
            pdfPath = Export.ExportToPdf.exportSet(this, settingsType);
            pdfPath = Settings.ExtAppsAndDir.printFolder + pdfPath;
            return pdfPath;
        }

        /// <summary>
        /// create the pdf and return the path to the pdf
        /// </summary>
        /// <param name="settingsType"></param>
        /// <returns></returns>
        public string getPdfPath(DisplayAndPrintSettingsType settingsType, string settingsPath)
        {
            string pdfPath;
            pdfPath = Export.ExportToPdf.exportSet(this, settingsType, settingsPath);
            pdfPath = Settings.ExtAppsAndDir.printFolder + pdfPath;
            return pdfPath;
        }


        public void displayPdf(DisplayAndPrintSettingsType settingsType)
        {
            
            var pdfPath = getPdfPath(settingsType);

            //get the filemanager for the filesystem
            string fileManager = Settings.ExtAppsAndDir.fileManager;
            //try run the file with the default application
            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(pdfPath);
            else
                System.Diagnostics.Process.Start(fileManager, pdfPath);

        }
    }
}
