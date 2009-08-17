<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="FrancosCompensatorios.aspx.cs" Inherits="Solicitudes_FrancosCompensatorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc2" %>

<%@ Register src="../Controles/FrancosCompensatorios.ascx" tagname="FrancosCompensatorios" tagprefix="uc1" %>

<asp:Content ID="cFrancosCompensatorios" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:Panel ID="pnlFrancosCompensatorios" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                Solicitud de francos compensatorios
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Fecha
            </td>
            <td>
                <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                <asp:ImageButton ID="imgFecha" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                <cc1:CalendarExtender ID="ceFecha" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgFecha"
                    TargetControlID="txtFecha">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Debe ingresar la fecha."
                    ControlToValidate="txtFecha" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvFecha" runat="server" ErrorMessage="La fecha no es válida."
                    ControlToValidate="txtFecha" Display="None" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="solicitud"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Duración
            </td>
            <td>
                <asp:TextBox ID="txtDuracion" runat="server" CssClass="text_custom" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDuracion" runat="server" ErrorMessage="Debe ingresar la duración."
                    ControlToValidate="txtDuracion" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvDuracion" runat="server" ErrorMessage="La duración no es válida."
                    Display="None" ValidationGroup="solicitud" ControlToValidate="txtDuracion" Operator="DataTypeCheck"
                    Type="Currency"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Descripción
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Height="64px" Width="368px" TextMode="MultiLine"
                    CssClass="text_custom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                    ControlToValidate="txtDescripcion" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Consentimiento de
            </td>
            <td>
                <asp:TextBox ID="txtConsentimiento" runat="server" CssClass="text_custom" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConsentimiento" runat="server" ErrorMessage="Debe ingresar el consentimiento de."
                    ControlToValidate="txtConsentimiento" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAceptar" runat="server" onclick="Button1_Click" 
                    Text="Confirmar solicitud" ValidationGroup="solicitud" CssClass="button_custom" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="text_custom">
                    <asp:ValidationSummary ID="vsSolicitud" runat="server" ValidationGroup="solicitud" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 20px">
            </td>
        </tr>
    </table>
    </asp:Panel>
    <uc1:FrancosCompensatorios ID="ucFrancosCompensatorios" runat="server" Visible="false" Imprimible="true" />
</asp:Content>

