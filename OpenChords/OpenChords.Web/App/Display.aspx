<%@ Page Title="" Language="C#" MasterPageFile="~/Master/OpenChordsDisplay.Master" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="OpenChords.Web.App.Display2" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PageName" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PagingPanel" runat="server">
    <asp:Button ID="cmdPreviousSong" runat="server" CssClass="nice-button" Text="Previous" OnClick="cmdPreviousSong_Click" />
    <asp:Button ID="cmdNextSong" runat="server" CssClass="nice-button" Text="Next" OnClick="cmdNextSong_Click" />


</asp:Content>
<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    <asp:label id="lblSongName" class="DisplaySongName" runat="server"></asp:label><br />
    <asp:label id="lblSongOrder" class="DisplaySongOrder" runat="server"></asp:label>
    <br />

    <div class="DisplayPage">
        <asp:literal id="litSongContent" runat="server" />
    </div>
</asp:Content>
