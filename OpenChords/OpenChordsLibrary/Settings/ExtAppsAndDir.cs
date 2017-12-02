using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;




namespace OpenChords.Config
{

    public class ExtAppsAndDirClass
    {

        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Entities.FileAndFolderSettings settings;

        private string fixPaths(string path)
        {
            path = path.Replace("\\", "#Path#");
            path = path.Replace("/", "#Path#");
            path = path.Replace("#Path#", Path.DirectorySeparatorChar.ToString());
            path = Path.GetFullPath(path);
            return path;
        }

        public ExtAppsAndDirClass(Entities.FileAndFolderSettings settings)
        {
            this.settings = settings;
           
        }

        public bool HttpServerEnabled
        {
            get
            {
                return settings.HttpServerEnabled;
            }
        }

        public int HttpServerPort
        {
            get
            {
                return settings.HttpServerPort;
            }
        }

        public String fileManager
        {
            get
            {
                if (IO.FileFolderFunctions.isFilePresent(fixPaths("/usr/bin/pcmanfm")))
                    return fixPaths("/usr/bin/pcmanfm");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("/usr/bin/nautilus")))
                    return fixPaths("/usr/bin/nautilus");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("/usr/bin/thunar")))
                    return fixPaths("/usr/bin/thunar");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("/usr/bin/nemo")))
                    return fixPaths("/usr/bin/nemo");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths(@"C:\Windows\explorer.exe")))
                    return fixPaths(@"C:\Windows\explorer.exe");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("/usr/bin/dolphin")))
                    return fixPaths("/usr/bin/dolphin");
                else
                    throw new Exception("Cannot find file manager on system");
            }
        }

        public String manual
        {
            get
            {
                var path = settings.OpenChordsApplicationDirectory + "help.html";
                return path;
            }

        }

        private string getFullPath(string path)
        {
            path = Path.GetFullPath(path);
            path = fixPaths(path);
            return path;
        }
        private string fixPathsAndMakeDirectory(string path)
        {
            path = fixPaths(path);
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            return path;
        }

        //external Apps
        public String OpenSongExecutable
        {
            get
            {
                return getFullPath(settings.OpenSongExecutable);
            }
        }

        public bool IsOpenSongExecutableConfigured
        {
            get
            {
                return !string.IsNullOrWhiteSpace(settings.OpenSongExecutable) && File.Exists(OpenSongExecutable);
            }
        }

        public String ApplicationDataFolder
        {
            get
            {
                string path = null;
                if (Path.IsPathRooted(settings.ApplicationDataFolder))
                    path = settings.ApplicationDataFolder + Path.DirectorySeparatorChar;
                else
                    path = Path.Combine(settings.OpenChordsApplicationDirectory, settings.ApplicationDataFolder);
                path = fixPathsAndMakeDirectory(path);
                return path;
            }
        }

        

        /// <summary>
        /// The song folder for OpenChords
        /// </summary>
        public String SongsFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Songs\\");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }

        /// <summary>
        /// The Sets folder for OpenChords
        /// </summary>
        public String SetsFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Sets\\");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }

        public String MediaFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Media\\");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }
        public String PrintFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Export/Print/");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }

        public String TabletFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Export/Tablet/");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }

        public String SettingsFolder
        {
            get
            {
                string path = Path.Combine(ApplicationDataFolder, "Settings\\");
                path = fixPathsAndMakeDirectory(path);
                return path;
            }
        }

        //save of last known state
        public String SessionSaveState
        {
            get
            {
                string path = Path.Combine(SettingsFolder, "SessionSaveState");
                path = fixPaths(path);
                return path;
            }
        }

        //print and display settings
        public String PrintSettingsFilename
        {
            get
            {
                string path = Path.Combine(SettingsFolder, "PrintSettings.xml");
                path = fixPaths(path);
                return path;
            }
        }
        public String DisplaySettingsFileName
        {
            get
            {
                string path = Path.Combine(SettingsFolder, "DisplaySettings.xml");
                path = fixPaths(path);
                return path;
            }
        }

        public string TabletSettingsFilename
        {
            get
            {
                string path = Path.Combine(SettingsFolder, "TabletSettings.xml");
                path = fixPaths(path);
                return path;
            }
        }

        public string UserInterfaceSettingsFileName
        {
            get
            {
                string path = Path.Combine(SettingsFolder, "UserInterfaceSettings.xml");
                path = fixPaths(path);
                return path;
            }
        }

        public bool IsOpenSongDataFolderConfigured
        {
            get
            {
                return !string.IsNullOrWhiteSpace(settings.OpenSongSetsAndSongs);
            }
        }


        //external Directories
        public String OpensongSongsFolder
        {
            get
            {
                string path = Path.GetFullPath(settings.OpenSongSetsAndSongs);
                path = Path.Combine(path, "Songs\\OpenChords\\");
                path = fixPathsAndMakeDirectory(path);
                return path;

            }
        }

        public String OpensongBackgroundsFolder
        {
            get
            {
                string path = Path.GetFullPath(settings.OpenSongSetsAndSongs);
                path = Path.Combine(path, "Backgrounds\\");
                path = fixPathsAndMakeDirectory(path);
                return path;
            }
        }
        public String OpenSongSetFolder
        {
            get
            {
                string path = Path.GetFullPath(settings.OpenSongSetsAndSongs);
                path = Path.Combine(path, "Sets\\");
                path = fixPathsAndMakeDirectory(path);
                return path;
            }
        }

        public bool PreferFlats
        {
            get
            {
                return settings.PreferFlats;
            }
        }

        public Entities.FileAndFolderSettings.KeyNotationLanguageType KeyNotationLanguage { get { return settings.KeyNotationLanguage; } set { settings.KeyNotationLanguage = value; } }

        public bool ForceAlwaysOnTopWhenPresenting { get { return settings.ForceAlwaysOnTopWhenPresenting; } }
    }
}
