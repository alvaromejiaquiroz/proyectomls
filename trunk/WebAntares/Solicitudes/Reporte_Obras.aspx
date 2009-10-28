<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Reporte_Obras.aspx.cs" Inherits="Solicitudes_Reporte_Obras" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx"   TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/ObrasRendicion.ascx" tagname="ObrasRendicion" tagprefix="uc1" %>
<asp:Content ID="cReporteObras" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:Panel ID="pnlReporteObras" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Reporte de obras e instalaciones
            </td>
        </tr>
        <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="tcReporteObras" runat="server" Height="450px">
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
                    <cc1:TabPanel ID="tpDescripcion" HeaderText="Detalles" runat="server">
                        <HeaderTemplate>
                            Detalles
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <table class="text_custom" width="600px">
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Descripción del Trabajo Realizado</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDescripcionTrabajo" runat="server" TextMode="MultiLine" Height="160px" Width="100%" CausesValidation="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="La descripción del trabajo realizado es obligatoria"
                                            ControlToValidate="txtDescripcionTrabajo" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>

                                    </td>
                                    <td></td>
                              
                                </tr>
                                </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                        <asp:TextBox ID="txtDescripcionTareas" runat="server" Width="250px" ReadOnly="true" 
                                            Height="64px" TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Fecha de inicio
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="80px" ReadOnly="true" CssClass="text_custom"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Fecha de entrega
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntrega" runat="server" MaxLength="10" Width="80px" ReadOnly="true" CssClass="text_custom"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Requisitos de aprobación
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequisitosAprovacion" runat="server" ReadOnly="true" Width="254px" Height="64px" 
                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Requisitos de ingreso
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequisitoIngreso" runat="server" ReadOnly="true" Width="254px" Height="64px" TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
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
                                                    OnRowDeleting="gvPersonas_RowDeleting" OnRowEditing="gvPersonas_RowEditing"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:CheckBoxField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                                                ShowEditButton="True" />
                                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Images/basuravacio.gif" Text="Delete" ToolTip="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlHorasPersonal" runat="server" Style="display: none" CssClass="modalPopup">
                                            <table width="700px" class="text_custom" style="border-style: solid; border-color: #000000; border-width: 1px">
                                                <tr class="header_custom">
                                                    <td align="center" colspan="2">
                                                        Reporte de horas de personal
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Persona
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litHorasPersonalPersona" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hfHorasPersonalPersona" runat="server" />
                                                        <asp:HiddenField ID="hfHorasPersonalSolicitud" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Día
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasPersonalDia" runat="server" MaxLength="10" Width="80px"
                                                            CssClass="text_custom"></asp:TextBox>
                                                        <asp:ImageButton ID="imgHorasPersonalDia" runat="server" CausesValidation="false"
                                                            ImageUrl="~/Images/calendario.gif" />
                                                        <cc1:CalendarExtender ID="ceHorasPersonalDia" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgHorasPersonalDia" TargetControlID="txtHorasPersonalDia">
                                                        </cc1:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvHorasPersonalDia" runat="server" ErrorMessage="Debe ingresar el día."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" ValidationGroup="horasPersonal"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasPersonalDia" runat="server" ErrorMessage="El día no es válido."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" Operator="DataTypeCheck"
                                                            Type="Date" ValidationGroup="horasPersonal"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Horas
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHorasPersonalHoras" runat="server" CssClass="text_custom">
                                                            <asp:ListItem Selected="True" Text="0,5" Value="0,5"></asp:ListItem>
                                                            <asp:ListItem Text="1,0" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="1,5" Value="1,5"></asp:ListItem>
                                                            <asp:ListItem Text="2,0" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="2,5" Value="2,5"></asp:ListItem>
                                                            <asp:ListItem Text="3,0" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="3,5" Value="3,5"></asp:ListItem>
                                                            <asp:ListItem Text="4,0" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="4,5" Value="4,5"></asp:ListItem>
                                                            <asp:ListItem Text="5,0" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="5,5" Value="5,5"></asp:ListItem>
                                                            <asp:ListItem Text="6,0" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="6,5" Value="6,5"></asp:ListItem>
                                                            <asp:ListItem Text="7,0" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="7,5" Value="7,5"></asp:ListItem>
                                                            <asp:ListItem Text="8,0" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="8,5" Value="8,5"></asp:ListItem>
                                                            <asp:ListItem Text="9,0" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="9,5" Value="9,5"></asp:ListItem>
                                                            <asp:ListItem Text="10,0" Value="10"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Descripción
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasPersonalDescripcion" runat="server" Width="250px" Height="64px"
                                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasPersonalDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                                                            Display="None" ValidationGroup="horasPersonal" ControlToValidate="txtHorasPersonalDescripcion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding:10px">
                                                        <asp:Button ID="btnHorasPersonalGuardar" runat="server" OnClick="btnHorasPersonalGuardar_Click"
                                                            Text="Guardar" ValidationGroup="horasPersonal" CssClass="button_custom" />
                                                        <asp:Button ID="btnHorasPersonalCerrar" runat="server" Text="Cerrar" CausesValidation="false"
                                                            CssClass="button_custom" />
                                                        <asp:CompareValidator ID="cvHorasPersonal" runat="server" ErrorMessage="La fecha debe ser mayor o igual a la fecha de inicio de la solicitud."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" Operator="GreaterThanEqual"
                                                            Type="Date" ValidationGroup="horasPersonal" ValueToCompare="01/01/2000"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvHorasPersonal" runat="server" AutoGenerateColumns="False" 
                                                            onrowdeleting="gvHorasPersonal_RowDeleting" Width="640px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                             <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" 
                                                                        CommandName="Delete" ImageUrl="~/Images/basuravacio.gif" Text="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:ValidationSummary ID="vsHorasPersonal" runat="server" ValidationGroup="horasPersonal" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnHorasPersonalPopUp" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="mpeHorasPersonal" runat="server" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnHorasPersonalCerrar" PopupControlID="pnlHorasPersonal"
                                            TargetControlID="btnHorasPersonalPopUp">
                                        </cc1:ModalPopupExtender>
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
                                                    OnRowDeleting="gvSolicitudVehiculos_RowDeleting" OnRowEditing="gvSolicitudVehiculos_RowEditing" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" Visible="False" />
                                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                                                ShowEditButton="True" />
                                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Images/basuravacio.gif" Text="Delete" ToolTip="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlHorasVehiculos" runat="server" Style="display: none" CssClass="modalPopup">
                                            <table width="700px" class="text_custom" style="border-style: solid; border-color: #000000; border-width: 1px">
                                                <tr class="header_custom">
                                                    <td align="center" colspan="2">
                                                        Reporte de horas de vehículos
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Vehículo
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litHorasVehiculosVehiculo" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hfHorasVehiculosVehiculo" runat="server" />
                                                        <asp:HiddenField ID="hfHorasVehiculosSolicitud" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Día
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosDia" runat="server" MaxLength="10" Width="80px"
                                                            CssClass="text_custom"></asp:TextBox>
                                                        <asp:ImageButton ID="imgHorasVehiculosDia" runat="server" CausesValidation="false"
                                                            ImageUrl="~/Images/calendario.gif" />
                                                        <cc1:CalendarExtender ID="ceHorasVehiculosDia" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgHorasVehiculosDia" TargetControlID="txtHorasVehiculosDia">
                                                        </cc1:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosDia" runat="server" ErrorMessage="Debe ingresar el día."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" ValidationGroup="horasVehiculos"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasVehiculosDia" runat="server" ErrorMessage="El día no es válido."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" Operator="DataTypeCheck"
                                                            Type="Date" ValidationGroup="horasVehiculos"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Horas
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHorasVehiculosHoras" runat="server" CssClass="text_custom">
                                                            <asp:ListItem Selected="True" Text="0,5" Value="0,5"></asp:ListItem>
                                                            <asp:ListItem Text="1,0" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="1,5" Value="1,5"></asp:ListItem>
                                                            <asp:ListItem Text="2,0" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="2,5" Value="2,5"></asp:ListItem>
                                                            <asp:ListItem Text="3,0" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="3,5" Value="3,5"></asp:ListItem>
                                                            <asp:ListItem Text="4,0" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="4,5" Value="4,5"></asp:ListItem>
                                                            <asp:ListItem Text="5,0" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="5,5" Value="5,5"></asp:ListItem>
                                                            <asp:ListItem Text="6,0" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="6,5" Value="6,5"></asp:ListItem>
                                                            <asp:ListItem Text="7,0" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="7,5" Value="7,5"></asp:ListItem>
                                                            <asp:ListItem Text="8,0" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="8,5" Value="8,5"></asp:ListItem>
                                                            <asp:ListItem Text="9,0" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="9,5" Value="9,5"></asp:ListItem>
                                                            <asp:ListItem Text="10,0" Value="10"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Kilómetros
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosKilometros" runat="server" MaxLength="10" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosKilometros" runat="server" ErrorMessage="Debe ingresar los kilómetros."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosKilometros"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasVehiculosKilometros" runat="server" ErrorMessage="Los kilómetros no son válidos."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosKilometros"
                                                            Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Descripción
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosDescripcion" runat="server" Width="250px" Height="64px"
                                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosDescripcion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding:10px">
                                                        <asp:Button ID="btnHorasVehiculosGuardar" runat="server" OnClick="btnHorasVehiculosGuardar_Click"
                                                            Text="Guardar" ValidationGroup="horasVehiculos" CssClass="button_custom" />
                                                        <asp:Button ID="btnHorasVehiculosCerrar" runat="server" Text="Cerrar" CausesValidation="false"
                                                            CssClass="button_custom" />
                                                        <asp:CompareValidator ID="cvHorasVehiculos" runat="server" ErrorMessage="La fecha debe ser mayor o igual a la fecha de inicio de la solicitud."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" Operator="GreaterThanEqual"
                                                            Type="Date" ValidationGroup="horasVehiculos" ValueToCompare="01/01/2000"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvHorasVehiculos" runat="server" AutoGenerateColumns="False" 
                                                            onrowdeleting="gvHorasVehiculos_RowDeleting" Width="640px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="Kilometros" HeaderText="Kilómetros" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Horas" HeaderText="Duración" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                             <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" 
                                                                        CommandName="Delete" ImageUrl="~/Images/basuravacio.gif" Text="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:ValidationSummary ID="vsHorasVehiculos" runat="server" ValidationGroup="horasVehiculos" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnHorasVehiculosPopUp" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="mpeHorasVehiculos" runat="server" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnHorasVehiculosCerrar" PopupControlID="pnlHorasVehiculos"
                                            TargetControlID="btnHorasVehiculosPopUp">
                                        </cc1:ModalPopupExtender>
                                    <div class="text_custom">
                                        <asp:ValidationSummary ID="vsVehiculos" runat="server" ValidationGroup="vehiculos" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="400px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                       Ingresar Gastos Estimados
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                        <asp:TextBox ID="txtPresupuesto" runat="server" ValidationGroup="solicitud" MaxLength="12" CssClass="text_custom"></asp:TextBox>
                                        <asp:CompareValidator ID="cvPresupuesto" runat="server" ErrorMessage="El monto del presupuesto no es válido."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtPresupuesto"
                                            Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                    <asp:ImageButton runat="server" ID="btnAceptarGastos" CommandArgument="Aceptar" CommandName="Aceptar"   ImageUrl="~/images/aprobar.gif"  OnClick="btnAceptarGastos_Click"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Gastos Estimados:
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                       <asp:Label runat="server" ID="lblGastos" Visible="true" ForeColor="Red" Font-Bold="true" ></asp:Label>
                                    </td>
                                    <td colspan="2" style="height: 20px">
                                     
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                            </asp:UpdatePanel>
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
    <uc1:ObrasRendicion ID="ucObrasRendicion" runat="server" Visible="false" Imprimible="false" />
</asp:Content>

