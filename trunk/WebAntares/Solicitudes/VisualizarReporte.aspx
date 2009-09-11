<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizarReporte.aspx.cs" Inherits="Solicitudes_VisualizarReporte" %>

<%@ Register src="../Controles/MantenimientoPreventivoRendicion.ascx" tagname="MantenimientoPreventivoRendicion" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Antares</title>
    <link href="~/App_Themes/antares/msvs_template_simple_tables.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" style="padding-top:20px">
                <uc1:MantenimientoPreventivoRendicion ID="ucMantenimientoPreventivoRendicion" runat="server" Imprimible="true" Visible="false" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
