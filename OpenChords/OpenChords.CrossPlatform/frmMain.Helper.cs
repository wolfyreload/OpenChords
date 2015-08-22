using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    public partial class frmMain
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
            //ensureValidFileSettingsFile();

            addIeEmulationModeRegistryKey();

            var fileSettings = Entities.FileAndFolderSettings.loadSettings();
            Settings.ExtAppsAndDir = new Config.ExtAppsAndDirClass(fileSettings);

            //ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.DisplaySettings);
            //ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.PrintSettings);
            //ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.TabletSettings);

            if (haveNoSongs() && haveNoSets())
            {
                DirectoryCopy("InitialData", Settings.ExtAppsAndDir.ApplicationDataFolder, true);
            }
        }

        private static void addIeEmulationModeRegistryKey()
        {
            if (Eto.Forms.Application.Instance.Platform.IsWinForms || Eto.Forms.Application.Instance.Platform.IsWpf)
            {
                // Setting
                RegistryKey registryKeyBase = Registry.CurrentUser;
                // I have to use CreateSubKey since open key is read only
                RegistryKey registrySubKey = registryKeyBase.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
                // Save the value
                string applicationExecutableName = System.AppDomain.CurrentDomain.FriendlyName;
                logger.Debug(applicationExecutableName);
                registrySubKey.SetValue(applicationExecutableName, 11001, RegistryValueKind.DWord);
            }
        }

        private static bool haveNoSets()
        {
            return new System.IO.DirectoryInfo(Settings.ExtAppsAndDir.SetsFolder).GetFiles().Length == 0;
        }

        private static bool haveNoSongs()
        {
            return new System.IO.DirectoryInfo(Settings.ExtAppsAndDir.SongsFolder).GetFiles().Length == 0;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        

    }

}
