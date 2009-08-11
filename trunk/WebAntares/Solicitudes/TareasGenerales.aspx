<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TareasGenerales.aspx.cs"
    Inherits="Solicitudes_TareasGenerales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<asp:Content ID="cTareasGenerales" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="4">
                Solicitud de tareas generales
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Tipo
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cmbTipoTarea" runat="server" CssClass="text_custom">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Fecha de inicio
            </td>
            <td>
                <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                <asp:ImageButton ID="imgInicio" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                <cc1:CalendarExtender ID="ceInicio" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgInicio"
                    TargetControlID="txtInicio">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvInicio" runat="server" ErrorMessage="Debe ingresar la fecha de inicio."
                    ControlToValidate="txtInicio" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvInicio" runat="server" ErrorMessage="La fecha de inicio no es válida."
                    ControlToValidate="txtInicio" Display="None" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="solicitud"></asp:CompareValidator>
            </td>
            <td>
                Fecha de fin
            </td>
            <td>
                <asp:TextBox ID="txtFin" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                <asp:ImageButton ID="imgfin" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                <cc1:CalendarExtender ID="ceFin" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgFin"
                    TargetControlID="txtFin">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvFin" runat="server" ErrorMessage="Debe ingresar la fecha de fin."
                    ControlToValidate="txtFin" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvFin" runat="server" ErrorMessage="La fecha de fin no es válida."
                    ControlToValidate="txtFin" Display="None" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="solicitud"></asp:CompareValidator>
                <asp:CompareValidator ID="cvFechas" runat="server" ErrorMessage="La fecha de inicio debe ser menor o igual que la fecha de fin."
                    ControlToCompare="txtFin" ControlToValidate="txtInicio" Display="None" Operator="LessThanEqual"
                    Type="Date" ValidationGroup="solicitud"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Duración
            </td>
            <td colspan="3">
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
            <td colspan="3">
                <asp:TextBox ID="txtDescripcion" runat="server" Height="64px" Width="368px" TextMode="MultiLine"
                    CssClass="text_custom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                    ControlToValidate="txtDescripcion" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnAceptar" runat="server" Text="Confirmar solicitud" OnClick="btnAceptar_Click"
                    ValidationGroup="solicitud" CssClass="button_custom" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="text_custom">
                    <asp:ValidationSummary ID="vsSolicitud" runat="server" ValidationGroup="solicitud" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Content>
