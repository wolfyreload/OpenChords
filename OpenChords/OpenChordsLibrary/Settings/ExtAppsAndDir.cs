using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace OpenChords.Settings
{
    public class ExtAppsAndDir
    {

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    	public static char seperator = System.IO.Path.DirectorySeparatorChar;
        private static Entities.FileAndFolderSettings settings = new Entities.FileAndFolderSettings();


    	
    	private static string fixPaths(string old)
    	{
    		return old.Replace('\\', seperator);
    	}

        //public static void setupExtAppsAndDir()
        //{
        //    if (!IO.FileFolderFunctions.isFilePresent(SETTINGS_FILENAME))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(Entities.Settings));
        //        TextWriter textWriter = new StreamWriter(SETTINGS_FILENAME);
        //        serializer.Serialize(textWriter, settings);
        //        textWriter.Close();
        //    }
        //    else
        //    {
        //        TextReader textReader = new StreamReader(SETTINGS_FILENAME);
        //        try
        //        {
        //            XmlSerializer deserializer = new XmlSerializer(typeof(Entities.Settings));
        //            settings = (Entities.Settings)deserializer.Deserialize(textReader);
        //        }
        //        catch (Exception Ex)
        //        {
        //            Console.WriteLine(Ex.StackTrace);

        //        }
        //        finally
        //        {
        //            textReader.Close();
        //        }

        //    }


        //}

        static ExtAppsAndDir()
        {
            settings = Entities.FileAndFolderSettings.loadSettings();
        }

        public static void refreshFileAndFolderSettings()
        {
            settings = Entities.FileAndFolderSettings.loadSettings();
        }
		
		public static String fileManager
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

        public static String manual
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
        public static String pdfViewerApp
        {
	        get
	        {	
	        	string path = AppsFolder + "PDF\\PDF.exe";
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }
        public static String openSongApp
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
        public static String musicPlayerApp{
	        get
	        {
                string path = AppsFolder + "Mp3\\xmplay.exe";
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }
        public static String FileSyncUtility{
	        get
	        {
                string path = settings.FileSync;
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }
		
		public static String dataFolder{
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
        public static String songsFolder{
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
        public static String setsFolder{
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
        public static String notesFolder{
	        get
	        {
                string path = dataFolder + "Notes\\";
	        	string fixedPath = fixPaths(path);
	        	if (!System.IO.Directory.Exists(fixedPath))
	        		System.IO.Directory.CreateDirectory(fixedPath);
	        	return fixedPath;
	        	
	        }
        }
        public static String mediaFolder{
	        get
	        {	
	        	string path = settingsFolder + "Media\\";
	        	string fixedPath = fixPaths(path);
	        	if (!System.IO.Directory.Exists(fixedPath))
	        		System.IO.Directory.CreateDirectory(fixedPath);
	        	return fixedPath;
	        	
	        }
        }
        //public static String presentations = ".\\Data\\Present\\";
        public static String printFolder{
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
        public static String opensongSongsFolder{
	        get
	        {	
	        	string path;
                //if (settings.isLocalOpenSong)
                //    path = AppsFolder + "OpenSong\\OpenSong Data\\Songs\\";
                //else
                    path = settings.OpenSongSetsAndSongs + "\\Songs\\OpenChords\\";
                
                string fixedPath = fixPaths(path);
	        	if (!System.IO.Directory.Exists(fixedPath))
	        		System.IO.Directory.CreateDirectory(fixedPath);
	        	return fixedPath;
	        	
	        }
        }
        public static String openSongSetFolder{
	        get
	        {	
                	string path;
                //if (settings.isLocalOpenSong)
                //    path = AppsFolder + "OpenSong\\OpenSong Data\\Sets\\";
                //else
                    path = settings.OpenSongSetsAndSongs + "\\Sets\\";

	        	string fixedPath = fixPaths(path);
	        	if (!System.IO.Directory.Exists(fixedPath))
	        		System.IO.Directory.CreateDirectory(fixedPath);
	        	return fixedPath;
	        	
	        }
        }
        
        //point to directory itself (and not the contents)
        public static String songsDisplay{
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

        public static String AppsFolder
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

        public static String settingsFolder
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
        public static String sessionSaveState{
	        get
	        {	
	        	string path = settingsFolder + "SessionSaveState";
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }
    
        //print and display settings
        public static String printSettingsFilename{
	        get
	        {	
	        	string path = settingsFolder + "PrintSettings.xml";
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }
        public static String displaySettingsFileName{
	        get
	        {	
	        	string path = settingsFolder + "DisplaySettings.xml";
	        	string fixedPath = fixPaths(path);
	        	return fixedPath;
	        	
	        }
        }

        public static string tabletSettingsFilename
        {
            get
            {
                string path = settingsFolder + "TabletSettings.xml";
                string fixedPath = fixPaths(path);
                return fixedPath;

            }
        }
		
		public static String welcomeSlide{
			get
			{
				
				Entities.FileAndFolderSettings settings =  Entities.FileAndFolderSettings.loadSettings();	
				
				if (!settings.OpenSongUseWelcomeSlide) return null;
				
				string path = dataFolder + settings.OpenSongWelcomeSlide;
				string fixedPath = fixPaths(path);
	        	return fixedPath;
			}
		}






        
    }
}
