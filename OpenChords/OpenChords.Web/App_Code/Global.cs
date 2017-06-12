using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenChords.Web.App_Code
{
    public class Global
    {
        public static string SettingsFileName
        {
            get
            {
                var settingsFileName = Guid.NewGuid().ToString() + ".xml";

                //create a cookie with the tablet settings filename for device thats connected
                if (HttpContext.Current.Request.Cookies["settingsFileName"] == null)
                {
                    HttpCookie settingsFile = new HttpCookie("settingsFileName", settingsFileName);
                    settingsFile.Expires = DateTime.Now.AddDays(90);
                    HttpContext.Current.Response.Cookies.Add(settingsFile);
                }
                //retrieve settings file on the filesystem
                else
                {
                    settingsFileName = HttpContext.Current.Request.Cookies["settingsFileName"].Value;
                }
                return OpenChords.Settings.GlobalApplicationSettings.SettingsFolder + settingsFileName;
            }
        }




    }
}