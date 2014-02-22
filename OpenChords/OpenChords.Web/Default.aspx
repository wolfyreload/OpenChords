<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenChords.Web._Default" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    
    <div>
        <asp:HyperLink ID="lnkSets" runat="server" NavigateUrl="~/App/Sets.aspx">Sets</asp:HyperLink>
        <br />
        <asp:HyperLink ID="lnkSongs" runat="server" NavigateUrl="~/App/Songs.aspx">Songs</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App/Settings.aspx">Settings</asp:HyperLink>
    </div>

</asp:Content>
