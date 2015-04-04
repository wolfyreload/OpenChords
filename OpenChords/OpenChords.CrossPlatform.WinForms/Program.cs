using System;
using Eto.Forms;
using System.IO;

namespace OpenChords.CrossPlatform.WinForms
{
    public class Program
    {


        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.WinForms).Run(new frmMain());
        }
    }
}

