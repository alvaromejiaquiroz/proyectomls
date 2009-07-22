<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantPreventivo.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Src="../Controles/jDatePick.ascx" TagName="jDatePick" TagPrefix="uc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/cboSitios.ascx" TagName="cboSitios" TagPrefix="uc3" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/CboGeneric.ascx" tagname="CboGeneric" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">

    <script type="text/javascript">
        $(function() {
            $('#Solapas').tabs();
            

        });

        //extender
        jQuery.fn.gboton = function() {
            this.each(function() {
                var $$ = $("<input name='buton' value='test'>");
                $(this).html($$);
                $(this).click(function() {
                alert($$.val());
                })
            });
        }
                       
        </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <uc2:SolDetalle ID="SolDetalle1" runat="server" />
    <div id="Solapas">
        <ul>
            <li><a href="#divtblTareas"><span>Tareas</span></a></li>
            <li><a href="#divtblPersonas"><span>Personal</span></a></li>
            <li><a href="#divvehiculos"><span>Vehiculos</span></a></li>
            <li><a href="#divtblComplemento"><span>Datos Complementarios</span></a></li>
            <li><a href="#divadjuntos"><span>Adjuntos </span></a></li>
            <li><a href="#divPresupuesto"><span>Presupuesto</span></a></li>
            <li><a href="#divFin"><span>Confirmación</span></a></li>    
        </ul>
        <div id="divtblTareas">
            
            <table id="tblTareas">
                <tr>
                    <td valign="top" >
                        <asp:Label ID="Label2" runat="server" Text="Sitio"></asp:Label>
                    </td>
                    <td valign="top" >
                          <asp:DropDownList ID="cboSitios" runat="server" AutoPostBack="false" Width="200px">
                                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label3" runat="server" Text="Fecha de Realizacion Propuesta"></asp:Label>
                    </td>
                    <td>
                        <uc1:jDatePick ID="jDatePick1" runat="server" Name="desde" CustomScript="customRange" />
                        <uc1:jDatePick ID="jDatePick2" runat="server" Name="hasta" CustomScript="customRange" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        <asp:Label ID="Label1" runat="server" Text="Tareas a Realizar"></asp:Label>
                    </td>
                    <td valign="top" >
                        <asp:ListBox ID="lstTareasaRealizar" runat="server" Width="324px" 
                            SelectionMode="Multiple" Height="174px">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="text-align: center;" colspan="2">
                        <asp:Button ID="btnAgregarTarea" runat="server" OnClick="btnAgregarTarea_Click" Text="Agregar Tarea" />
                        <asp:Button ID="Button1" runat="server" Text="Cancelar" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" style="text-align: center;">
                     <asp:UpdatePanel ID="UpdateTareas" runat="server">
                         <ContentTemplate>
                        <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting"
                            OnSelectedIndexChanged="gvTareas_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" visible="false"/>
                                <asp:BoundField DataField="Id_solicitud" HeaderText="Id_solicitud" ReadOnly="True" visible="false"/>
                                <asp:BoundField DataField="Id_tarea" HeaderText="Id_tarea" ReadOnly="True" visible="false"/>    
                                <asp:BoundField DataField="Inicio" HeaderText="Inicio" />
                                <asp:BoundField DataField="Inicio" HeaderText="Fin" />
                                <asp:BoundField DataField="tarea" HeaderText="Tarea" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div id="noprint">
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/Images/delete.gif" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>

        </div>
        <div id="divtblPersonas">
            <asp:UpdatePanel ID="UpdatePersonas" runat="server">
                <ContentTemplate>
                    <table>
                         <tr>
                            <td valign="top">
                                <asp:Label ID="Label4" runat="server" Text="Responsable"></asp:Label>
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="cmbResponsable_SelectedIndexChanged" Width="247px">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label5" runat="server" Text="Personal"></asp:Label>
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:ListBox ID="lstEmpleadosSolicitud" runat="server" SelectionMode="Multiple" 
                                    Height="136px" Width="321px" AutoPostBack="True">
                                </asp:ListBox>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" Text="Asignar Empleados"
                                    OnClick="btnAsignaEmpleadoSolicitud_Click" />
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td style="width: 509px" valign="top">
                                                        <asp:GridView ID="gvSolicitudPersonas" runat="server" AutoGenerateColumns="False"
                                    OnRowDeleting="gvPersonas_RowDeleting" 
                                    OnRowDataBound="gvSolicitudPersonas_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible ="false" />
                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" /> 
                                        <asp:TemplateField   Visible="false"  HeaderText="Responsable">
                                        <ItemTemplate>
                                            <asp:Image  ID="responsable" ImageUrl="~/Images/engranaje.gif" runat="server" CausesValidation="False"  />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div ID="noprint0">
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/delete.gif" Text="Delete" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divvehiculos">
            <asp:UpdatePanel ID="UpdateVehiculos" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label6" runat="server" Text="Vehiculos"></asp:Label>
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" 
                                    Height="147px" Width="349px"></asp:ListBox>
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td style="width: 509px" valign="top">
                                <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" Text="Asigna Vehiculos"
                                    OnClick="btnAsignaVehiculoSolicitud_Click" />
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False" 
                                    OnRowDeleting="gvSolicitudVehiculos_rowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" Visible="False" />
                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div id="noprint1">
                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Delete"
                                                        ImageUrl="~/Images/delete.gif" Text="Delete" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divtblComplemento">
            <asp:UpdatePanel ID="UpdateComplemento" runat="server">
                <ContentTemplate>
                      <table>
                <tr>
                    <td valign="top" >
                        <asp:Label ID="Label7" runat="server" Text="Datos Complementarios"></asp:Label>
                    </td>
                    <td valign="top" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" >
                        Cliente</td>
                    <td valign="top" >
                        <asp:DropDownList ID="cmbClientes" runat="server" Height="34px" Width="210px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        <asp:Label ID="Label9" runat="server" Text="Contacto Cliente"></asp:Label>
                    </td>
                    <td valign="top" >
                        <asp:TextBox ID="txtContactoCliente" runat="server" style="margin-left: 0px" 
                            Width="210px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        <asp:Label ID="Label10" runat="server" Text="Nro.Orden"></asp:Label>
                        
                        
                      
                    </td>
                    <td valign="top" >
                        <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        Telefono Contacto</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        E-Mail Contacto</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtMailContacto" runat="server" Width="210px"></asp:TextBox>
                    </td>
                </tr>
            </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divadjuntos">
            
                    <table>
                        <tr>
                            <td valign="top" >
                                Datos Ajuntos
                            </td>
                            <td valign="top" >
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" >
                                <uc4:Adjuntos ID="Adjuntos1" runat="server" />
                            </td>
                            <td valign="top" >
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                    </table>
             
        </div>
        <div id="divPresupuesto">
            <asp:UpdatePanel ID="UpdatePanelPresupuesto" runat="server">
                <ContentTemplate>
                         <table>
                             <tr>
                                 <td >
                                     Presupuesto</td>
                                 <td >
                                     <asp:TextBox ID="txtPresupuesto" CssClass="numeric" runat="server"></asp:TextBox>
                                 </td>
                                 <td >
                                     &nbsp;
                                 </td>
                                 <td >
                                     &nbsp;
                                 </td>
                             </tr>
                         </table>
                         </ContentTemplate>
            </asp:UpdatePanel>
       
        </div>
        <div id="divFin">
         <table>
        <tr>
            <td valign="top">
                &nbsp;
            </td>
            <td style="width: 509px" valign="top">
                <asp:Button ID="btnAceptarSolicitud" runat="server" OnClick="btnAceptarSolicitud_Click"
                    Text="Confirmar Solicitud" />
            </td>
            <td valign="top">
                &nbsp;
            </td>
            <td valign="top">
                &nbsp;
            </td>
        </tr>
    </table>

        </div>
    </div>
    

    <script type="text/javascript">
        //funcion adicional para controlar el rango de fechas

        function customRange(input) {
            return { minDate: (input.id == hasta ? $("#" + desde).datepicker("getDate") : null),
                maxDate: (input.id == desde ? $("#" + hasta).datepicker("getDate") : null)
            };
        } 


    </script>
</asp:Content>
