using System;
using Eto.Forms;
using Eto.Wpf;

namespace OpenChords.CrossPlatform.Wpf
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var wpfPlatform = new Platform();
            wpfPlatform.Add<WebView.IHandler>(() => new Controls.WebViewHandlerLite());
            new Application(wpfPlatform).Run(new frmMain());
        }
    }
}

