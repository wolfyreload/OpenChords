/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/14
 * Time: 09:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;


namespace OpenChords.IO
{
	/// <summary>
	/// Description of DirectoryListing.
	/// </summary>
	public static class FileFolderFunctions
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		/// <summary>
		/// get a listing of all the files in the selected folder
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static String[] getDirectoryListing (string path)
		{
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo (path);
			System.IO.FileInfo[] allFiles = dir.GetFiles ("*");
			
			int size = allFiles.GetLength (0);
			String[] list = new string[size];
			
			for (int i = 0; i < list.Length; i++) {
				list [i] = allFiles [i].Name;
			}
			
			return list;
		}

        public static bool IsRunningInVisualStudio()
        {
            var basePath = System.Windows.Forms.Application.StartupPath;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo (basePath);
            System.IO.FileInfo[] visualStudioFiles = dir.GetFiles("*.vshost.exe");
            if (visualStudioFiles.Length > 0)
                return true;
            else
                return false;
        }
		
        /// <summary>
        /// get the listing of all the files in the selected folder
        /// and return the results as a List
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		public static List<string> getDirectoryListingAsList (string path)
		{
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo (path);
			System.IO.FileInfo[] allFiles = dir.GetFiles ("*", System.IO.SearchOption.AllDirectories);
			
			//int size = allFiles.GetLength(0);
			List<string> list = new List<string> ();
			
			for (int i = 0; i < allFiles.Length; i++) {
                string songWithFullPath = allFiles[i].FullName;
                string songNameWithSubFolder = songWithFullPath.Replace(path, "");
                list.Add (songNameWithSubFolder);
			}
			
			return list;
		}

        /// <summary>
        /// delete all files in folder
        /// </summary>
        /// <param name="path"></param>
        public static void clearFolder(string path)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                {
                    logger.Error("failed to delete file " + file.FullName, ex);
                }
            }
        }
		
        /// <summary>
        /// checks if the file exists
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public static bool isFilePresent (string filename)
		{
			return System.IO.File.Exists (filename);
		}
		
        /// <summary>
        /// deletes a file
        /// </summary>
        /// <param name="filename"></param>
		public static void deleteFile (string filename)
		{
			if (isFilePresent (filename)) {
                logger.DebugFormat("try delete {0}", filename);
				try {
					System.IO.File.Delete (filename);
				} catch (Exception ex) 
                {
                    logger.Error("Error deleting file", ex);
				}
					
			}
			
		}
	}
}
