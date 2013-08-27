using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace OpenChords.Functions
{
    public class Updater
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string VERSION_ADDRESS = "http://openchords.sourceforge.net/version.txt";
        private static string UPDATE_PATH = "http://sourceforge.net/projects/openchords/files/latest/download";

        private delegate void Async(bool forceCheck);
        /// <summary>
        /// check if there is a new version of OpenChords online
        /// </summary>
        /// <param name="forceCheck"></param>
        public static void checkForNewVersion(bool forceCheck)
        {
            var settings = Entities.FileAndFolderSettings.loadSettings();
            if (settings.CheckForUpdates || forceCheck)
            {
                Async checker = new Async(checkForNewVersionAsync);
                checker.BeginInvoke(forceCheck, null, null);
            }
        }

        /// <summary>
        /// We dont want to block the main gui thread so we do do the check itself async
        /// </summary>
        /// <param name="forceCheck"></param>
        private static void checkForNewVersionAsync(bool forceCheck)
        {
            var sOnlineVersion = checkOnline();
            var sLocalVersion = Application.ProductVersion;
            var onlineVersion = getIntVersion(sOnlineVersion);
            var localVersion = getIntVersion(sLocalVersion);

            if (forceCheck && onlineVersion == 0)
                showMessageBox("Cannot access the update server");
            else if (forceCheck && onlineVersion <= localVersion)
                showMessageBox("You have the most recent version of OpenChords.");
            else if (onlineVersion > localVersion)
            {
                DialogResult result = showNewVersionDialog(sLocalVersion, sOnlineVersion);

                if (result == DialogResult.Yes)
                {
                    upgrade();
                }
                else if (forceCheck == false)
                {
                    DialogResult result2 = disableNewVersionNotificationsDialog();

                    if (result2 == DialogResult.Yes)
                        disableNewVersionNotifications();
                }

            }
                 


        }

        //upgrade to the newest version
        private static void upgrade()
        {
            try
            {
                //download the latest update
                OpenChords.Functions.Updater.downloadUpdate();

                //extract the update
                OpenChords.Functions.Updater.extractUpdate();
                DialogResult result = showUpgradeNowDialog();

                //write the upgrade script
                OpenChords.Functions.Updater.writeUpdateScript();

                if (result == DialogResult.Yes)
                {
                    OpenChords.Entities.GlobalVariables.restartApplicationOnExit = true;
                    Application.Exit();
                }
                else
                {
                    OpenChords.Entities.GlobalVariables.patchApplicationOnExit = true;
                }
            }
            catch
            {
                MessageBox.Show(null, "Upgrade failed, this is most likely due to a connection problem or the file host is down\r\nto try again go to Preferences>>General Settings>>Check Now", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        /// <summary>
        /// popup message box
        /// </summary>
        /// <param name="message"></param>
        private static void showMessageBox(string message)
        {
            MessageBox.Show(null, message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// remove all the dots between the versions and return an integer, to help with comparing the versions
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private static int getIntVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return 0;

            version = version.Replace(".", "");

            return int.Parse(version);

        }

        /// <summary>
        /// get the online version number
        /// </summary>
        /// <returns></returns>
        private static string checkOnline()
        {
            string onlineVersion = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(VERSION_ADDRESS);
                var response = request.GetResponse();

                byte[] byteResponse = new byte[7];
                response.GetResponseStream().Read(byteResponse, 0, 7);


                onlineVersion = System.Text.Encoding.UTF8.GetString(byteResponse);
            }
            catch (Exception ex) {
                logger.Error("Error checking online version number", ex);
            }

            return onlineVersion;
        }

        /// <summary>
        /// show yes/no dialog for getting the update
        /// </summary>
        /// <param name="yours"></param>
        /// <param name="theres"></param>
        /// <returns></returns>
        private static DialogResult showNewVersionDialog(string yours, string theres)
        {
            DialogResult result =
                MessageBox.Show(null,
                                String.Format("There is a newer version of OpenChords available for download. Would you like to upgrade the newest version? your version: {0} current version {1}", yours, theres),
                                "Upgrade?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            return result;
        }

        /// <summary>
        /// show yes/no dialog for getting the update
        /// </summary>
        /// <param name="yours"></param>
        /// <param name="theres"></param>
        /// <returns></returns>
        private static DialogResult showUpgradeNowDialog()
        {
            DialogResult result =
                MessageBox.Show(null,
                                String.Format("You will need to restart the application to complete the upgrade, would you like to restart now? Please remember to save any unsaved work first"),
                                "Information",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            return result;
        }

        /// <summary>
        /// show yes no dialog for disabling the update checker
        /// </summary>
        /// <returns></returns>
        private static DialogResult disableNewVersionNotificationsDialog()
        {
            DialogResult result =
                MessageBox.Show(null,
                                "Would you like to disable the update checker?",
                                "Disable?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            return result;
        }

        /// <summary>
        /// disable the new version notifications on startup
        /// </summary>
        private static void disableNewVersionNotifications()
        {
            var settings = Entities.FileAndFolderSettings.loadSettings();
            settings.CheckForUpdates = false;
            settings.saveSettings();
        }

        /// <summary>
        /// download the update from the internet
        /// </summary>
        /// <param name="httpPath"></param>
        /// <returns></returns>
        public static string downloadFile(string httpPath)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(httpPath);
            myHttpWebRequest.MaximumAutomaticRedirections = 5;
            myHttpWebRequest.AllowAutoRedirect = true;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var iStream = myHttpWebResponse.GetResponseStream();

            Encoding ANSI = Encoding.GetEncoding(1252);
            StreamReader reader = new StreamReader(iStream, ANSI);

            var result = reader.ReadToEnd();

            return result;
        }

        /// <summary>
        /// downloads the zip file with the update from the internet
        /// </summary>
        public static void downloadUpdate()
        {
            string fileContent = downloadFile(UPDATE_PATH);
            //string fileContent = downloadFile("http://openchords.sourceforge.net/OpenChords_1.2.7.zip"); //debug
            FileStream oStream = new FileStream("OpenChordsUpdate.zip", FileMode.Create);

            Encoding ANSI = Encoding.GetEncoding(1252);
            StreamWriter writer = new StreamWriter(oStream, ANSI);

            writer.AutoFlush = true;
            writer.Write(fileContent);
            
            oStream.Close();
        }

        /// <summary>
        /// write the batch script to update the application once it is closed
        /// </summary>
        public static void writeUpdateScript()
        {
            var script = @"
                @echo off 
                SET Param=%1
                ::Wait 2 seconds
                ping 1.1.1.1 -n 1 -w 2000 > nul
                
                ::First delete the config file
                DEL .\upgrade\OpenChords\App\Bin\*.xml
                ::Copy all other files
                COPY .\upgrade\OpenChords\App\Bin\* /Y
                ::Remove the upgrade folder
                RMDIR upgrade /S /Q
                
                ::Remove the upgrade file
                DEL OpenChordsUpdate.zip
                
                ::Restart openchords if desired
                IF DEFINED Param START OpenChords.exe

                ::Make the script delete itself
                DEL UpgradeScript.bat
                ";

            FileStream file = new FileStream("UpgradeScript.bat", FileMode.Create);
            StreamWriter writer = new StreamWriter(file);
            writer.AutoFlush = true;
            writer.Write(script);
            file.Close();

        }

        /// <summary>
        /// extract all the files in the bin folder from the new zip file 
        /// </summary>
        public static void extractUpdate()
        {
            try
            {
                Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile("OpenChordsUpdate.zip");

                zip.ExtractSelectedEntries("name = '*'", @"OpenChords\App\Bin", @".\upgrade", Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
            }
            catch { }
        }
    }
}
