<%@ Page Title="" Language="C#" MasterPageFile="~/Master/OpenChordsMaster.Master" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="OpenChords.Web.App.Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PagingPanel" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainForm" runat="server">
    <asp:label id="lblSongName" class="DisplaySongName" runat="server"></asp:label><br />
    <asp:label id="lblSongOrder" class="DisplaySongOrder" runat="server"></asp:label>
    <br />

    <div class="DisplayPage">
        <asp:literal id="litSongContent" runat="server" />
    </div>
</asp:Content>
