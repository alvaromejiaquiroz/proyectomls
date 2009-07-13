<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal.aspx.cs" Inherits="Solicitudes_Personal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <script src="../Libs/jquery.pack.js" type="text/javascript"></script>
    <script src="../Libs/ui.core.packed.js" type="text/javascript"></script>
        <script src="../Libs/jquery.autocomplete.pack.js" type="text/javascript"></script>

    <script>
        $(document).ready(function() {
            $("#Buscar").autocomplete("../ajax/personas.ashx", {
                width: 260,
                selectFirst: false
            });
            $("#Buscar").result(function(event, data, formatted) {
                if (data) {
                    $("#Codigo").val(data[1]);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="Buscar" runat="server"></asp:TextBox>
        <asp:TextBox ID="Codigo" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
