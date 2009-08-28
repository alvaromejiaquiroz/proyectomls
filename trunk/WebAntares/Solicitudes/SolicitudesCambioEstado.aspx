<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="SolicitudesCambioEstado.aspx.cs" Inherits="Solicitudes_SolicitudesCambioEstado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc1" %>

<asp:Content ID="cCambioEstado" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="4">
                Cambio de estado de solicitud
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width:100px;padding-left: 20px">
                Estado
            </td>
            <td>
                <asp:DropDownList ID="cmbEstados" runat="server" CssClass="text_custom">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvEstados" runat="server" ErrorMessage="Debe seleccionar un estado."
                    ControlToValidate="cmbEstados" Display="None" Operator="NotEqual" ValueToCompare="-1"
                    ValidationGroup="estado"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" ValidationGroup="estado" CssClass="button_custom" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="text_custom">
                    <asp:ValidationSummary ID="vsEstado" runat="server" ValidationGroup="estado" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlReprogramado" runat="server" Visible="false">
                    <table cellpadding="1px" cellspacing="1px" border="0" width="100%">
                        <tr>
                            <td style="width:240px;padding-left: 20px">
                                Causa
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtCausa" runat="server" TextMode="MultiLine" Height="64px" Width="238px" CssClass="text_custom"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Debe ingresar la causa."
                                    ControlToValidate="txtCausa" Display="None" ValidationGroup="reprogramacion"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                Aprobador
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAprobador" runat="server" CssClass="text_custom" Width="236px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAprobador" runat="server" ErrorMessage="Debe ingresar el aprobador."
                                    ControlToValidate="txtAprobador" Display="None" ValidationGroup="reprogramacion"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                <asp:Literal ID="litFecha" runat="server"></asp:Literal>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtReprogramacion" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                <asp:ImageButton ID="imgReprogramacion" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                                <cc1:CalendarExtender ID="ceReprogramacion" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgReprogramacion"
                                    TargetControlID="txtReprogramacion">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvReprogramacion" runat="server"
                                    ControlToValidate="txtReprogramacion" Display="None" ValidationGroup="reprogramacion"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvReprogramacion" runat="server"
                                    ControlToValidate="txtReprogramacion" Display="None" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="reprogramacion"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr id="trFechas" runat="server">
                            <td style="padding-left: 20px">
                                Próxima fecha de realización desde
                            </td>
                            <td>
                                <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                <asp:ImageButton ID="imgInicio" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                                <cc1:CalendarExtender ID="ceInicio" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgInicio"
                                    TargetControlID="txtInicio">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvInicio" runat="server" ErrorMessage="Debe ingresar la próxima fecha de realización desde."
                                    ControlToValidate="txtInicio" Display="None" ValidationGroup="reprogramacion"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvInicio" runat="server" ErrorMessage="La próxima fecha de realización desde no es válida."
                                    ControlToValidate="txtInicio" Display="None" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="reprogramacion"></asp:CompareValidator>
                            </td>
                            <td>
                                Hasta
                            </td>
                            <td>
                                <asp:TextBox ID="txtFin" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                <asp:ImageButton ID="imgfin" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                                <cc1:CalendarExtender ID="ceFin" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgFin"
                                    TargetControlID="txtFin">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvFin" runat="server" ErrorMessage="Debe ingresar la próxima fecha de realización hasta."
                                    ControlToValidate="txtFin" Display="None" ValidationGroup="reprogramacion"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvFin" runat="server" ErrorMessage="La próxima fecha de realización hasta no es válida."
                                    ControlToValidate="txtFin" Display="None" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="reprogramacion"></asp:CompareValidator>
                                <asp:CompareValidator ID="cvFechas" runat="server" ErrorMessage="La próxima fecha de realización desde debe ser menor o igual que la próxima fecha de realización hasta."
                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" Display="None" Operator="LessThanEqual"
                                    Type="Date" ValidationGroup="reprogramacion"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnAceptarReprogramacion" runat="server" OnClick="btnAceptarReprogramacion_Click" CssClass="button_custom"
                                    Text="Aceptar" ValidationGroup="reprogramacion" />
                                <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" CssClass="button_custom"
                                    Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                        <td colspan="4">
                            <div class="text_custom">
                                <asp:ValidationSummary ID="vsReprogramacion" runat="server" ValidationGroup="reprogramacion" />
                            </div>
                        </td>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
