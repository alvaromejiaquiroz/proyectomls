<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisualizarSolicitud.aspx.cs" Inherits="Solicitudes_VisualizarSolicitud" %>

<%@ Register src="../Controles/MantenimientoPreventivo.ascx" tagname="MantenimientoPreventivo" tagprefix="uc1" %>
<%@ Register src="../Controles/MantenimientoCorrectivo.ascx" tagname="MantenimientoCorrectivo" tagprefix="uc2" %>
<%@ Register src="../Controles/Obras.ascx" tagname="Obras" tagprefix="uc3" %>
<%@ Register src="../Controles/Capacitacion.ascx" tagname="Capacitacion" tagprefix="uc4" %>

<%@ Register src="../Controles/Licencias.ascx" tagname="Licencias" tagprefix="uc5" %>

<%@ Register src="../Controles/TareasGenerales.ascx" tagname="TareasGenerales" tagprefix="uc6" %>

<%@ Register src="../Controles/FrancosCompensatorios.ascx" tagname="FrancosCompensatorios" tagprefix="uc7" %>

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
                <uc1:MantenimientoPreventivo ID="ucMantenimientoPreventivo" runat="server" Imprimible="true" Visible="false" />
                <uc2:MantenimientoCorrectivo ID="ucMantenimientoCorrectivo" runat="server" Imprimible="true" Visible="false" />
                <uc3:Obras ID="ucObras" runat="server" Imprimible="true" Visible="false" />
                <uc4:Capacitacion ID="ucCapacitacion" runat="server" Imprimible="true" Visible="false" />
                <uc5:Licencias ID="ucLicencias" runat="server" Imprimible="true" Visible="false" />                
                <uc6:TareasGenerales ID="ucTareasGenerales" runat="server" Imprimible="true" Visible="false" />
                <uc7:FrancosCompensatorios ID="ucFrancosCompensatorios" runat="server" Imprimible="true" Visible="false" />
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
