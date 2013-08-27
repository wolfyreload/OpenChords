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


namespace OpenChords
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public class Program
	{

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
 

        //make the settings files if they dont exist
        private static void setup()
        {
            var defaultDisplaySettings = new Entities.DisplayAndPrintSettings(Entities.DisplayAndPrintSettingsType.DisplaySettings);
            var defaultPrintSettings = new Entities.DisplayAndPrintSettings(Entities.DisplayAndPrintSettingsType.PrintSettings);
            var defaultTabletSettings = new Entities.DisplayAndPrintSettings(Entities.DisplayAndPrintSettingsType.TabletSettings);

            if (!IO.FileFolderFunctions.isFilePresent(Settings.ExtAppsAndDir.displaySettingsFileName))
                defaultDisplaySettings.saveSettings();
            if (!IO.FileFolderFunctions.isFilePresent(Settings.ExtAppsAndDir.printSettingsFilename))
                defaultPrintSettings.saveSettings();
            if (!IO.FileFolderFunctions.isFilePresent(Settings.ExtAppsAndDir.tabletSettingsFilename))
                defaultTabletSettings.saveSettings();

            OpenChords.Entities.GlobalVariables.restartApplicationOnExit = false;
            OpenChords.Entities.GlobalVariables.patchApplicationOnExit = false;
        }


		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{

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
            if (OpenChords.Entities.GlobalVariables.restartApplicationOnExit)
                System.Diagnostics.Process.Start("UpgradeScript.bat", "restart");
            if (OpenChords.Entities.GlobalVariables.patchApplicationOnExit)
                System.Diagnostics.Process.Start("UpgradeScript.bat");


        }

        




	}
}
