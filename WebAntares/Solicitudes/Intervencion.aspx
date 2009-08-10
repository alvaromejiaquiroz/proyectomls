<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Intervencion.aspx.cs" Inherits="_Default" %>
<%@ Register Src="../Controles/cboTipoSolicitud.ascx" TagName="cboTipoSolicitud"
    TagPrefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                Ingreso de solicitud de intervención
            </td>
        </tr>
        <tr>
            <td>
               Tipo de solicitud
            </td>
            <td>
                <uc1:cboTipoSolicitud ID="ucTipoSolicitud" runat="server" />
                <asp:CustomValidator ID="cvTipoSolicitud" runat="server" 
                    ErrorMessage="Debe seleccionar el tipo de solicitud." Display="None" 
                    ValidationGroup="intervencion" 
                    onservervalidate="cvTipoSolicitud_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                Título de referencia
            </td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Height="80px" Width="467px" TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ErrorMessage="Debe ingresar el título de referencia." Display="None" ControlToValidate="txtTitulo" ValidationGroup="intervencion"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Cargada por
            </td>
            <td>
                <asp:Label ID="lblUserLogueado" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Fecha
            </td>
            <td>
                <asp:Label ID="lblFechaCreacionSolicitud" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAceptar" runat="server" CssClass="button_custom" OnClick="btnAceptar_Click" Text="Siguiente" ValidationGroup="intervencion" />
            </td>
        </tr>
    </table>
    <div>
        <asp:ValidationSummary ID="vsIntervencion" runat="server" ValidationGroup="intervencion" />
    </div>
</asp:Content>
