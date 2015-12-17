using OpenChords.Entities;
using OpenChords.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OpenChords.Export
{
    public class ExportToOpenSong
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// exports a single set and its songs to OpenSong
        /// </summary>
        /// <param name="set"></param>
        public static void exportSetAndSongsToOpenSong(Set set)
        {
            //copy all songs in the set and the welcome slide if needed
            exportSongListToOpenSong(set);

            //write the set file
            writeOpenSongSet(set);
        }

        public static void exportAllSongsToOpenSong()
        {
            var songNameList = IO.FileFolderFunctions.getDirectoryListingAsList(Settings.ExtAppsAndDir.SongsFolder);
            
            //clear all the files in the songs folder
            IO.FileFolderFunctions.clearFolder(Settings.ExtAppsAndDir.OpensongSongsFolder);

            //copy all the new files accross
            foreach (string songName in songNameList)
            {
                var song = Song.loadSong(songName);
                saveSonginOpenSong(song);
            }
        }

        private static void saveSonginOpenSong(Song song)
        {
            //write song
            var destinationSongFolder = Settings.ExtAppsAndDir.OpensongSongsFolder;
            var destinationFile = destinationSongFolder + song.title;
            song.saveSong(destinationFile);
        }

        /// <summary>
        /// export the list of songs and the welcome slide
        /// </summary>
        /// <param name="set"></param>
        private static void exportSongListToOpenSong(Set set)
        {
            string destination =Settings.ExtAppsAndDir.OpensongSongsFolder;

            foreach (var song in set.songList)
            {
                saveSonginOpenSong(song);
            }

        }

        /// <summary>
        /// export all the sets in openchords to OpenSong
        /// </summary>
        public static void exportAllSetsToOpenSong()
        {
            var ListOfSetNames = IO.FileFolderFunctions.getDirectoryListingAsList(Settings.ExtAppsAndDir.SetsFolder);
            foreach (string setName in ListOfSetNames)
            {
                var set = Set.loadSet(setName);
                writeOpenSongSet(set);
            }

        }


        /// <summary>
        /// writes the current set as an OpenSong set
        /// </summary>
        /// <param name="set">The Original OpenChords set</param>
        private static void writeOpenSongSet(Set set)
        {
            try
            {
                var filename = Settings.ExtAppsAndDir.OpenSongSetFolder + set.setName;
                XmlReaderWriter.writeSet(filename, set);
            }
            catch (Exception Ex)
            {
                logger.Error("Error writing opensong set", Ex);
            }

        }

        /// <summary>
        /// start opensong if it is not already open
        /// </summary>
        public static void launchOpenSong()
        {
            //first check that opensong is not already running
            Process[] pname = Process.GetProcessesByName("OpenSong");
            if (pname.Count() > 0) //opensong is not already open
            {
                MessageBox.Show("There is already an instance of OpenSong open");
            }
            else
            {
                if (Settings.ExtAppsAndDir.IsOpenSongExecutableConfigured)
                    System.Diagnostics.Process.Start(Settings.ExtAppsAndDir.OpenSongExecutable);
                if (Settings.ExtAppsAndDir.IsOpenSongDataFolderConfigured)
                    System.Diagnostics.Process.Start(Settings.ExtAppsAndDir.OpenSongSetFolder);
            }
        }







    }
}
