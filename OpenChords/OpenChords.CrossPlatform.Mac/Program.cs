using System;
using Eto.Forms;

namespace OpenChords.CrossPlatform.Mac
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Mac).Run(new frmMain());
        }
    }
}

