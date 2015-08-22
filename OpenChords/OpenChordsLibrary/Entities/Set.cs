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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class setStyle
    {
        public songStyleSongElements title { get; set; }
        public songStyleSongElements body { get; set; }
        public songStyleSongElements subtitle { get; set; }
        public songStyleBackground background { get; set; }
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class songStyleBackground
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string filename { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class songStyleSongElements
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string align { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bold { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string border { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string border_color { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string color { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string fill { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string fill_color { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string font { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string italic { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string highlight_chorus { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string shadow { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string shadow_color { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string underline { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string valign { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string enabled { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string auto_scale { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string size { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string include_verse { get; set; }
    }



    [Serializable]
    [XmlRoot("slidegroup")]
    public class XmlSetSong
    {
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("type")]
        public string type { get; set; }
        [XmlAttribute("presentation")]
        public string presentation { get; set; }
        [XmlAttribute("path")]
        public string path { get; set; }
        [XmlAttribute("transition")]
        public string transition { get; set; }

        public setStyle style;


        public XmlSetSong()
        {
            type = "song";
            path = "OpenChords/";
            presentation = "";
            transition = "0";
            initializeSetStyle();
        }

        public XmlSetSong(string songName)
        {
            name = songName;
            type = "song";
            path = "OpenChords/";
            presentation = Song.loadSong(songName).presentation;
            transition = "0";
            initializeSetStyle(songName);
        }

        private void initializeSetStyle(string songName = null)
        {
            //only override style if there is an actual background image
            if (songName != null && doesBackgroundFileExist(songName))
            {
                this.style = new setStyle();

                this.style.title = new songStyleSongElements()
                {
                    enabled = "false"
                };

                this.style.subtitle = new songStyleSongElements()
                {
                    enabled = "false"
                };

                this.style.body = new songStyleSongElements()
                {
                    align = "center",
                    bold = "true",
                    border = "true",
                    border_color = "#000000",
                    color = "#FFFFFF",
                    fill = "false",
                    fill_color = "#FF0000",
                    font = "Helvetica",
                    highlight_chorus = "true",
                    italic = "false",
                    shadow = "true",
                    shadow_color = "#000000",
                    size = "48",
                    underline = "false",
                    valign = "middle",
                    enabled = "true",
                    auto_scale = "true"
                };

                style.background = new songStyleBackground();
                style.background.filename = getBackgroundFilename(songName);
            }
        }

        private bool doesBackgroundFileExist(string songName)
        {
            //check if background fileexists
            var backgroundFileName = getBackgroundFilename(songName);
            var destinationBackgroundFile = String.Format("{0}{1}", Settings.ExtAppsAndDir.OpensongBackgroundsFolder, backgroundFileName);
            return File.Exists(destinationBackgroundFile);
        }

        private string getBackgroundFilename(string songName)
        {
            return string.Format("OpenChords/{0}.jpg", songName);
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
                XmlSetSong temp = new XmlSetSong(song.SongFileName);
                setSongs.Add(temp);
            }
        }

        public void Insert(int index, XmlSetSong song)
        {
            setSongs.Insert(index, song);
        }

        internal List<string> listOfSongNames()
        {
            return setSongs.Select(s => s.name).ToList();
        }
    }

    
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
        public bool changeMade { get; set; }

        private List<Song> _songList = null;
        [XmlIgnore]
        public List<Song> songList
        {
            get
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
            if (index >= songSetSize - 1)
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
            return IO.FileFolderFunctions.getDirectoryListingAsList(Settings.ExtAppsAndDir.SetsFolder);
        }

        public static Set loadSet(string setName)
        {
            Set set = XmlReaderWriter.readSet(Settings.ExtAppsAndDir.SetsFolder + setName);
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
                XmlReaderWriter.writeSet(Settings.ExtAppsAndDir.SetsFolder + this.setName, this);
                changeMade = false;
            }
        }

        /// <summary>
        /// reload and return the newly reloaded set
        /// </summary>
        /// <returns></returns>
        public Set reloadSet()
        {
            this.loadAllSongs();
            return this;
        }

        public void exportSetAndSongsToOpenSong()
        {
            Set setClone = this.Clone();
            setClone.reloadSet();
            Export.ExportToOpenSong.exportSetAndSongsToOpenSong(setClone);
        }

        private Set Clone()
        {
            var set = new Set();
            set.indexOfCurrentSong = this.indexOfCurrentSong;
            set.setName = this.setName;
            set.songSetSize = this.songSetSize;
            set._songList = new List<Song>(this._songList);
            set.xmlSetSongCollection = new XmlSetSongCollection(set._songList);
            return set;
        }

        public void loadAllSongs()
        {
            if (_songList == null) _songList = new List<Song>();
            _songList.Clear();
            foreach (string songName in xmlSetSongCollection.listOfSongNames())
            {
                try
                {
                    Song temp = Song.loadSong(songName);
                    _songList.Add(temp);
                }
                catch (Exception ex)
                {
                    logger.Error("Error loading song file", ex);
                }
            }
        }

        public int getSongSetSize()
        {
            return songSetSize;

        }

        public void revertSet()
        {
            var set = Set.loadSet(this.setName);
            this.setName = set.setName;
            this.changeMade = false;
            this.songSetSize = set.songSetSize;
            this.songList = set.songList;
            this.xmlSetSongCollection = set.xmlSetSongCollection;
        }

        /// <summary>
        /// returns the set in html format
        /// </summary>
        /// <returns></returns>
        public string getHtml(DisplayAndPrintSettings settings)
        {
            Export.ExportToHtml htmlExporter = new Export.ExportToHtml(this, settings);
            var result = htmlExporter.GenerateHtml();
            return result;
        }

        public string ExportToHtml(DisplayAndPrintSettings settings)
        {
            Export.ExportToHtml htmlExporter = new Export.ExportToHtml(this, settings);
            string htmlText = htmlExporter.GenerateHtml();
            string folder = null;
            if (settings.settingsType == DisplayAndPrintSettingsType.TabletSettings)
                folder = Settings.ExtAppsAndDir.TabletFolder;
            else
                folder = Settings.ExtAppsAndDir.PrintFolder;
            string destination = String.Format("{0}/{1}.html", folder, this.setName);
            File.WriteAllText(destination, htmlText);
            return destination;
        }

        public string getFullPath()
        {
            return Path.GetFullPath(Settings.ExtAppsAndDir.SetsFolder + this.setName);
        }

        public static Set NewSet(string setName)
        {
            var set = new Set() { setName = setName, changeMade = true };
            set.saveSet();
            return set;
        }

        public static void DeleteSet(Set set)
        {
            File.Delete(Settings.ExtAppsAndDir.SetsFolder + set.setName);
        }
    }
}
