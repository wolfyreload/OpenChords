using System;
using System.Xml.Linq;
using OpenChords.Entities;
using System.IO;
using System.Xml.Serialization;

using System.Drawing;

namespace OpenChords.IO
{









    public static class XmlReaderWriter
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    	/// <summary>
        /// writes the current song to file
        /// </summary>
        /// <param name="filename"></param>
        public static void writeSong(String filename, Song song)
        {
            //"fix" the lyrics before we save them
            song.lyrics = song.lyrics.Replace("\r", "");
            song.lyrics = song.lyrics.Replace("\n", "\r\n");

            XmlSerializer serializer = new XmlSerializer(typeof(Entities.Song));
            TextWriter textWriter = new StreamWriter(filename);
            serializer.Serialize(textWriter, song);
            textWriter.Close();
        }
               
        /// <summary>
        /// reads in the specified song
        /// </summary>
        /// <param name="filename"></param>
        public static Song readSong(String filename)
        {
            Song song = new Song();
            TextReader textReader = new StreamReader(filename);
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Entities.Song));
                song = (Entities.Song)deserializer.Deserialize(textReader);

                //"fix" the notes and lyrics so that they display properly
                song.lyrics = song.lyrics.Replace("\r", "");
                song.notes = song.notes.Replace("\r", "");
                song.songFilePath = filename;
                song.SongFileName = Path.GetFileName(filename);
            }
            catch (Exception Ex)
            {
                logger.Error("Error reading song", Ex);
            }
            finally
            {
                textReader.Close();
            }


            return song;
        }

        private static string SETTINGS_LOCAL_FILENAME = "settings.xml";
        private static string SETTINGS_APPDATA_FILENAME = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/OpenChords/settings.xml";

        public static void writeFileAndFolderSettings(Entities.FileAndFolderSettings settings)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(Entities.FileAndFolderSettings));

            string path = null;
            if (File.Exists(SETTINGS_LOCAL_FILENAME))
                path = SETTINGS_LOCAL_FILENAME;
            else
                path = SETTINGS_APPDATA_FILENAME;

            //if we select portable mode delete the other settings file
            if (settings.PortableMode)
            {
                path = SETTINGS_LOCAL_FILENAME;
                if (File.Exists(SETTINGS_APPDATA_FILENAME))
                    File.Delete(SETTINGS_APPDATA_FILENAME);
            }
            else //if we select app data for the settings delete the local settings file
            {
                path = SETTINGS_APPDATA_FILENAME;
                if (File.Exists(SETTINGS_LOCAL_FILENAME))
                    File.Delete(SETTINGS_LOCAL_FILENAME);
            }

            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, settings);
            textWriter.Close();

        }

        /// <summary>
        /// read file and folder settings with a provided settings path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Entities.FileAndFolderSettings readFileAndFolderSettings(string path)
        {
            Entities.FileAndFolderSettings settings = new OpenChords.Entities.FileAndFolderSettings();
            if (!IO.FileFolderFunctions.isFilePresent(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Entities.FileAndFolderSettings));
                new FileInfo(path).Directory.Create(); //create directory if it doesn't exist
                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, settings);
                textWriter.Close();
            }
            else
            {
                TextReader textReader = new StreamReader(path);
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(Entities.FileAndFolderSettings));
                    settings = (Entities.FileAndFolderSettings)deserializer.Deserialize(textReader);
                }
                catch (Exception Ex)
                {
                    logger.Error("Error reading settings", Ex);

                }
                finally
                {
                    textReader.Close();
                }

            }

            return settings;

        }

        /// <summary>
        /// read file and folder settings with using the default file and folder setting path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Entities.FileAndFolderSettings readFileAndFolderSettings()
        {
            if (File.Exists(SETTINGS_LOCAL_FILENAME))
                return readFileAndFolderSettings(SETTINGS_LOCAL_FILENAME);
            else
                return readFileAndFolderSettings(SETTINGS_APPDATA_FILENAME);
        }


        public static DisplayAndPrintSettings readSettings(String fileName)
        {
            TextReader textReader = null;
            DisplayAndPrintSettings settings = null;
            try
            {
                textReader = new StreamReader(fileName);
                XmlSerializer deserializer = new XmlSerializer(typeof(DisplayAndPrintSettings));
                settings = (DisplayAndPrintSettings)deserializer.Deserialize(textReader);
                fixNulls(settings);
                textReader.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Error reading settings", ex);
            }

            return settings;
        }

        /// <summary>
        /// check if color col1 matches color col2
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <returns></returns>
        private static bool ifColorsMatch(Color col1, Color col2)
        {
            return col1.A == col2.A && col1.R == col2.R && col1.G == col2.G && col1.B == col2.B;
        }

        /// <summary>
        /// fix null values in the settings files, this is needed for upgrades
        /// </summary>
        /// <param name="settings"></param>
        private static void fixNulls(DisplayAndPrintSettings settings)
        {
           
            //var blankColor = Color.FromArgb(0,0,0,0);

            //if (ifColorsMatch(settings.ChordColor, blankColor))  settings.ChordColor = Color.Black;
            //if (ifColorsMatch(settings.BackgroundColor, blankColor)) settings.BackgroundColor = Color.White;
            //if (ifColorsMatch(settings.LyricsColor, blankColor)) settings.LyricsColor = Color.Black;
            //if (ifColorsMatch(settings.NoteColor, blankColor)) settings.NoteColor = Color.Black;
            //if (ifColorsMatch(settings.HeadingsColor, blankColor)) settings.HeadingsColor = Color.Black;
            //if (ifColorsMatch(settings.TitleColor, blankColor)) settings.TitleColor = Color.Black;
            //if (ifColorsMatch(settings.OrderColor, blankColor)) settings.OrderColor = Color.Black;
            //if (ifColorsMatch(settings.OrderColor2, blankColor)) settings.OrderColor2 = Color.DarkGray;


            //settings.ShowLyrics = settings.ShowLyrics ?? true;
            //settings.ShowChords = settings.ShowChords ?? true;
            //settings.ShowGeneralNotes = settings.ShowGeneralNotes ?? false;
            //settings.ShowNotes = settings.ShowNotes ?? true;
           
   
            //settings.BoldLyrics = settings.BoldLyrics ?? false;
            //settings.BoldChords = settings.BoldChords ?? true;
            //settings.BoldHeadings = settings.BoldHeadings ?? true;
            //settings.BoldNotes = settings.BoldNotes ?? false;
            //settings.BoldTitle = settings.BoldTitle ?? true;
         


        }

        public static void writeSettings(String fileName, DisplayAndPrintSettings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DisplayAndPrintSettings));
            TextWriter textWriter = new StreamWriter(fileName);
            serializer.Serialize(textWriter, settings);
            textWriter.Close();

        }

    }
}
