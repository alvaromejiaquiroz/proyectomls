<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Obras.aspx.cs" Inherits="Solicitudes_Obras"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx"   TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/Obras.ascx" tagname="Obras" tagprefix="uc1" %>
<asp:Content ID="cObras" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:Panel ID="pnlObras" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitud de obras e instalaciones
            </td>
        </tr>
        <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="tcObras" runat="server" Height="450px">
                    <cc1:TabPanel ID="tpCliente" HeaderText="Cliente" runat="server">
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
                                        <asp:DropDownList ID="cmbCliente" runat="server" CssClass="text_custom">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Nro. de orden de compra o contrato
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOrdenCompra" runat="server" Width="254px" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrdenCompra" runat="server" ErrorMessage="Debe ingresar el número de orden de compra o contrato."
                                            ControlToValidate="txtOrdenCompra" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContacto" runat="server" Width="254px" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContacto" runat="server" ErrorMessage="Debe ingresar el contacto."
                                            ControlToValidate="txtContacto" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        E-Mail de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMail" runat="server" Width="254px" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMail" runat="server" ErrorMessage="Debe ingresar el e-mail de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMail" runat="server" ErrorMessage="El e-mail de contacto no es válido"
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMail"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Teléfono de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="254px" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefonoContacto" runat="server" ErrorMessage="Debe ingresar el teléfono de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtTelefonoContacto"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpTareas" HeaderText="Tareas" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Descripción de tareas solicitadas
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescripcionTareas" runat="server" Width="250px" 
                                            Height="64px" TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescripcionTareas" runat="server" ErrorMessage="Debe ingresar la descripción de tareas solicitadas."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtDescripcionTareas"></asp:RequiredFieldValidator>
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
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Fecha de entrega
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntrega" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                        <asp:ImageButton ID="imgEntrega" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                                        <cc1:CalendarExtender ID="ceEntrega" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgEntrega"
                                            TargetControlID="txtEntrega">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvEntrega" runat="server" ErrorMessage="Debe ingresar la fecha de entrega."
                                            ControlToValidate="txtEntrega" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvEntrega" runat="server" ErrorMessage="La fecha de entrega no es válida."
                                            ControlToValidate="txtEntrega" Display="None" Operator="DataTypeCheck" Type="Date"
                                            ValidationGroup="solicitud"></asp:CompareValidator>
                                        <asp:CompareValidator ID="cvFechas" runat="server" ErrorMessage="La fecha de inicio debe ser menor o igual que la fecha de entrega."
                                            ControlToCompare="txtEntrega" ControlToValidate="txtInicio" Display="None" Operator="LessThanEqual"
                                            Type="Date" ValidationGroup="solicitud"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Requisitos de aprobación
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequisitosAprovacion" runat="server" Width="254px" Height="64px" 
                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRequisitosAprovacion" runat="server" ErrorMessage="Debe ingresar los requisitos de aprobación."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtRequisitosAprovacion"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Requisitos de ingreso
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequisitoIngreso" runat="server" Width="254px" Height="64px" TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRequisitoIngreso" runat="server" ErrorMessage="Debe ingresar los requisitos de ingreso."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtRequisitoIngreso"></asp:RequiredFieldValidator>
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
                                                <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbResponsable_SelectedIndexChanged" CssClass="text_custom">
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
                                                    OnRowDeleting="gvPersonas_RowDeleting"
                                                    Width="100%">
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
                                                <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" Height="150px" CssClass="text_custom">
                                                </asp:ListBox>
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
                                        <asp:TextBox ID="txtPresupuesto" runat="server" ValidationGroup="solicitud" MaxLength="12" CssClass="text_custom"></asp:TextBox>
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
                    <cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
                        <ContentTemplate>
                            <div style="width: 600px">
                                <uc4:Adjuntos ID="ucAdjuntos" runat="server" TipoSol="MantenimientoPreventivo" />
                            </div>
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
    <uc1:Obras ID="ucObras" runat="server" Visible="false" Imprimible="false" />
</asp:Content>

