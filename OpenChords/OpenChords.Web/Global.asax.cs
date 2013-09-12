using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OpenChords.Web
{
    public class Global : System.Web.HttpApplication
    {
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
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            setup();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}