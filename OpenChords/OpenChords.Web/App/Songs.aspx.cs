using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Songs : System.Web.UI.Page
    {
       
    


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SongList.DataBind();
            }
        }

        private void downloadSong(string songName)
        {

            var song = Entities.Song.loadSong(songName);
            var settingsPath = OpenChords.Web.App_Code.Global.SettingsFileName;
            var pdfPath = song.getPdfPath(Entities.DisplayAndPrintSettingsType.TabletSettings, settingsPath);


            Response.ClearContent();
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + songName + ".pdf");
            Response.TransmitFile(pdfPath);
            Response.Flush();

            Response.End();
        }


    }
}