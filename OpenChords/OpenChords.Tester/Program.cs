using OpenChords.Entities;
using OpenChords.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChords.Teester
{
    class Program
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private static void ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType type)
        {
            //get the default settings
            var defaultSettings = new Entities.DisplayAndPrintSettings(type);

            //check if the setting file is present
            var settingsFilePresent = defaultSettings.isSettingsFilePresent();

            //create the settings file if it does not already exist
            if (!settingsFilePresent)
                defaultSettings.saveSettings();

            //check if there are nulls in the file
            var savedSettingsFile = Entities.DisplayAndPrintSettings.loadSettings(type);
            var nullsInSettingsFile = savedSettingsFile.nullsInFile();

            //if there are nulls in the file use the default settings file
            if (nullsInSettingsFile)
                defaultSettings.saveSettings();
        }


        //make the settings files if they dont exist
        private static void setup()
        {
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.DisplaySettings);
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.PrintSettings);
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.TabletSettings);

            OpenChords.Entities.GlobalVariables.restartApplicationOnExit = false;
            OpenChords.Entities.GlobalVariables.patchApplicationOnExit = false;
        }


        /// <summary>
        /// Program entry point.
        /// </summary>
        private static void Main(string[] args)
        {   
            setup();

            //testSearchFunctionality();

            testPrintingToPdf();
			
        }

        private static void testPrintingToPdf()
        {
            
            string[] songlist =
                FileFolderFunctions.getDirectoryListing(OpenChords.Settings.ExtAppsAndDir.songsFolder);
            string[] setList = FileFolderFunctions.getDirectoryListing(OpenChords.Settings.ExtAppsAndDir.setsFolder);


            var firstSong = Song.loadSong(songlist.First());
            var firstSet = Set.loadSet(setList.First());


            var settings = new Entities.DisplayAndPrintSettings(DisplayAndPrintSettingsType.TabletSettings);
            settings.saveSettings();
            //firstSong.displayPdf(Entities.DisplayAndPrintSettingsType.TabletSettings);

            firstSet.displayPdf(Entities.DisplayAndPrintSettingsType.TabletSettings);
        }

        private static void testSearchFunctionality()
        {

            var list1 = OpenChords.Functions.SongSearch.search("You are holy");

            var list2 = OpenChords.Functions.SongSearch.search("'There must be more' 'consuming fire'");
            logger.Info(list1);

            logger.Info(list2);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (OpenChords.Entities.GlobalVariables.restartApplicationOnExit)
                System.Diagnostics.Process.Start("UpgradeScript.bat", "restart");
            if (OpenChords.Entities.GlobalVariables.patchApplicationOnExit)
                System.Diagnostics.Process.Start("UpgradeScript.bat");


        }

    }
}
