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
        


        //make the settings files if they dont exist
        private static void setupApplication()
        {
            var path = System.Web.Configuration.WebConfigurationManager.AppSettings["OpenChordsSettingsFilePath"];
            var fileAndFolderSettings = Entities.FileAndFolderSettings.loadSettings(path);
            OpenChords.Settings.setup(fileAndFolderSettings);
         
        }

        private static void setupTabletSettings(string settingsFilePath)
        {
            var tablet = Entities.DisplayAndPrintSettings.loadSettings(Entities.DisplayAndPrintSettingsType.TabletSettings, settingsFilePath);
            tablet.saveSettings();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            setupApplication();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var fileName = OpenChords.Web.App_Code.Global.SettingsFileName;
            setupTabletSettings(fileName);
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