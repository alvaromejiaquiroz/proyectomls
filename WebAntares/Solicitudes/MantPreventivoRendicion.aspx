<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="MantPreventivoRendicion.aspx.cs"
    Inherits="Solicitudes_MantPreventivoRendicion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/MantenimientoPreventivoRendicion.ascx" tagname="MantenimientoPreventivoRendicion" tagprefix="uc1" %>
<asp:Content ID="cMantenimientoPreventivoRendicion" ContentPlaceHolderID="PageContainer"
    runat="Server">
    <asp:Panel ID="pnlMantenimientoPreventivoRendicion" runat="server">
        <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
            <tr class="header_custom">
                <td align="center">
                    REPORTE DE MANTENIMIENTO PREVENTIVO
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
                        <cc1:TabPanel ID="tpTareas" HeaderText="Tareas" runat="server">
                            <HeaderTemplate>
                                Tareas
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="upTareas" runat="server">
                                    <ContentTemplate>
                                        <table id="tblTareas" class="text_custom" width="600px">
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 20px">
                                                    Sitio
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litSitio" runat="server"></asp:Literal>
                                                    <asp:HiddenField ID="hdnSitio" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 20px">
                                                    Fecha de realización de propuesta
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDesde" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                                    <asp:ImageButton ID="imgDesde" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
                                                    <cc1:CalendarExtender ID="ceDesde" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDesde"
                                                        TargetControlID="txtDesde">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvDesde" runat="server" ErrorMessage="Debe ingresar la fecha desde."
                                                        ControlToValidate="txtDesde" Display="None" ValidationGroup="tareas"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cvDesde" runat="server" ErrorMessage="La fecha desde no es válida."
                                                        ControlToValidate="txtDesde" Display="None" Operator="DataTypeCheck" Type="Date"
                                                        ValidationGroup="tareas"></asp:CompareValidator>
                                                    <asp:TextBox ID="txtHasta" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                                    <asp:ImageButton ID="imgHasta" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
                                                    <cc1:CalendarExtender ID="ceHasta" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgHasta"
                                                        TargetControlID="txtHasta">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvHasta" runat="server" ErrorMessage="Debe ingresar la fecha hasta."
                                                        ControlToValidate="txtHasta" Display="None" ValidationGroup="tareas"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cvHasta" runat="server" ErrorMessage="La fecha hasta no es válida."
                                                        ControlToValidate="txtHasta" Display="None" Operator="DataTypeCheck" Type="Date"
                                                        ValidationGroup="tareas"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="cvFecha" runat="server" ErrorMessage="La fecha de desde debe ser menor o igual que la fecha hasta."
                                                        ControlToCompare="txtHasta" ControlToValidate="txtDesde" Display="None" Operator="LessThanEqual"
                                                        Type="Date" ValidationGroup="tareas"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 20px">
                                                    Tareas a realizar
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lstTareas" runat="server" SelectionMode="Multiple" Height="150px"
                                                        CssClass="text_custom"></asp:ListBox>
                                                    <asp:CustomValidator ID="cvTareas" runat="server" ErrorMessage="Debe seleccionar al menos una tarea."
                                                        Display="None" ValidationGroup="tareas" OnServerValidate="cvTareas_ServerValidate"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btnAgregarTarea" runat="server" OnClick="btnAgregarTarea_Click" Text="Agregar tarea"
                                                        ValidationGroup="tareas" CssClass="button_custom" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvTareas_RowDeleting"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                            <asp:BoundField DataField="Id_solicitud" Visible="false" />
                                                            <asp:BoundField DataField="Id_tarea" Visible="false" />
                                                            <asp:BoundField DataField="tarea" HeaderText="Tarea" HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Inicio" HeaderText="Inicio" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Fin" HeaderText="Fin" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center" />
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
                                        <div class="text_custom">
                                            <asp:ValidationSummary ID="vsTareas" runat="server" ValidationGroup="tareas" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                                        OnRowDeleting="gvPersonas_RowDeleting" OnRowEditing="gvPersonas_RowEditing" Width="100%">
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
                                                                        ImageUrl="~/Images/basuravacio.gif" Text="Delete" />
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
                                                        OnRowDeleting="gvSolicitudVehiculos_rowDeleting" OnRowEditing="gvSolicitudVehiculos_RowEditing" Width="100%">
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
                                                                        ImageUrl="~/Images/basuravacio.gif" Text="Delete" />
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
                        <cc1:TabPanel ID="tpComplemento" HeaderText="Datos Cliente" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Contrato
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
                                        <asp:TextBox ID="txtContactoCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactoCliente" runat="server" ErrorMessage="Debe ingresar el contacto de cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtContactoCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        N° orden del Cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNroOrdenCliente" runat="server" ErrorMessage="Debe ingresar el nro. de orden del cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtNroOrdenCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Teléfono de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefonoContacto" runat="server" ErrorMessage="Debe ingresar el teléfono de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtTelefonoContacto"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        E-mail de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMailContacto" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
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
                        <cc1:TabPanel ID="tpRegistroCalidad" HeaderText="Registro de Calidad" runat="server">
                        <ContentTemplate>
                            <div style="width: 600px">
                                Adjuntos Calidad....
                            </div>
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
                                    <td style="padding-left: 10px">
                                        Gastos Reales Incurridos
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
                        <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%; text-align: center; margin-top: 30px">
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
    <uc1:MantenimientoPreventivoRendicion ID="ucMantenimientoPreventivoRendicion" runat="server" Imprimible="false" Visible="false" />
</asp:Content>
