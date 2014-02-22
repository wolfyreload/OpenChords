<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sets.aspx.cs" Inherits="OpenChords.Web.Sets" MasterPageFile="~/Master/OpenChordsMaster.Master" %>


<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    
    <asp:Panel ID="pnlSets" runat="server" CssClass="BoxPanel" GroupingText="Sets">
        <asp:ListBox ID="lstSets" runat="server"></asp:ListBox>
        <asp:Label ID="lblSearchSet" runat="server" Text="Search"></asp:Label>
        <asp:TextBox ID="txtSearchSet" runat="server" AutoPostBack="true" OnTextChanged="txtSearchSet_TextChanged"></asp:TextBox>
        <asp:ObjectDataSource ID="objSets" runat="server" OnSelected="objSets_Selected"></asp:ObjectDataSource>
    </asp:Panel>
    
</asp:Content>