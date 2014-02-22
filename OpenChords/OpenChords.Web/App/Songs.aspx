<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Songs.aspx.cs" Inherits="OpenChords.Web.Songs" MasterPageFile="~/Master/OpenChordsMaster.Master" %>

<%@ Register Src="~/Controls/SongList.ascx" TagPrefix="uc1" TagName="SongList" %>


<asp:Content ContentPlaceHolderID="MainForm" runat="server">
    <uc1:SongList runat="server" ID="SongList" />
</asp:Content>