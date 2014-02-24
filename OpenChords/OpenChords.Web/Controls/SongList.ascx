<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SongList.ascx.cs" Inherits="OpenChords.Web.Controls.SongList" %>

<asp:Panel ID="pnlSongs" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows PanelWithSearchBox" GroupingText="Available Songs">
    <asp:ListView ID="lstViewSongs" runat="server" OnDataBinding="lstViewSongs_DataBinding" DataKeyNames="Name" OnSelectedIndexChanging="lstViewSongs_SelectedIndexChanging">
        <LayoutTemplate>
            <ul>
                <li runat="server" id="itemPlaceholder"></li>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="ListItemUnselected">
                <asp:LinkButton ID="lnkSets" runat="server" Text='<%#Eval("Name") %>' CommandName="Select"></asp:LinkButton>
            </li>
        </ItemTemplate>
        <SelectedItemTemplate>
            <li class="ListItemSelected">
                <asp:LinkButton ID="lnkSets" runat="server" Text='<%#Eval("Name") %>' CommandName="Select"></asp:LinkButton>
            </li>
        </SelectedItemTemplate>
    </asp:ListView>
    <br />
    <asp:Label ID="lblSearchSong" runat="server" Text="Search"></asp:Label>
    <asp:TextBox ID="txtSearchSong" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSong_TextChanged"></asp:TextBox>
    <asp:ImageButton ID="cmdAdvancedSearch" runat="server" OnClick="cmdAdvancedSearch_Click" SkinID="imgSearch" />
</asp:Panel>
