<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetList.ascx.cs" Inherits="OpenChords.Web.Controls.SetList" %>

<asp:Panel ID="pnlSets" runat="server" CssClass="BoxPanel gradientBoxesWithOuterShadows PanelWithSearchBox" GroupingText="Sets">
    <asp:ListView ID="lstViewSets" runat="server" OnDataBinding="lstViewSets_DataBinding" DataKeyNames="Name" OnSelectedIndexChanging="lstViewSets_SelectedIndexChanging" OnSelectedIndexChanged="lstViewSets_SelectedIndexChanged">
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
    <asp:Label ID="lblSearchSet" runat="server" Text="Search"></asp:Label>
    <asp:TextBox ID="txtSearchSet" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSet_TextChanged"></asp:TextBox>
    <asp:ImageButton ID="cmdAdvancedSetSearch" runat="server" SkinID="imgSearch" />
</asp:Panel>

