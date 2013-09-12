<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Songs.aspx.cs" Inherits="OpenChords.Web.Songs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grdSongs" runat="server" EnableModelValidation="True" OnRowDataBound="grdSongs_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Songs">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSong" runat="server" CommandArgument="" OnClick="lnkSong_Click"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        Hello World

                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

       
    </div>
    </form>
</body>
</html>
