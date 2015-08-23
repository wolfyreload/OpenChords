using System;
using System.Xml.Linq;
using OpenChords.Entities;
using System.IO;
using System.Xml.Serialization;

using System.Drawing;
using System.Xml;
using System.Text;

namespace OpenChords.IO
{
    public static class XmlReaderWriter
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private static void xmlWriter<T>(string filename, T item)
        {
            using (StringWriter stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var blankNamespace = new XmlSerializerNamespaces();
                blankNamespace.Add(string.Empty, string.Empty);

                serializer.Serialize(stringWriter, item, blankNamespace);
                //"windowsify the output"
                string xmlString = stringWriter.ToString();
                xmlString = xmlString.Replace("\r", "");
                xmlString = xmlString.Replace("\n", "\r\n");
                File.WriteAllText(filename, xmlString);
            }
        }

        private static T xmlReader<T>(string filename)
        {
            using (TextReader textReader = new StreamReader(filename))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T));
                T item = (T)deserializer.Deserialize(textReader);
                return item;
            }
        }

        /// <summary>
        /// writes the current song to file
        /// </summary>
        /// <param name="filename"></param>
        public static void writeSong(String filename, Song song)
        {
            xmlWriter<Song>(filename, song);
        }

        /// <summary>
        /// reads in the specified song
        /// </summary>
        /// <param name="filename"></param>
        public static Song readSong(String filename)
        {
            try
            {
                Song song = xmlReader<Song>(filename);
                song.SongFileName = Path.GetFileName(filename); 
                song.songFilePath = filename;
                return song;
            }
            catch (Exception Ex)
            {
                logger.Error("Error reading song", Ex);
            }

            return new Song();
           
        }

        private static string SETTINGS_LOCAL_FILENAME = "settings.xml";
        private static string SETTINGS_APPDATA_FILENAME = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/OpenChords/settings.xml";

        public static void writeFileAndFolderSettings(FileAndFolderSettings settings)
        {
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

            xmlWriter<FileAndFolderSettings>(path, settings);
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
                new FileInfo(path).Directory.Create(); //create directory if it doesn't exist
                xmlWriter<FileAndFolderSettings>(path, settings);
            }
            else
            {
                try
                {
                    settings = xmlReader<FileAndFolderSettings>(path);
                }
                catch (Exception Ex)
                {
                    logger.Error("Error reading settings", Ex);

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
            DisplayAndPrintSettings settings = null;
            try
            {
                settings = xmlReader<DisplayAndPrintSettings>(fileName);
            }
            catch (Exception ex)
            {
                logger.Error("Error reading settings", ex);
            }

            return settings;
        }
     
        public static void writeSettings(String fileName, DisplayAndPrintSettings settings)
        {
            xmlWriter<DisplayAndPrintSettings>(fileName, settings);
        }

        /// <summary>
        /// reads in the specified song set
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Set readSet(string filename)
        {
            Set set = new Set();
            try
            {
                set = xmlReader<Set>(filename);
            }
            catch (Exception ex)
            {
                logger.Debug("Failed to decode set", ex);
            }
            return set;
        }

        /// <summary>
        /// writes the supplied song set to disk
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="set"></param>
        public static void writeSet(string filename, Set set)
        {
            try
            {
                xmlWriter<Set>(filename, set);
            }
            catch (Exception Ex)
            {
                logger.Error("Error writing set", Ex);
            }
        }

    }
}
