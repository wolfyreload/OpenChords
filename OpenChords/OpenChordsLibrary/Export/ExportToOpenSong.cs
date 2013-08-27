using OpenChords.Entities;
using OpenChords.IO;
using OpenChords.Settings;
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
            var songNameList = IO.FileFolderFunctions.getDirectoryListingAsList(ExtAppsAndDir.songsFolder);
            string source = ExtAppsAndDir.songsFolder;
            string destination = ExtAppsAndDir.opensongSongsFolder;
            
            //clear all the files in the songs folder
            IO.FileFolderFunctions.clearFolder(destination);

            //copy all the new files accross
            foreach (string filename in songNameList)
            {
                var sourceFile = source + filename;
                var destinationFile = destination + filename;
                File.Copy(sourceFile, destinationFile, true);
            }

            //copy welcome slide
            if (!string.IsNullOrEmpty(ExtAppsAndDir.welcomeSlide))
                File.Copy(ExtAppsAndDir.welcomeSlide, destination + "welcome", true);
        }

        /// <summary>
        /// export the list of songs and the welcome slide
        /// </summary>
        /// <param name="set"></param>
        private static void exportSongListToOpenSong(Set set)
        {
            string source = ExtAppsAndDir.songsFolder;
            string destination = ExtAppsAndDir.opensongSongsFolder;
            foreach (string filename in set.songNames)
            {
                var sourceFile = source + filename;
                var destinationFile = destination + filename;
                File.Copy(sourceFile, destinationFile, true);
            }

            //copy welcome slide
            if (!string.IsNullOrEmpty(ExtAppsAndDir.welcomeSlide))
                File.Copy(ExtAppsAndDir.welcomeSlide, destination + "welcome", true);
        }

        /// <summary>
        /// export all the sets in openchords to OpenSong
        /// </summary>
        public static void exportAllSetsToOpenSong()
        {
            var ListOfSetNames = IO.FileFolderFunctions.getDirectoryListingAsList(ExtAppsAndDir.setsFolder);
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
                var filename = ExtAppsAndDir.openSongSetFolder + set.setName;
                Entities.FileAndFolderSettings settings = Entities.FileAndFolderSettings.loadSettings();
                string path = "OpenChords/";

                String Header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
                XElement Contents = new XElement("set");
                Contents.SetAttributeValue("name", set.setName);
                XElement songAttributes = new XElement("slide_groups");
                Contents.Add(songAttributes);

                //use welcome slide if its there and if we want it
                if (settings.OpenSongUseWelcomeSlide)
                {
                    XElement temp = new XElement("slide_group");
                    temp.SetAttributeValue("name", "welcome");
                    temp.SetAttributeValue("type", "song");
                    temp.SetAttributeValue("presentation", "");
                    temp.SetAttributeValue("path", path);

                    songAttributes.Add(temp);
                }

                for (int i = 0; i < set.songNames.Count; i++)
                {
                    XElement temp = new XElement("slide_group");
                    temp.SetAttributeValue("name", set.songNames[i]);
                    temp.SetAttributeValue("type", "song");
                    temp.SetAttributeValue("presentation", "");
                    temp.SetAttributeValue("path", path);

                    songAttributes.Add(temp);
                }
                FileReaderWriter.writeToFile(filename, Header + Contents.ToString());
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
                  MessageBox.Show("OpenSong is already open");
             }
             else
             {
                 System.Diagnostics.Process.Start(ExtAppsAndDir.openSongApp);
             }
         }







    }
}
