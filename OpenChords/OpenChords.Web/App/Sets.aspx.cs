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

        public List<string> _songsIncurrentSet
        {
            get { return (List<string>)(this.ViewState["CurrentSet"]); }
            set { this.ViewState["CurrentSet"] = value; }
        }
        

        private void downloadSet(OpenChords.Entities.Set set)
        {
            var settingsPath = App_Code.Global.SettingsFileName;
            var pdfPath = set.getPdfPath(Entities.DisplayAndPrintSettingsType.TabletSettings, settingsPath);


            Response.ClearContent();
            Response.Clear();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + set.setName + ".pdf");
            Response.TransmitFile(pdfPath);
            Response.Flush();

            Response.End();
        }

        protected void txtSearchSet_TextChanged(object sender, EventArgs e)
        {
            lstSets.DataBind();
        }

        protected void lstSets_DataBinding(object sender, EventArgs e)
        {
            var allSets = OpenChords.Entities.Set.listOfAllSets();

            var filter = txtSearchSet.Text.ToUpper();
            if (!string.IsNullOrEmpty(filter))
            {
                allSets = allSets.Where(a => a.ToUpper().Contains(filter)).ToList();
            }

            lstSets.DataSource = allSets;    
        }

        protected void lstSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentSet = OpenChords.Entities.Set.loadSet(lstSets.SelectedValue);
            _songsIncurrentSet = currentSet.songList.Select(s => s.title).ToList();
            SongList.DataBind();
            lstSongsInSet.DataBind();
            setUpAndDownButtons();
            pnlSetContents.Visible = true;
        }

        
        protected void lstSongsInSet_DataBinding(object sender, EventArgs e)
        {
            lstSongsInSet.DataSource = _songsIncurrentSet;
        }

        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            var selectedSong = SongList.SelectedValue;
            _songsIncurrentSet.Add(selectedSong);
            lstSongsInSet.DataBind();
            lstSongsInSet.SelectedIndex = lstSongsInSet.Items.Count - 1;
        }

        protected void cmdCancel_Click(object sender, ImageClickEventArgs e)
        {
            pnlSetContents.Visible = false;
            lstSets.SelectedIndex = -1;
        }

        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            var currentSet = OpenChords.Entities.Set.loadSet(lstSets.SelectedValue);
            currentSet.clearSongSet();
            foreach (string songTitle in _songsIncurrentSet)
                currentSet.addSongToSet(songTitle);
            currentSet.saveSet();
            pnlSetContents.Visible = false;
            lstSets.SelectedIndex = -1;
           
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            
            var selectedIndex = lstSongsInSet.SelectedIndex;
            if (selectedIndex == -1) return;

            _songsIncurrentSet.RemoveAt(selectedIndex);
                       
            lstSongsInSet.DataBind();
            if (lstSongsInSet.Items.Count == 0) return;

            selectedIndex--;
            if (selectedIndex == -1) selectedIndex = 0;
            lstSongsInSet.SelectedIndex = selectedIndex;
        }

        protected void imgSetItemUp_Click(object sender, ImageClickEventArgs e)
        {
            var selectedIndex = lstSongsInSet.SelectedIndex;
            var item = _songsIncurrentSet[selectedIndex];
            _songsIncurrentSet.RemoveAt(selectedIndex);
            selectedIndex--;
            _songsIncurrentSet.Insert(selectedIndex, item);
            lstSongsInSet.DataBind();
            lstSongsInSet.SelectedIndex = selectedIndex;
            setUpAndDownButtons();
        }

        protected void imgSetItemDown_Click(object sender, ImageClickEventArgs e)
        {
            var selectedIndex = lstSongsInSet.SelectedIndex;
            var item = _songsIncurrentSet[selectedIndex];
            _songsIncurrentSet.RemoveAt(selectedIndex);
            selectedIndex++;
            _songsIncurrentSet.Insert(selectedIndex, item);
            lstSongsInSet.DataBind();
            lstSongsInSet.SelectedIndex = selectedIndex;
            setUpAndDownButtons();
        }

        protected void lstSongsInSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUpAndDownButtons();
        }

        private void setUpAndDownButtons()
        {
            imgSetItemUp.Visible = true;
            imgSetItemDown.Visible = true;
            
            var selectedIndex = lstSongsInSet.SelectedIndex;
            if (lstSongsInSet.Items.Count <= 1 || selectedIndex == -1)
            {
                imgSetItemUp.Visible = false;
                imgSetItemDown.Visible = false;
            }
            else if (selectedIndex == 0)
            {
                imgSetItemUp.Visible = false;
                imgSetItemDown.Visible = true;
            }
            else if (selectedIndex >= lstSongsInSet.Items.Count - 1)
            {
                imgSetItemUp.Visible = true;
                imgSetItemDown.Visible = false;
            }
            
        }

        
        protected void cmdGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void cmdAdvancedSetSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void exportToPdf_Click(object sender, ImageClickEventArgs e)
        {
            var currentSet = OpenChords.Entities.Set.loadSet(lstSets.SelectedValue);
            currentSet.clearSongSet();
            foreach (string songTitle in _songsIncurrentSet)
                currentSet.addSongToSet(songTitle);
            downloadSet(currentSet);
        }



    
      
    }
}