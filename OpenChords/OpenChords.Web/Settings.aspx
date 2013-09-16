<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="OpenChords.Web.Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Width <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox><br />
        Height <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox><br />

        Top Margin <asp:TextBox ID="txtTopMargin" runat="server"></asp:TextBox><br />
        Left Margin <asp:TextBox ID="txtLeftMargin" runat="server"></asp:TextBox><br />
        Bottom Margin <asp:TextBox ID="txtBottomMargin" runat="server"></asp:TextBox><br />
        Right Margin <asp:TextBox ID="txtRightMargin" runat="server"></asp:TextBox><br />
        
        Content Line Spacing <asp:TextBox ID="txtContentLineSpacing" runat="server"></asp:TextBox><br />
        Paragraph Spacing <asp:TextBox ID="txtParagraphSpacing" runat="server"></asp:TextBox><br />
        


        Show Dual Columns <asp:CheckBox id="chkDualColumns" runat="server" /><br />
        Show Notes <asp:CheckBox id="chkShowNotes" runat="server" /><br />
        Show Lyrics <asp:CheckBox ID="chkShowLyrics" runat="server" /><br />
        Show Chords <asp:CheckBox ID="chkShowChords" runat="server" /><br />
        
        Note Width <asp:TextBox ID="txtNoteWidth" runat="server"></asp:TextBox><br />

        Background Color <asp:TextBox ID="txtBackgroundColor" runat="server"></asp:TextBox><br />
        
        <asp:Panel ID="flowTitle" runat="server"></asp:Panel>
        <asp:Panel ID="flowOrder1" runat="server"></asp:Panel>
        <asp:Panel ID="flowOrder2" runat="server"></asp:Panel>
        <asp:Panel ID="flowHeadings" runat="server"></asp:Panel>
        <asp:Panel ID="flowChords" runat="server"></asp:Panel>
        <asp:Panel ID="flowLyrics" runat="server"></asp:Panel>
        <asp:Panel ID="flowNotes" runat="server"></asp:Panel>
        
        <br />
        <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" Text="Save" />


    </div>
    </form>
</body>
</html>
