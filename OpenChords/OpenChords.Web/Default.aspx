<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenChords.Web._Default" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">

    <div class="Centered">
        <label id="ApplicationTitle">OpenChords</label>
        <ul>
            <li>
                <asp:HyperLink ID="lnkSets" runat="server" NavigateUrl="~/App/Sets.aspx" CssClass="nice-button">Sets</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="lnkSongs" runat="server" NavigateUrl="~/App/Songs.aspx" CssClass="nice-button">Songs</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App/Settings.aspx" CssClass="nice-button">Settings</asp:HyperLink>
            </li>
        </ul>

    </div>

</asp:Content>
