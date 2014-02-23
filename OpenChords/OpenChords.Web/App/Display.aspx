<%@ Page Title="" Language="C#" MasterPageFile="~/Master/OpenChordsDisplay.Master" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="OpenChords.Web.App.Display2" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PageName" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="PagingPanel" runat="server">
    <asp:Button ID="cmdGoBack" runat="server" OnClick="cmdGoBack_Click" CssClass="nice-button" Text="Back" />
    <asp:Button ID="cmdPreviousSong" runat="server" CssClass="nice-button" Text="Previous Song" OnClick="cmdPreviousSong_Click" />
    <asp:Button ID="cmdNextSong" runat="server" CssClass="nice-button" Text="Next Song" OnClick="cmdNextSong_Click" />
    <asp:Button ID="cmdKeyUp" runat="server" CssClass="nice-button" Text="Key Up" OnClick="cmdKeyUp_Click" />
    <asp:Button ID="cmdKeyDown" runat="server" CssClass="nice-button" Text="Key Down" OnClick="cmdKeyDown_Click" />
    <asp:Button ID="cmdCapoUp" runat="server" CssClass="nice-button" Text="Capo Up" OnClick="cmdCapoUp_Click" />
    <asp:Button ID="cmdCapoDown" runat="server" CssClass="nice-button" Text="Capo Down" OnClick="cmdCapoDown_Click" />


</asp:Content>
<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    <asp:label id="lblSongName" class="DisplaySongName" runat="server"></asp:label><br />
    <asp:label id="lblSongOrder" class="DisplaySongOrder" runat="server"></asp:label>
    <br />

    <div class="DisplayPage">
        <asp:literal id="litSongContent" runat="server" />
    </div>
</asp:Content>
