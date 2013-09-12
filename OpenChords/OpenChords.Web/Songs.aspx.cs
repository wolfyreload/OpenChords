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
            makeSongList();

            var query = Request.Url.Query;
            if (query.Length > 0)
            {
                downloadSong(query);
            }
        }

        private void makeSongList()
        {
            var listOfSongs = Entities.Song.listOfAllSongs();
            foreach (var song in listOfSongs)
            {
                var link = new HyperLink()
                {
                    Text = song,
                    NavigateUrl = "~/Songs.aspx?" + song
                };
                pnlSongs.Controls.Add(link);
                pnlSongs.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        private void downloadSong(string query)
        {
            var songClean = query.Remove(0, 1);
            songClean = songClean.Replace("%20", " ");

            var song = Entities.Song.loadSong(songClean);
            var pdfPath = song.getPdfPath(Entities.DisplayAndPrintSettingsType.TabletSettings);


            Response.ClearContent();
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + songClean + ".pdf");
            Response.TransmitFile(pdfPath);
            Response.Flush();

            Response.End();
        }


    }
}