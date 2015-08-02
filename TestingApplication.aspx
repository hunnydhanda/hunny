<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingApplication.aspx.cs" Inherits="WebApplication1.TestingApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtnumone" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtnumtwo" runat="server"></asp:TextBox>
        <asp:Button ID="btnclick" runat="server" Text="multiply" OnClick="btnclick_Click" />
    </div>
        <asp:Label ID="lblmultiply" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>
