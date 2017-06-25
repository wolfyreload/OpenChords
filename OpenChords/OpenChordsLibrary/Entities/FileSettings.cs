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

        public enum KeyNotationLanguageType
        {
            English,
            German
        };

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string OpenSongExecutable { get; set; }
        public string OpenSongSetsAndSongs { get; set; }
        public bool PortableMode { get; set; }
        public string ApplicationDataFolder { get; set; }
        public bool HttpServerEnabled { get; set; }
        public int HttpServerPort { get; set; }
        public bool PreferFlats { get; set; }
        public string ExportToOpenSongSubFolder { get; set; }
        public KeyNotationLanguageType KeyNotationLanguage { get; set; }


        [XmlIgnore]
        public string OpenChordsApplicationDirectory { get; private set; }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar;
            }
        }

        public static FileAndFolderSettings loadSettings()
        {
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings();
            if (settings.ExportToOpenSongSubFolder == null)
                settings.ExportToOpenSongSubFolder = "OpenChords";
            return settings;
        }

        /// <summary>
        /// load settings with specified path
        /// </summary>
        /// <returns></returns>
        public static FileAndFolderSettings loadSettings(string path)
        {        
            var settings = IO.XmlReaderWriter.readFileAndFolderSettings(path);
            return settings;
        }

       
        public void saveSettings()
        {
            IO.XmlReaderWriter.writeFileAndFolderSettings(this);

        }

        protected FileAndFolderSettings()
        {
            OpenChordsApplicationDirectory = AssemblyDirectory;
        }

        public FileAndFolderSettings(FileAndFolderSettingsType type = FileAndFolderSettingsType.Normal)
        {
            OpenChordsApplicationDirectory = AssemblyDirectory;
            if (type == FileAndFolderSettingsType.Normal)
            {
                PortableMode = false;
                ApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/OpenChords";
            }
            else
            {
                PortableMode = true;
                ApplicationDataFolder = "../Data";
            }

            OpenSongExecutable = "";
            OpenSongSetsAndSongs = "";
            HttpServerPort = 8083;
            HttpServerEnabled = false;
            PreferFlats = false;
            KeyNotationLanguage = KeyNotationLanguageType.English;
            ExportToOpenSongSubFolder = "OpenChords";
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
