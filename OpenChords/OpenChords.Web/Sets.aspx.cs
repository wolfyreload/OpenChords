using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Sets : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            makeSetList();

            var query = Request.Url.Query;
            if (query.Length > 0)
            {
                downloadSet(query);

            }
        }




        private void makeSetList()
        {

            var listOfSets = Entities.Set.listOfAllSets();
            foreach (var song in listOfSets)
            {
                var link = new HyperLink()
                {
                    Text = song,
                    NavigateUrl = "~/Sets.aspx?" + song
                };
                pnlSets.Controls.Add(link);
                pnlSets.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        private void downloadSet(string query)
        {
            var songSet = query.Remove(0, 1);
            songSet = songSet.Replace("%20", " ");

            var set = Entities.Set.loadSet(songSet);
            var pdfPath = set.getPdfPath(Entities.DisplayAndPrintSettingsType.TabletSettings);


            Response.ClearContent();
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + songSet + ".pdf");
            Response.TransmitFile(pdfPath);
            Response.Flush();

            Response.End();
        }
    }
}