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
            if (!IsPostBack)
            {
                lstSets.DataBind();
            }
        }




        

        private void downloadSet(string query)
        {
            var songSet = query.Remove(0, 1);
            songSet = songSet.Replace("%20", " ");

            var set = Entities.Set.loadSet(songSet);
            var settingsPath = App_Code.Global.SettingsFileName;
            var pdfPath = set.getPdfPath(Entities.DisplayAndPrintSettingsType.TabletSettings, settingsPath);


            Response.ClearContent();
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + songSet + ".pdf");
            Response.TransmitFile(pdfPath);
            Response.Flush();

            Response.End();
        }

        protected void txtSearchSet_TextChanged(object sender, EventArgs e)
        {
            lstSets.DataBind();
        }

        

        protected void objSets_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var allSets = OpenChords.Entities.Set.listOfAllSets();
            var filter = txtSearchSet.Text.ToUpper();
            var filteredResults = allSets.Where(a => filter.Contains(a.ToUpper()));

            foreach (var res in  filteredResults)
            e.OutputParameters.Add(res, res);
        }
    }
}