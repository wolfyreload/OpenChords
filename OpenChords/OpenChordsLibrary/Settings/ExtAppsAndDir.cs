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
        public char seperator = System.IO.Path.DirectorySeparatorChar;
        private Entities.FileAndFolderSettings settings;

        private string fixPaths(string old)
        {
            return old.Replace('\\', seperator);
        }



        public ExtAppsAndDirClass(Entities.FileAndFolderSettings settings)
        {
            this.settings = settings;
           
        }


        public void refreshFileAndFolderSettings()
        {
            settings.refresh();
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
                else
                    return null;
            }
        }

        public String manual
        {
            get
            {
                Console.WriteLine(settings.CurrentPath);
                var path = settings.CurrentPath + "help.html";
                Console.WriteLine(path);
                if (IO.FileFolderFunctions.isFilePresent(path))
                    return path;
                else
                    return null;
            }

        }

        //external Apps
        public String openSongApp
        {
            get
            {
                string path;
                path = settings.OpenSongExecutable;
                path = Path.GetFullPath(path);
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }

        public bool IsOpenSongExecutableConfigured
        {
            get
            {
                return !string.IsNullOrWhiteSpace(settings.OpenSongExecutable) && File.Exists(openSongApp);
            }
        }

        public String dataFolder
        {
            get
            {
                string path = null;
                if (Path.IsPathRooted(settings.ApplicationDataFolder))
                    path = settings.ApplicationDataFolder + "\\";
                else
                    path = String.Format("{0}{1}\\", settings.CurrentPath, settings.ApplicationDataFolder);
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;
            }
        }

        /// <summary>
        /// The song folder for OpenChords
        /// </summary>
        public String songsFolder
        {
            get
            {
                string path = dataFolder + "Songs\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }

        /// <summary>
        /// The Sets folder for OpenChords
        /// </summary>
        public String setsFolder
        {
            get
            {
                string path = dataFolder + "Sets\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }

        /// <summary>
        /// The Notes folder for OpenChords
        /// </summary>
        public String notesFolder
        {
            get
            {
                string path = dataFolder + "Notes\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }
        public String mediaFolder
        {
            get
            {
                string path = dataFolder + "Media\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }
        //public String presentations = ".\\Data\\Present\\";
        public String printFolder
        {
            get
            {
                string path = dataFolder + "/Export/Print/";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }

        public String tabletFolder
        {
            get
            {
                string path = dataFolder + "/Export/Tablet/";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

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
        public String opensongSongsFolder
        {
            get
            {
                string path;
                var directory = new DirectoryInfo(settings.OpenSongSetsAndSongs);
                path = directory + "\\Songs\\OpenChords\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }

        public String opensongBackgroundsFolder
        {
            get
            {
                string path;
                var directory = new DirectoryInfo(settings.OpenSongSetsAndSongs);
                path = directory.FullName + "\\Backgrounds\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }
        public String openSongSetFolder
        {
            get
            {
                string path;
                var directory = new DirectoryInfo(settings.OpenSongSetsAndSongs);
                path = directory + "\\Sets\\";

                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }

        //point to directory itself (and not the contents)
        public String songsDisplay
        {
            get
            {
                string path;
                //if (settings.isLocalOpenSong)
                //    path = AppsFolder + "OpenSong\\OpenSong Data\\Songs";
                //else
                path = settings.OpenSongSetsAndSongs + "\\Songs\\OpenChords";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

            }
        }


        public String settingsFolder
        {
            get
            {
                string path = dataFolder + "Settings\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;
            }
        }

        //save of last known state
        public String sessionSaveState
        {
            get
            {
                string path = settingsFolder + "SessionSaveState";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }

        //print and display settings
        public String printSettingsFilename
        {
            get
            {
                string path = settingsFolder + "PrintSettings.xml";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }
        public String displaySettingsFileName
        {
            get
            {
                string path = settingsFolder + "DisplaySettings.xml";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }

        public string tabletSettingsFilename
        {
            get
            {
                string path = settingsFolder + "TabletSettings.xml";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }

    }
}
