<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenChords.Web._Default" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">

    <div class="Centered">
        <label id="ApplicationTitle">OpenChords</label>
        <br /><br /><br /><br /><br />
        
        <asp:Panel ID="pnlMenu" runat="server" Height="100%">
            <asp:HyperLink ID="lnkSets" runat="server" NavigateUrl="~/App/Sets.aspx" CssClass="nice-button">Sets</asp:HyperLink>
            <br />
            <br /><br />
            <asp:HyperLink ID="lnkSongs" runat="server" NavigateUrl="~/App/Songs.aspx" CssClass="nice-button">Songs</asp:HyperLink>
            <br />
            <br /><br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App/Settings.aspx" CssClass="nice-button">Settings</asp:HyperLink>
            <br /><br />
        </asp:Panel>

    </div>

</asp:Content>
