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
        private Entities.FileAndFolderSettings settings = new Entities.FileAndFolderSettings();

        private string fixPaths(string old)
        {
            return old.Replace('\\', seperator);
        }



        public ExtAppsAndDirClass()
        {
            settings = Entities.FileAndFolderSettings.loadSettings();
        }

        public void refreshFileAndFolderSettings()
        {
            settings = Entities.FileAndFolderSettings.loadSettings();
        }

        public String fileManager
        {
            get
            {
                if (IO.FileFolderFunctions.isFilePresent(fixPaths("\\usr\\bin\\pcmanfm")))
                    return fixPaths("\\usr\\bin\\pcmanfm");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("\\usr\\bin\\nautilus")))
                    return fixPaths("\\usr\\bin\\nautilus");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("\\usr\\bin\\thunar")))
                    return fixPaths("\\usr\\bin\\thunar");
                else
                    return null;
            }
        }

        public String manual
        {
            get
            {
                if (IO.FileFolderFunctions.isFilePresent("Manual.pdf"))
                    return "Manual.pdf";
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("..\\Manual.pdf")))
                    return fixPaths("..\\Manual.pdf");
                else if (IO.FileFolderFunctions.isFilePresent(fixPaths("..\\..\\Manual.pdf")))
                    return fixPaths("..\\..\\Manual.pdf");
                else
                    return null;
            }

        }

        //external Apps
        public String pdfViewerApp
        {
            get
            {
                string path = AppsFolder + "PDF\\PDF.exe";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }
        public String openSongApp
        {
            get
            {
                string path;
                //if (settings.isLocalOpenSong)
                //    path = AppsFolder + "OpenSong\\OpenSong.exe";
                //else
                path = settings.OpenSongExecutable;

                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }
        public String musicPlayerApp
        {
            get
            {
                string path = AppsFolder + "Mp3\\xmplay.exe";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }
        public String FileSyncUtility
        {
            get
            {
                string path = settings.FileSync;
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }

        public String dataFolder
        {
            get
            {
                string path = settings.ApplicationDataFolder + "\\";
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
                string path = settingsFolder + "Media\\";
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
                string path = settingsFolder + "Print\\";
                string fixedPath = fixPaths(path);
                if (!System.IO.Directory.Exists(fixedPath))
                    System.IO.Directory.CreateDirectory(fixedPath);
                return fixedPath;

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

        public String AppsFolder
        {
            get
            {
                string path = settings.AppsFolder;
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
                string path = settings.SettingsFolder;
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

        public String welcomeSlide
        {
            get
            {

                Entities.FileAndFolderSettings settings = Entities.FileAndFolderSettings.loadSettings();

                if (!settings.OpenSongUseWelcomeSlide) return null;

                string path = dataFolder + settings.OpenSongWelcomeSlide;
                string fixedPath = fixPaths(path);
                return fixedPath;
            }
        }







    }
}
