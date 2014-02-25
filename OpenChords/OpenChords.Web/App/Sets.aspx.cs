﻿using System;
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
                SetList.DataBind();
                hideSetDetails();
                if (SetName != null)
                {
                    SetList.SelectedSet = SetName;
                    showSetDetails();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        private string SetName
        { get { return (string)this.Request.Params["Set"]; } }


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

        
        private void showSetDetails()
        {
            
            SongList.DataBind();
            var currentSet = OpenChords.Entities.Set.loadSet(SetList.SelectedSet);
            SongsInSetList.SongsIncurrentSet = currentSet.songNames;
            SongsInSetList.DataBind();
            setUpAndDownButtons();
            pnlControls1.Visible = true;
            pnlControls2.Visible = true;
            SongsInSetList.Visible = true;
            SongList.Visible = true;
            pnlButtons.Visible = true;
        }

        private void hideSetDetails()
        {
            pnlControls1.Visible = false;
            pnlControls2.Visible = false;
            SongsInSetList.Visible = false;
            SongList.Visible = false;
            pnlButtons.Visible = false;
        }


        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            var selectedSong = SongList.SelectedValue;
            SongsInSetList.AddSong(selectedSong);
        }

        
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            var currentSet = OpenChords.Entities.Set.loadSet(SetList.SelectedSet);
            currentSet.clearSongSet();
            foreach (string songTitle in SongsInSetList.SongsIncurrentSet)
                currentSet.addSongToSet(songTitle);
            currentSet.saveSet();
            hideSetDetails();
        
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            SongsInSetList.RemoveSong();
        }

        protected void imgSetItemUp_Click(object sender, ImageClickEventArgs e)
        {
            SongsInSetList.MoveSongUp();
            setUpAndDownButtons();
        }

        protected void imgSetItemDown_Click(object sender, ImageClickEventArgs e)
        {
            SongsInSetList.MoveSongDown();
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


            var selectedIndex = SongsInSetList.SelectedIndex;
            if (SongsInSetList.Count <= 1 || selectedIndex == -1)
            {
                imgSetItemUp.Visible = false;
                imgSetItemDown.Visible = false;
            }
            else if (selectedIndex == 0)
            {
                imgSetItemUp.Visible = false;
                imgSetItemDown.Visible = true;
            }
            else if (selectedIndex >= SongsInSetList.Count - 1)
            {
                imgSetItemUp.Visible = true;
                imgSetItemDown.Visible = false;
            }

        }


        protected void cmdGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void exportToPdf_Click(object sender, ImageClickEventArgs e)
        {
            var currentSet = OpenChords.Entities.Set.loadSet(SetList.SelectedSet);
            currentSet.clearSongSet();
            foreach (string songTitle in SongsInSetList.SongsIncurrentSet)
                currentSet.addSongToSet(songTitle);
            downloadSet(currentSet);
        }

        protected void imgHtml_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Display.aspx?Set=" + SetList.SelectedSet);
        }

        protected void cmdCancel_Click(object sender, ImageClickEventArgs e)
        {
            hideSetDetails();
        }

        protected void SetList_SelectedSetChanged(object sender, EventArgs e)
        {
            showSetDetails();
        }

        protected void SongsInSetList_SelectedSongChanged(object sender, EventArgs e)
        {
            setUpAndDownButtons();
        }

        




    }
}