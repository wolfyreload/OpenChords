<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sets.aspx.cs" Inherits="OpenChords.Web.Sets" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<asp:Content ContentPlaceHolderID="PageName" runat="server">
    Configure Song Sets
</asp:Content>

<asp:Content ContentPlaceHolderID="PagingPanel" runat="server">
    <asp:Button ID="cmdGoBack" runat="server" OnClick="cmdGoBack_Click" CssClass="nice-button" Text="Back" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainForm" runat="server">

    <asp:Panel ID="pnlSets" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows" GroupingText="Sets">
        <asp:ListBox ID="lstSets" CssClass="SetList" runat="server" OnDataBinding="lstSets_DataBinding" OnSelectedIndexChanged="lstSets_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
        <br />
        <asp:Label ID="lblSearchSet" runat="server" Text="Search"></asp:Label>
        <asp:TextBox ID="txtSearchSet" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSet_TextChanged"></asp:TextBox>
    </asp:Panel>

    <asp:Panel ID="pnlSetContents" Visible="false" runat="server" CssClass="Inline">
        <asp:Panel ID="pnlSongs" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows" GroupingText="Available Songs">
            <asp:ListBox ID="lstSongs" CssClass="SongList" runat="server" OnDataBinding="lstSongs_DataBinding"></asp:ListBox>
            <br />
            <asp:Label ID="lblSearchSong" runat="server" Text="Search"></asp:Label>
            <asp:TextBox ID="txtSearchSong" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSong_TextChanged"></asp:TextBox>
            <asp:ImageButton ID="cmdAdvancedSearch" runat="server" OnClick="cmdAdvancedSearch_Click" SkinID="imgSearch" />
        </asp:Panel>

        <asp:Panel ID="pnlControls1" runat="server" CssClass="Inline SidewayButtonPane">
            <asp:ImageButton ID="imgAdd" runat="server" SkinID="imgNew" OnClick="imgAdd_Click" />
            <asp:ImageButton ID="imgDelete" runat="server" SkinID="imgDelete" OnClick="imgDelete_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlSongsInSet" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows" GroupingText="Songs In Set">
            <asp:ListBox ID="lstSongsInSet" CssClass="SongList" runat="server" OnDataBinding="lstSongsInSet_DataBinding" OnSelectedIndexChanged="lstSongsInSet_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
            <br />
            <asp:ImageButton ID="cmdCancel" runat="server" SkinID="imgCancel" OnClick="cmdCancel_Click" />
            <asp:ImageButton ID="cmdSave" runat="server" SkinID="imgSave" OnClick="cmdSave_Click" />

        </asp:Panel>
        <asp:Panel ID="pnlControls2" runat="server" CssClass="Inline SidewayButtonPane">
            <asp:ImageButton ID="imgSetItemUp" runat="server" SkinID="imgUp" OnClick="imgSetItemUp_Click" />
            <asp:ImageButton ID="imgSetItemDown" runat="server" SkinID="imgDown" OnClick="imgSetItemDown_Click" />
        </asp:Panel>
    </asp:Panel>

</asp:Content>
