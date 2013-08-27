using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenChords.Entities;
using OpenChords.Forms.Custom_Controls;


namespace OpenChords.Forms
{
    public partial class Settings : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private SettingsPanel     
        //private SettingsPanel     
        //private SettingsPanel     
        //private FileSettingsPanel 

        public Settings()
        {
            InitializeComponent();

            //tabPageFileSettings.Controls.Add(fileSettingsCustomControl);
            //tabPageDisplaySettings.Controls.Add(displaySettingsCustomControl);
            //tabPagePrintSettings.Controls.Add(printSettingsCustomControl);
            //tabPageTabletSettings.Controls.Add(tabletSettingsCustomControl);
        }




        private void Settings_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.guitar;
            displaySettingsCustomControl.loadSettings(DisplayAndPrintSettingsType.DisplaySettings);
            printSettingsCustomControl.loadSettings(DisplayAndPrintSettingsType.PrintSettings);
            tabletSettingsCustomControl.loadSettings(DisplayAndPrintSettingsType.TabletSettings);
            fileSettingsCustomControl.loadSettings();
        }

		
		
		
		
		
		
		void closeForm()
		{
            fileSettingsCustomControl.saveSettings();
         
            displaySettingsCustomControl.saveSettings();
            printSettingsCustomControl.saveSettings();
            tabletSettingsCustomControl.saveSettings();
           
			this.Dispose();
		}


        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeForm();
        }


        private void fileSettings_DoubleClick(object sender, MouseEventArgs e)
        {

        }

       



    }
}
