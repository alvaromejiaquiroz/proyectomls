<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantPreventivo.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
    <cc1:TabContainer ID="tcMantenimientoPreventivo" runat="server" Height="450px">
        <cc1:TabPanel ID="tpTareas" HeaderText="Tareas" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="upTareas" runat="server">
                    <ContentTemplate>
                        <table id="tblTareas" width="600px">
                            <tr>
                                <td>
                                    Sitio
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboSitios" runat="server">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cvSitios" runat="server" ErrorMessage="Debe seleccionar un sitio."
                                        ControlToValidate="cboSitios" Display="None" Operator="NotEqual" ValueToCompare="-1"
                                        ValidationGroup="tareas"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fecha de Realización Propuesta
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesde" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                    <asp:ImageButton ID="imgDesde" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.gif" />
                                    <cc1:CalendarExtender ID="ceDesde" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDesde"
                                        TargetControlID="txtDesde">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvDesde" runat="server" ErrorMessage="Debe ingresar la fecha desde."
                                        ControlToValidate="txtDesde" Display="None" ValidationGroup="tareas"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvDesde" runat="server" ErrorMessage="La fecha desde no es válida."
                                        ControlToValidate="txtDesde" Display="None" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="tareas"></asp:CompareValidator>
                                    <asp:TextBox ID="txtHasta" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                    <asp:ImageButton ID="imgHasta" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.gif" />
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
                                <td>
                                    Tareas a Realizar
                                </td>
                                <td>
                                    <asp:ListBox ID="lstTareas" runat="server" SelectionMode="Multiple" Height="174px">
                                    </asp:ListBox>
                                    <asp:CustomValidator ID="cvTareas" runat="server" ErrorMessage="Debe seleccionar al menos una tarea."
                                        Display="None" ValidationGroup="tareas" OnServerValidate="cvTareas_ServerValidate"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAgregarTarea" runat="server" OnClick="btnAgregarTarea_Click" Text="Agregar Tarea"
                                        ValidationGroup="tareas" />
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
                                                        ImageUrl="~/Images/delete.gif" Text="Delete" ToolTip="Eliminar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <div>
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
                        <table width="600px">
                            <tr>
                                <td>
                                    Responsable
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbResponsable_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="litPersonal" runat="server" Text="Personal" Visible="false"></asp:Literal>
                                </td>
                                <td>
                                    <asp:ListBox ID="lstEmpleadosSolicitud" runat="server" SelectionMode="Multiple" Height="136px"
                                        Visible="false"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" Text="Asignar Empleados"
                                        OnClick="btnAsignaEmpleadoSolicitud_Click" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSolicitudPersonas" runat="server" AutoGenerateColumns="False"
                                        OnRowDeleting="gvPersonas_RowDeleting" OnRowDataBound="gvSolicitudPersonas_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                            <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField Visible="false" HeaderText="Responsable">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgResponsable" ImageUrl="~/Images/engranaje.gif" runat="server" CausesValidation="False" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                        <table width="600px">
                            <tr>
                                <td>
                                    Vehículos
                                </td>
                                <td>
                                    <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" Height="147px">
                                    </asp:ListBox>
                                    <asp:CustomValidator ID="cvVehiculos" runat="server" ErrorMessage="Debe seleccionar al menos un vehículo."
                                        Display="None" ValidationGroup="vehiculos" OnServerValidate="cvVehiculos_ServerValidate"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" Text="Asignar Vehiculo"
                                        OnClick="btnAsignaVehiculoSolicitud_Click" ValidationGroup="vehiculos" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False"
                                        OnRowDeleting="gvSolicitudVehiculos_rowDeleting" Width="100%">
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
                        <div>
                            <asp:ValidationSummary ID="vsVehiculos" runat="server" ValidationGroup="vehiculos" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tpComplemento" HeaderText="Complemento" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            Cliente
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbClientes" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contacto Cliente
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactoCliente" runat="server" Width="210px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nro.Orden
                        </td>
                        <td>
                            <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Teléfono Contacto
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            E-Mail Contacto
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailContacto" runat="server" Width="210px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
            <ContentTemplate>
                <div style="width:600px">
                    <uc4:Adjuntos ID="ucAdjuntos" runat="server" TipoSol="MantenimientoPreventivo" />
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            Monto
                        </td>
                        <td>
                            <asp:TextBox ID="txtPresupuesto" CssClass="numeric" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
            <ContentTemplate>
               <asp:Button ID="btnAceptarSolicitud" runat="server" OnClick="btnAceptarSolicitud_Click"
                     Text="Confirmar Solicitud" />
               <div>
                    <asp:BulletedList ID="blErrores" runat="server" ForeColor="Red" BulletStyle="NotSet">
                    </asp:BulletedList>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
