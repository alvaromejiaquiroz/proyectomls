<%@ Page Language="C#" AutoEventWireup="true" CodeFile="excel.aspx.cs" Inherits="excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    <asp:LinkButton runat="server" id="lkn" onclick="lkn_Click" >exportar a excel</asp:LinkButton>
    <br />
    
    <asp:GridView ID="gv" runat="server" ></asp:GridView>
    </div>
    
    <asp:DropDownList ID="cboSitios" runat="server" 
        CssClass="text_custom">
    </asp:DropDownList>
    </form>
</body>
</html>
