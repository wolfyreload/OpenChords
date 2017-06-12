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
        /// read last open state
        /// </summary>
        /// <returns></returns>
		public static string readSessionState()
		{
            try
            {
                return FileReaderWriter.readFromFile(Settings.GlobalApplicationSettings.SessionSaveState);
            }
            catch
            {
                return "";
            }
		}
		
        /// <summary>
        /// write last open state
        /// </summary>
        /// <param name="state"></param>
		public static void writeSessionState(string state)
		{
			FileReaderWriter.writeToFile(Settings.GlobalApplicationSettings.SessionSaveState,state);
		}
	

	}
}
