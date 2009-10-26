<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Solicitudes_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
           .IE8Fix
           {
               z-index: 100;
           }
       </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal">
        <DynamicMenuStyle CssClass="IE8Fix" />
    </asp:Menu>
    <asp:Button runat="server" ID="btnSend" onclick="btnSend_Click" Text="mande"  /></asp:button>
    
    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
        <asp:ListItem>si</asp:ListItem>
        <asp:ListItem Selected="True">no</asp:ListItem>
    </asp:RadioButtonList>
    
</form>
</body>
</html>

