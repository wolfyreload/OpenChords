/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/15
 * Time: 02:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Xml.Linq;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using OpenChords.Entities;
using OpenChords.Functions;

using System.Xml.Serialization;

namespace OpenChords.IO
{
    
	/// <summary>
	/// Class for reading and writing:
	/// Settings files, set files and saveStateFiles
	/// </summary>
	public static class SettingsReaderWriter
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);	
		
		/// <summary>
		/// reads in the specified song set
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Set readSet(string filename)
		{
		    try
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(Set));
                var fileContents = File.ReadAllText(filename);
                StringReader reader = new StringReader(fileContents);
                var set = (Set)serialiser.Deserialize(reader);
                return set;
            }
            catch(Exception ex)
            {
                logger.Debug("Failed to decode set trying old read method");
                return readOldSet(filename);
            }
            
            
		}

        /// <summary>
        /// reads in the specified song set
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static Set readOldSet(string filename)
        {
            Set set = new Set();
            try
            {
                String Contents = FileReaderWriter.readFromFile(filename);

                string[] elements = SongProcessor.multiLineToStringArray(Contents, true);

                foreach (string element in elements)
                {
                    if (FileFolderFunctions.isFilePresent(Settings.ExtAppsAndDir.songsFolder + element))
                        set.addSongToSet(element);
                }

                return set;

            }
            catch (Exception Ex)
            {
                logger.Error("Error reading set", Ex);
                return new Set();
            }
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
                XmlSerializer serialiser = new XmlSerializer(typeof(Set));
                FileStream file = new FileStream(filename, FileMode.Create);
                serialiser.Serialize(file, set);
                file.Flush();
                file.Close();
            
			}
			catch (Exception Ex)
			{
                logger.Error("Error writing set", Ex);
			}
		}
		

        /// <summary>
        /// read last open state
        /// </summary>
        /// <returns></returns>
		public static string readSessionState()
		{
			return FileReaderWriter.readFromFile(Settings.ExtAppsAndDir.sessionSaveState);
		}
		
        /// <summary>
        /// write last open state
        /// </summary>
        /// <param name="state"></param>
		public static void writeSessionState(string state)
		{
			FileReaderWriter.writeToFile(Settings.ExtAppsAndDir.sessionSaveState,state);
		}
	

	}
}
