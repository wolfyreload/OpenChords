using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web.App
{
    public partial class Display : System.Web.UI.Page
    {

        private string SongName
        { get { return (string)this.Request.Params["Song"]; } }

        private string SetName
        { get { return (string)this.Request.Params["Set"]; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var settingsPath = App_Code.Global.SettingsFileName;
                
                if (SongName != null)
                {
                    var song = OpenChords.Entities.Song.loadSong(SongName);
                    var html = song.getHtml(settingsPath);
                    lblSongName.Text = html[0].Name;
                    lblSongOrder.Text = html[0].Order;
                    litSongContent.Mode = LiteralMode.PassThrough;
                    litSongContent.Text = html[0].Html;
                }
            }
        }
    }
}