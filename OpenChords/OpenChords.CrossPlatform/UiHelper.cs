using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    public static class UiHelper
    {
        public static void ShowInFileManager(string songPath)
        {
            string fileManager = OpenChords.Settings.GlobalApplicationSettings.fileManager;

            bool isFile = File.Exists(songPath);
            string directoryPath = new FileInfo(songPath).DirectoryName;

            //get output path based off of if its a file or directory
            string outputPath = (isFile) ? songPath : directoryPath;

            //add quotes to output path
            outputPath = "\"" + outputPath + "\"";

            if (fileManager == @"C:\Windows\explorer.exe")
                System.Diagnostics.Process.Start(fileManager, "/select, " + outputPath);
            else
                System.Diagnostics.Process.Start(fileManager, outputPath);
        }
    }
}
