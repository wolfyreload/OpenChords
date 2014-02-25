<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SongsInSetList.ascx.cs" Inherits="OpenChords.Web.Controls.SongsInSetList" %>

<asp:Panel ID="pnlSongsInSet" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows PanelWithoutSearchBox" GroupingText="Songs in Set">
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
        <EmptyDataTemplate>
            There are no songs in this set
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Panel>
