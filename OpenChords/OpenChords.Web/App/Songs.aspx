<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Songs.aspx.cs" Inherits="OpenChords.Web.Songs" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<%@ Register Src="~/Controls/SongList.ascx" TagPrefix="uc1" TagName="SongList" %>
<%@ Register Src="~/Controls/SongMetaData.ascx" TagPrefix="uc1" TagName="SongMetaData" %>


<asp:Content ContentPlaceHolderID="PageName" runat="server">
    Edit Songs
</asp:Content>

<asp:Content ContentPlaceHolderID="PagingPanel" runat="server">
    <asp:Button ID="cmdGoBack" runat="server" OnClick="cmdGoBack_Click" CssClass="nice-button" Text="Back" />
</asp:Content>


<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    <uc1:SongList runat="server" ID="SongList" OnNewSongSelected="SongList_NewSongSelected" />
    <asp:Panel ID="pnlSongEdit" runat="server" CssClass="Inline SongEditPanel" Visible="false">
        <uc1:SongMetaData runat="server" ID="SongMetaData" OnPresentationOrderChanged="SongMetaData_PresentationOrderChanged" OnSongKeyIncreased="SongMetaData_SongKeyIncreased" OnSongKeyDecreased="SongMetaData_SongKeyDecreased" OnSongCapoDecreased="SongMetaData_SongCapoDecreased" OnSongCapoIncreased="SongMetaData_SongCapoIncreased" />
        <br />
        <br />
        <asp:Panel ID="pnlLyrics" runat="server" GroupingText="Chords and lyrics" CssClass="BoxPanel Inline gradientBoxesWithOuterShadows SongEditLyricsPanel">
            <asp:TextBox ID="txtSongEditLyrics" CssClass="SongEditLyrics" runat="server" TextMode="MultiLine">

            </asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnlNotes" runat="server" GroupingText="Notes" CssClass="BoxPanel Inline gradientBoxesWithOuterShadows  SongEditNotesPanel">
            <asp:TextBox ID="txtSongEditNotes" CssClass="SongEditNotes" runat="server" TextMode="MultiLine">

            </asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnlControls1" runat="server" CssClass="Inline">
            <asp:ImageButton ID="imgCancel" runat="server" SkinID="imgCancel" OnClick="imgCancel_Click" />
            <asp:ImageButton ID="imgSave" runat="server" SkinID="imgSave" OnClick="imgSave_Click" />
            <asp:ImageButton ID="imgPdf" runat="server" SkinID="imgPdf" OnClick="imgPdf_Click" />
            <asp:ImageButton ID="imgHtml" runat="server" SkinID="imgHtml" OnClick="imgHtml_Click" />
        </asp:Panel>

    </asp:Panel>
</asp:Content>
