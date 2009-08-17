<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FrancosCompensatorios.ascx.cs" Inherits="Controles_FrancosCompensatorios" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
    <tr class="header_custom">
        <td align="center" colspan="3">
            Solicitud de francos compensatorios
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 20px">
        </td>
        <td rowspan="3" align="right" valign="top" style="padding-right:10px">
            <asp:Image ID="imgAntares" runat="server" AlternateText="Antares" ToolTip="Antares" ImageUrl="~/images/LogoAntaresThumbnail.png" />
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;width:150px">
            Número
        </td>
        <td>
            <asp:Literal ID="litNumero" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Título
        </td>
        <td colspan="2">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha
        </td>
        <td colspan="2">
            <asp:Literal ID="litFecha" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Duración
        </td>
        <td colspan="2">
            <asp:Literal ID="litDuracion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Descripción
        </td>
        <td colspan="2">
            <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Consentimiento de
        </td>
        <td colspan="2">
            <asp:Literal ID="litConsentimientoDe" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" 
                CssClass="button_custom" onclick="btnFinalizar_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px">
    </tr>
</table>
<asp:Panel ID="pnlImprimir" runat="server">
    <div style="margin: 20px;text-align: center">
        Sistema de gestión de tareas - V 1.0 - ANTARES COM S.A
    </div>
    <div style="margin: 20px;text-align: center">
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" 
            CssClass="button_custom" />
    </div>
</asp:Panel>