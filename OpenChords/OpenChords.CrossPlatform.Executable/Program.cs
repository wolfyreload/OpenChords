using System;
using Eto.Forms;

namespace OpenChords.CrossPlatform.Executable
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var runningPlatform = Helpers.DetectPlatform.GetRunningPlatform();
            if (runningPlatform == Helpers.DetectPlatform.Platform.Windows)
            {
                var wpfPlatform = new Eto.Wpf.Platform();
                wpfPlatform.Add<WebView.IHandler>(() => new Controls.WebViewHandlerLite());
                new Application(wpfPlatform).Run(new frmMain());
            }
            else if (runningPlatform == Helpers.DetectPlatform.Platform.Linux)
            {
                var gtkPlatform = new Eto.GtkSharp.Platform();
                new Application(gtkPlatform).Run(new frmMain());
            }
            else
            {
                Console.WriteLine("Unsupported Platform");
            }
        }

        
    }
}

