﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenChords.Web._Default" MasterPageFile="~/Master/OpenChordsPublic.Master" %>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">

   
        
      <asp:Panel ID="pnlMenu" runat="server" Height="100%">
            <asp:HyperLink ID="lnkSets" runat="server" NavigateUrl="~/App/Sets.aspx" CssClass="menu nice-button">Sets</asp:HyperLink>
            <asp:HyperLink ID="lnkSongs" runat="server" NavigateUrl="~/App/Songs.aspx" CssClass="menu nice-button">Songs</asp:HyperLink>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App/Settings.aspx" CssClass="menu nice-button" Visible="false">Settings</asp:HyperLink>
            
        </asp:Panel>

 
</asp:Content>
