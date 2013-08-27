/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 5/15/2011
 * Time: 5:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenChords.Entities;
using OpenChords.Settings;
using OpenChords.IO;

namespace OpenChords.Entities
{
	/// <summary>
	/// Description of Note.
	/// </summary>
	public class Note
	{

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public string notes {get; set;}
		public string songName {get; set;}
		
		public Note()
		{
		
		
		}
		

		public static Note loadNotes(string songName)
        {
            Note notesObject = new Note();
            notesObject.songName = songName;

            if (notesFileExists(songName))
            {
                notesObject.notes = FileReaderWriter.readFromFile(ExtAppsAndDir.notesFolder + songName);
            }
            else
            {
                notesObject.notes = "";
            }
            return notesObject;

        }
		
		public void saveNotes()
		{
			FileReaderWriter.writeToFile(ExtAppsAndDir.notesFolder + songName, notes);
		}
		
		
		private static bool notesFileExists(string songName)
		{
			string path = ExtAppsAndDir.notesFolder + songName;
			bool exists = FileFolderFunctions.isFilePresent(path);
			return exists;
		}

	}
}
