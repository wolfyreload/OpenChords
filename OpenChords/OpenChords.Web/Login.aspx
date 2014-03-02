<%@ Page Title="" Language="C#" MasterPageFile="~/Master/OpenChordsPublic.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OpenChords.Web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageName" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PagingPanel" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainForm" runat="server">

    <div class="LoginForm">
    <label>Username</label>
    <asp:TextBox id="txtUserName" runat="server" />
    <br />
    
    <label>Password</label>    
    <asp:TextBox id="txtPassword" TextMode="Password" runat="server" />
    <br />
    <asp:Button ID="cmdLogin" runat="server" CssClass="nice-button" Text="Login" OnClick="cmdLogin_Click" />
    </div>
    <asp:Label ID="lblError" runat="server" Text="Invalid username or password" Visible="false"></asp:Label><br />
</asp:Content>
