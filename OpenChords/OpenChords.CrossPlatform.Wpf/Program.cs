using System;
using Eto.Forms;

namespace OpenChords.CrossPlatform.Wpf
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Wpf).Run(new frmMain());
        }
    }
}

