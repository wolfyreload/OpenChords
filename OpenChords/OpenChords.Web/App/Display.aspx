<%@ Page Title="" Language="C#" MasterPageFile="~/Master/OpenChordsMaster.Master" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="OpenChords.Web.App.Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PagingPanel" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainForm" runat="server">
    <asp:Label ID="lblSongName" runat="server"></asp:Label>
    <asp:Label ID="lblSongOrder" runat="server"></asp:Label>
    <br />
    
    <asp:Literal ID="litSongContent" runat="server" />
        
</asp:Content>
