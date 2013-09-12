<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenChords.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="lnkSets" runat="server" NavigateUrl="~/Sets.aspx">Sets</asp:HyperLink>
        <br />
        <asp:HyperLink ID="lnkSongs" runat="server" NavigateUrl="~/Songs.aspx">Songs</asp:HyperLink>
    </div>
    </form>
</body>
</html>
