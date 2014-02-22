<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SongList.ascx.cs" Inherits="OpenChords.Web.Controls.SongList" %>

<asp:Panel ID="pnlSongs" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows" GroupingText="Available Songs">
        <asp:ListBox ID="lstSongs" CssClass="Listbox" runat="server" OnDataBinding="lstSongs_DataBinding" OnSelectedIndexChanged="lstSongs_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
        <br />
        <asp:Label ID="lblSearchSong" runat="server" Text="Search"></asp:Label>
        <asp:TextBox ID="txtSearchSong" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSong_TextChanged"></asp:TextBox>
        <asp:ImageButton ID="cmdAdvancedSearch" runat="server" OnClick="cmdAdvancedSearch_Click" SkinID="imgSearch" />
</asp:Panel>