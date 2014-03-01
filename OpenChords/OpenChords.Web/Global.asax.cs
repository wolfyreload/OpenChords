using System;
using System.Collections.Generic;
using System.IO;
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
            path = getFullPath(path);
            
            
            var fileAndFolderSettings = Entities.FileAndFolderSettings.loadSettings(path);
            OpenChords.Settings.setup(fileAndFolderSettings);
         
        }

        private static string getFullPath(string path)
        {
            if (!path.Contains(":"))
            {
                var directoryPathOfApp = HttpContext.Current.Server.MapPath("~/");
                DirectoryInfo dirRootDirectory = new DirectoryInfo(directoryPathOfApp+path);
                return dirRootDirectory.FullName;
            }
            return path;
            
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