using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenChords.Entities
{


    [XmlRoot("settings")]
    public class FileAndFolderSettings
    {
        public enum FileAndFolderSettingsType
        {
            Portable,
            Normal
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string OpenSongExecutable { get; set; }
        public string OpenSongSetsAndSongs { get; set; }
        public bool PortableMode { get; set; }
        public string ApplicationDataFolder { get; set; }
        public bool CheckForUpdates { get; set; }
        
        public static FileAndFolderSettings loadSettings()
        {
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings();
            var dir = new DirectoryInfo(@".\");
            settings.CurrentPath = dir.FullName;
            return settings;
        }

        /// <summary>
        /// load settings with specified path
        /// </summary>
        /// <returns></returns>
        public static FileAndFolderSettings loadSettings(string path)
        {        
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings(path);
            var dir = new DirectoryInfo(path);
            settings.CurrentFullPath = path;
            settings.CurrentPath = dir.Parent.FullName + "\\";
            return settings;
        }

        [XmlIgnore]
        public string CurrentPath { get; protected set; }

        [XmlIgnore]
        public string CurrentFullPath { get; protected set; }

        public void saveSettings()
        {
            IO.XmlReaderWriter.writeFileAndFolderSettings(this);

        }

        protected FileAndFolderSettings() { }

        public FileAndFolderSettings(FileAndFolderSettingsType type = FileAndFolderSettingsType.Normal)
        {
            if (type == FileAndFolderSettingsType.Normal)
            {
                OpenSongExecutable = "";
                OpenSongSetsAndSongs = "";
                PortableMode = false;
                ApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\OpenChords";
                CheckForUpdates = true;
            }
            else
            {
                OpenSongExecutable = "";
                OpenSongSetsAndSongs = "";
                PortableMode = false;
                ApplicationDataFolder = "..\\Data";
                CheckForUpdates = true;
            }
        }
        
        /// <summary>
        /// gets a fresh version of the current settings and returns the settings
        /// </summary>
        /// <returns></returns>
        public void refresh()
        {
            logger.Debug("Refresh function called that doesn't do anything yet");
            return;
            
        }
    }
}
