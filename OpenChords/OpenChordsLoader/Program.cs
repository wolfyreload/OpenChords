/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 05:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Linq;
using System.Threading;


namespace OpenChords
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    public class Program
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
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.DisplaySettings);
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.PrintSettings);
            ensureValidSettingsFile(Entities.DisplayAndPrintSettingsType.TabletSettings);

            //ensureValidFileSettingsFile();

            OpenChords.Entities.GlobalVariables.restartApplicationOnExit = false;
            OpenChords.Entities.GlobalVariables.patchApplicationOnExit = false;
        }

        /// <summary>
        /// check if portable mode is off. if it is we need to point to the app data folder for the current user
        /// </summary>
        private static void ensureValidFileSettingsFile()
        {
            var fileSettings = Entities.FileAndFolderSettings.loadSettings();
            if (!fileSettings.PortableMode)
            {
                var oldSettingsFolder = fileSettings.SettingsFolder;
                fileSettings.SettingsFolder = Application.UserAppDataPath + "\\";
                if (oldSettingsFolder != fileSettings.SettingsFolder)
                    fileSettings.saveSettings();
            }
        }

        private static void logErrors(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            logger.Error("unhandled and unknown error", exception);
            MessageBox.Show(exception.Message, "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void logErrors(object sender, ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;
            logger.Error("unhandled thread exception", exception);
            MessageBox.Show(exception.Message, "Unhandled thread exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            
            logger.Info("Starting Openchords");

            //make sure errors are not swallowed up when debugging
            if (!AppDomain.CurrentDomain.FriendlyName.EndsWith("vshost.exe"))
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(logErrors);
                // Set the unhandled exception mode to force all Windows Forms errors
                // to go through our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(logErrors);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            setup();

            //check for program updates
            OpenChords.Functions.Updater.checkForNewVersion(false);


            Application.ApplicationExit += Application_ApplicationExit;

            Application.Run(new Forms.EditorForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            logger.Debug("Closing OpenChords");
            if (OpenChords.Entities.GlobalVariables.restartApplicationOnExit)
            {
                logger.Debug("Shutting down app to upgrade with restart enabled");
                System.Diagnostics.Process.Start("UpgradeScript.bat", "restart");
            }
            if (OpenChords.Entities.GlobalVariables.patchApplicationOnExit)
            {
                logger.Debug("Shutting down app to run upgrade");
                System.Diagnostics.Process.Start("UpgradeScript.bat");
            }

        }






    }
}
