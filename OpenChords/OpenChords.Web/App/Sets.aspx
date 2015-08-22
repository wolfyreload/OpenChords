<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sets.aspx.cs" Inherits="OpenChords.Web.Sets" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<%@ Register Src="~/Controls/SongList.ascx" TagPrefix="uc1" TagName="SongList" %>
<%@ Register Src="~/Controls/SetList.ascx" TagPrefix="uc1" TagName="SetList" %>
<%@ Register Src="~/Controls/SongsInSetList.ascx" TagPrefix="uc1" TagName="SongsInSetList" %>




<asp:Content ContentPlaceHolderID="PageName" runat="server">
    Edit Song Sets
</asp:Content>

<asp:Content ContentPlaceHolderID="PagingPanel" runat="server">
    <asp:Button ID="cmdGoBack" runat="server" OnClick="cmdGoBack_Click" CssClass="nice-button" Text="Back" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">

    <uc1:SetList runat="server" ID="SetList" OnSelectedSetChanged="SetList_SelectedSetChanged" />

    <uc1:SongList runat="server" ID="SongList" />

    <asp:Panel ID="pnlControls1" runat="server" CssClass="Inline SidewayButtonPane">
        <asp:ImageButton ID="imgAdd" runat="server" SkinID="imgNew" OnClick="imgAdd_Click" />
        <asp:ImageButton ID="imgDelete" runat="server" SkinID="imgDelete" OnClick="imgDelete_Click" />
    </asp:Panel>

    <uc1:SongsInSetList runat="server" ID="SongsInSetList" OnSelectedSongChanged="SongsInSetList_SelectedSongChanged" />
    <asp:Panel ID="pnlControls2" runat="server" CssClass="Inline SidewayButtonPane">
        <asp:ImageButton ID="imgSetItemUp" runat="server" SkinID="imgUp" OnClick="imgSetItemUp_Click" />
        <asp:ImageButton ID="imgSetItemDown" runat="server" SkinID="imgDown" OnClick="imgSetItemDown_Click" />
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlButtons" runat="server" CssClass="Inline">
        <asp:ImageButton ID="cmdCancel" runat="server" SkinID="imgCancel" OnClick="cmdCancel_Click" />
        <asp:ImageButton ID="cmdSave" runat="server" SkinID="imgSave" OnClick="cmdSave_Click" />
        <asp:ImageButton ID="imgHtml" runat="server" SkinID="imgHtml" OnClick="imgHtml_Click" />
    </asp:Panel>


</asp:Content>
