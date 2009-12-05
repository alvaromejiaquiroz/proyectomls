<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Intervencion.aspx.cs" Inherits="_Default" %>

<%@ Register Src="../Controles/cboTipoSolicitud.ascx" TagName="cboTipoSolicitud"     TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 2px">
        <tr class="header_custom">
            <td align="center" colspan="4">
               <p class="Titulo_3"> Ingresar Solicitud de Intervención</p>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
            </td>
        </tr>
        <tr>
            <td style="padding-left:20px">
                Tipo de solicitud
            </td>
            <td>
                <asp:DropDownList ID="cmbTipoSolicitud" runat="server" CssClass="text_custom" Width="250px">
                </asp:DropDownList>
                <asp:CustomValidator ID="cvTipoSolicitud" runat="server" ErrorMessage="Debe seleccionar el tipo de solicitud."
                    Display="None" ValidationGroup="intervencion" OnServerValidate="cvTipoSolicitud_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left:20px">
                Título de referencia
            </td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Height="80px" Width="467px" TextMode="MultiLine"
                    CssClass="text_custom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ErrorMessage="Debe ingresar el título de referencia."
                    Display="None" ControlToValidate="txtTitulo" ValidationGroup="intervencion"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left:20px">
                Cargada por
            </td>
            <td>
                <asp:Label ID="lblUserLogueado" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-left:20px">
                Fecha
            </td>
            <td>
                <asp:Label ID="lblFechaCreacionSolicitud" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAceptar" runat="server" CssClass="button_custom" OnClick="btnAceptar_Click"
                    Text="Siguiente" ValidationGroup="intervencion" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="vsIntervencion" runat="server" ValidationGroup="intervencion" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Content>
