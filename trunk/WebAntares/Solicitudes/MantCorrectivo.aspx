<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantCorrectivo.aspx.cs" Inherits="Solicitudes_MantCorrectivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/MantenimientoCorrectivo.ascx" tagname="MantenimientoCorrectivo" tagprefix="uc1" %>
<asp:Content ID="cMantenimientoCorrectivo" ContentPlaceHolderID="PageContainer" runat="Server">
    <asp:Panel ID="pnlMantenimientoCorrectivo" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitud de mantenimiento correctivo
            </td>
        </tr>
        <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="tcMantenimientoPreventivo" runat="server" Height="450px">
                    <cc1:TabPanel ID="tpFalla" HeaderText="Falla" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Reportó falla
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReportoFalla" runat="server" CssClass="text_custom" Width="250px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReportoFalla" runat="server" ErrorMessage="Debe ingresar quien reportó la falla."
                                            ControlToValidate="txtReportoFalla" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Causa probable
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCausa" runat="server" Height="64px" Width="470px" TextMode="MultiLine"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Debe ingresar la causa probable de la falla."
                                            ControlToValidate="txtCausa" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Fecha de reporte
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaReporte" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                        <asp:ImageButton ID="imgFechaReporte" runat="server" CausesValidation="False" 
                                            ImageUrl="~/images/calendar.png" />
                                        <cc1:CalendarExtender ID="ceFechaReporte" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgFechaReporte"
                                            TargetControlID="txtFechaReporte" Enabled="True">
                                        </cc1:CalendarExtender>
                                        <asp:DropDownList ID="ddlHoraReporte" runat="server" CssClass="text_custom">
                                            <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMinutosReporte" runat="server" CssClass="text_custom">
                                            <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                            <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                            <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                            <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                            <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                            <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                            <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                            <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                            <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                            <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                            <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                            <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                            <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                            <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                            <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                            <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                            <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                            <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                            <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                            <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                            <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                            <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                            <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                            <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                            <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                            <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFechaReporte" runat="server" ErrorMessage="Debe ingresar la fecha de reporte de la falla."
                                            ControlToValidate="txtFechaReporte" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvFechaReporte" runat="server" ErrorMessage="La fecha de reporte de la falla no es válida."
                                            ControlToValidate="txtFechaReporte" Display="None" Operator="DataTypeCheck" Type="Date"
                                            ValidationGroup="solicitud"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Falla
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFalla" runat="server" Height="64px" Width="470px" TextMode="MultiLine"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFalla" runat="server" ErrorMessage="Debe ingresar la falla."
                                            ControlToValidate="txtFalla" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpServicios" HeaderText="Servicios" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upServicios" runat="server">
                                <ContentTemplate>
                                    <table class="text_custom" width="600px">
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="padding-left: 20px">
                                                Servicios
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstServiciosAfectados" runat="server" Height="141px" SelectionMode="Multiple"
                                                    CssClass="text_custom"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btnAsignaServicio" runat="server" Text="Asignar servicio" OnClick="btnAsignaServicio_Click"
                                                        CssClass="button_custom" ValidationGroup="servicios" />
                                                    <asp:CustomValidator ID="cvServiciosAfectados" runat="server" ErrorMessage="Debe seleccionar al menos un servicio."
                                                        Display="None" ValidationGroup="servicios" OnServerValidate="cvServiciosAfectados_ServerValidate"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvServicios_RowDeleting"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                            <asp:BoundField DataField="IdServicioAfectado" HeaderText="IdServicioAfectado" Visible="false" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                        ImageUrl="~/Images/delete.gif" Text="Eliminar" ToolTip="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                    <div class="text_custom">
                                        <asp:ValidationSummary ID="vsServicios" runat="server" ValidationGroup="servicios" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpPlazo" HeaderText="Plazo" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Plazo de atención
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbPlazoAtencion" runat="server" CssClass="text_custom">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpPersonal" HeaderText="Personal" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upPersonas" runat="server">
                                <ContentTemplate>
                                    <table width="600px" class="text_custom">
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px">
                                                Responsable
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbResponsable_SelectedIndexChanged"
                                                    CssClass="text_custom">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px">
                                                <asp:Literal ID="litPersonal" runat="server" Text="Personal" Visible="false"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstEmpleadosSolicitud" runat="server" SelectionMode="Multiple" Height="150px"
                                                    Visible="false" CssClass="text_custom"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" Text="Asignar empleado"
                                                    OnClick="btnAsignaEmpleadoSolicitud_Click" Visible="false" CssClass="button_custom" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvSolicitudPersonas" runat="server" AutoGenerateColumns="False"
                                                    OnRowDeleting="gvPersonas_RowDeleting" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:CheckBoxField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Images/delete.gif" Text="Delete" ToolTip="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpVehiculos" HeaderText="Vehículos" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upVehiculos" runat="server">
                                <ContentTemplate>
                                    <table width="600px" class="text_custom">
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="padding-left: 20px">
                                                Vehículos
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" Height="150px"
                                                    CssClass="text_custom"></asp:ListBox>
                                                <asp:CustomValidator ID="cvVehiculos" runat="server" ErrorMessage="Debe seleccionar al menos un vehículo."
                                                    Display="None" ValidationGroup="vehiculos" OnServerValidate="cvVehiculos_ServerValidate"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" Text="Asignar vehículo"
                                                    OnClick="btnAsignaVehiculoSolicitud_Click" ValidationGroup="vehiculos" CssClass="button_custom" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False"
                                                    OnRowDeleting="gvSolicitudVehiculos_RowDeleting" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" Visible="False" />
                                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Images/delete.gif" Text="Delete" ToolTip="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="text_custom">
                                        <asp:ValidationSummary ID="vsVehiculos" runat="server" ValidationGroup="vehiculos" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpComplemento" HeaderText="Complemento" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Cliente
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbClientes" runat="server" CssClass="text_custom">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Contacto de cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactoCliente" runat="server" Width="210px" ValidationGroup="solicitud"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactoCliente" runat="server" ErrorMessage="Debe ingresar el contacto de cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtContactoCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Nro. de orden
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px" ValidationGroup="solicitud"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNroOrdenCliente" runat="server" ErrorMessage="Debe ingresar el nro. de orden del cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtNroOrdenCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Teléfono de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px" ValidationGroup="solicitud"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefonoContacto" runat="server" ErrorMessage="Debe ingresar el teléfono de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtTelefonoContacto"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        E-mail de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMailContacto" runat="server" Width="210px" ValidationGroup="solicitud"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMailContacto" runat="server" ErrorMessage="Debe ingresar el e-mail de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMailContacto"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMailContacto" runat="server" ErrorMessage="El e-mail de contacto no es válido"
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMailContacto"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
                        <ContentTemplate>
                            <div style="width: 600px">
                                <uc4:Adjuntos ID="ucAdjuntos" runat="server" TipoSol="MantenimientoPreventivo" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Monto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPresupuesto" runat="server" ValidationGroup="solicitud" MaxLength="12"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPresupuesto" runat="server" ErrorMessage="Debe ingresar el monto del presupuesto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtPresupuesto"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvPresupuesto" runat="server" ErrorMessage="El monto del presupuesto no es válido."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtPresupuesto"
                                            Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                        <ContentTemplate>
                            <div style="width:100%;text-align:center;margin-top:30px">
                                <asp:Button ID="btnAceptarSolicitud" runat="server" OnClick="btnAceptarSolicitud_Click"
                                    Text="Confirmar solicitud" ValidationGroup="solicitud" CssClass="button_custom" />
                            </div>
                            <div class="text_custom">
                                <asp:BulletedList ID="blErrores" runat="server" ForeColor="Red" BulletStyle="NotSet">
                                </asp:BulletedList>
                                <asp:ValidationSummary ID="vsSolicitud" runat="server" ValidationGroup="solicitud" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <uc1:MantenimientoCorrectivo ID="ucMantenimientoCorrectivo" runat="server" Visible="false" Imprimible="false" />
</asp:Content>
