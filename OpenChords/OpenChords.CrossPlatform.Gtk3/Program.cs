using System;
using Eto.Forms;

namespace OpenChords.CrossPlatform.Gtk3
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Gtk3).Run(new frmMain());
        }
    }
}

