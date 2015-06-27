using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenChords.CrossPlatform
{
    class frmNewSet : Dialog
    {
        private TextBox txtSetName = new TextBox();
        private Button cmdOk = new Button() { Text = "Ok" };
     
        public string SetName { get { return txtSetName.Text;} }

        public frmNewSet()
        {
            Title = "New Set";
            this.DefaultButton = cmdOk;
            Content = new TableLayout()
            {
                Rows =
                {
                    new GroupBox() { Text = "Name of new set?", Content = txtSetName },
                    new TableRow(cmdOk)
                }
            };

            txtSetName.Focus();
            cmdOk.Click += cmdOk_Click;
        }

      
        void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
             
    }
}
