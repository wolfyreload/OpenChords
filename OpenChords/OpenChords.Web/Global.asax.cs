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
        private static void setup()
        {
            var path = System.Web.Configuration.WebConfigurationManager.AppSettings["OpenChordsSettingsFilePath"];
            var fileAndFolderSettings = Entities.FileAndFolderSettings.loadSettings(path);
            OpenChords.Settings.setup(fileAndFolderSettings);
            var tablet = new Entities.DisplayAndPrintSettings(Entities.DisplayAndPrintSettingsType.TabletSettings);
            tablet.saveSettings();
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