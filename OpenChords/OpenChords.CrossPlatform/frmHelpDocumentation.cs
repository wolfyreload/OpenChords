using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    class frmHelpDocumentation : Form
    {
        WebView webView;

        public frmHelpDocumentation()
        {
            this.Width=1024;
            this.Height=768;
            this.Icon = Graphics.Icon;
            this.Title = "OpenChords Help Documentation";

            this.Content =  webView = new WebView();
            string manualHtmlFile = OpenChords.Settings.GlobalApplicationSettings.manual;
            //var uri = new Uri(manualHtmlFile);
            webView.Url = new Uri(manualHtmlFile);
        }



         
    }
}
