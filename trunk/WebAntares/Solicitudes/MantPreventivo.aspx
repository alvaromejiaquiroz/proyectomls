<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantPreventivo.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/MantenimientoPreventivo.ascx" tagname="MantenimientoPreventivo" tagprefix="uc1" %>
<%@ Register src="../Controles/Solicitud_Gastos.ascx" tagname="Solicitud_Gastos" tagprefix="uc3" %>

<asp:Content ID="cMantenimientoPreventivo" ContentPlaceHolderID="PageContainer" runat="Server">
    <asp:Panel ID="pnlMantenimientoPreventivo" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                SOLICITUD DE MANTENIMIENTO PREVENTIVO
            </td>
        </tr>
        <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="tcMantenimientoPreventivo" runat="server" Height="450px" 
                    ActiveTabIndex="0" 
                    onactivetabchanged="tcMantenimientoPreventivo_ActiveTabChanged">
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
                                            
                                                <asp:DropDownList ID="cboSitios" runat="server" CssClass="text_custom">
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="cvSitios" runat="server" ErrorMessage="Debe seleccionar un sitio."
                                                    ControlToValidate="cboSitios" Display="None" Operator="NotEqual" ValueToCompare="-1"
                                                    ValidationGroup="tareas"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px">
                                                Intervalo Programado
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDesde" runat="server" MaxLength="10" Width="80px" 
                                                    CssClass="text_custom" ontextchanged="txtDesde_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgDesde" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
                                                <cc1:CalendarExtender ID="ceDesde" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDesde" CssClass="cal_Theme1" 
                                                    TargetControlID="txtDesde" >
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvDesde" runat="server" ErrorMessage="Debe ingresar la fecha desde."
                                                    ControlToValidate="txtDesde" Display="None" ValidationGroup="tareas"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvDesde" runat="server" ErrorMessage="La fecha desde no es válida."
                                                    ControlToValidate="txtDesde" Display="None" Operator="DataTypeCheck" Type="Date"
                                                    ValidationGroup="tareas"></asp:CompareValidator>
                                                <asp:TextBox ID="txtHasta" runat="server" MaxLength="10" Width="80px" CssClass="text_custom" Enabled="false"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px">
                                                Tareas a Realizar
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstTareas" runat="server" SelectionMode="Single" Height="150px" CssClass="text_custom">
                                                </asp:ListBox>
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
                                                <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvTareas_RowDeleting" OnRowDataBound="gvTareas_RowDataBound" OnRowCreated="gvTareas_RowCreated"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="Id_solicitud" Visible="false" />
                                                        <asp:BoundField DataField="Id_tarea" Visible="false"  />
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
                    <cc1:TabPanel ID="tpPersonal" HeaderText="Responsable y Acompañante" runat="server">
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
                                                <asp:GridView ID="gvSolicitudPersonas" runat="server" AutoGenerateColumns="False" AllowPaging="true" onpageindexchanging="gvSolicitudPersonas_PageIndexChanging" PageSize="5"
                                                    OnRowDeleting="gvPersonas_RowDeleting" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:CheckBoxField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
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
                                          
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" Text="Asignar vehículo"
                                                    OnClick="btnAsignaVehiculoSolicitud_Click"  CssClass="button_custom" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="true" onpageindexchanging="gvSolicitudVehiculos_PageIndexChanging" PageSize="5"
                                                    OnRowDeleting="gvSolicitudVehiculos_rowDeleting" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" Visible="False" />
                                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HeaderStyle-HorizontalAlign="Center" />
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
                                        Contacto del Cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactoCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactoCliente" runat="server" ErrorMessage="Debe ingresar el contacto de cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtContactoCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        N° Orden del Cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNroOrdenCliente" runat="server" ErrorMessage="Debe ingresar el nro. de orden del cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtNroOrdenCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Teléfono de Contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefonoContacto" runat="server" ErrorMessage="Debe ingresar el teléfono de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtTelefonoContacto"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        E-mail de Contacto
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                            <div style="width: 600px">
                                    <asp:GridView ID="gvCalidad" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Existen Registros de Calidad Asignados" onrowcommand="gvCalidad_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="False" />
                                            <asp:BoundField DataField="NombreArchivo" HeaderText="Archivo Calidad" HeaderStyle-HorizontalAlign="Left"/> 
                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                
                                                    <asp:ImageButton ID="imgAdjunto" runat="server" CausesValidation="False" 
                                                    CommandName="Descargar" 
                                                    CommandArgument='<%# Bind("Id") %>'
                                                    ImageUrl="~/Images/guardar.gif" Text="Descargar" ToolTip="Descargar el Archivo"  />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="gvCalidad" />
                        </Triggers>
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
                                     <uc3:Solicitud_Gastos ID="ucSolicitudGastos" runat="server" />
                                    </td>
                                </tr>
                            </table>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                        <ContentTemplate>
                            <div style="width:100%;text-align:center;margin-top:30px">
                                <asp:Button ID="btnAceptarSolicitud" runat="server" OnClick="btnAceptarSolicitud_Click"
                                    Text="Confirmar Solicitud" ValidationGroup="solicitud" CssClass="button_custom" />
                            <asp:CustomValidator ID="cvGastosEnSolicitud" runat="server" ErrorMessage="" Display="None" ValidationGroup="solicitud" OnServerValidate="cvGastosEnSolicitud_ServerValidate"></asp:CustomValidator>
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
    <uc1:MantenimientoPreventivo ID="ucMantenimientoPreventivo" runat="server" Visible="false" Imprimible="false" />
   

</asp:Content>
