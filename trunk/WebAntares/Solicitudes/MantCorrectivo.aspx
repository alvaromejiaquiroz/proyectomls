<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantCorrectivo.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Src="../Controles/jDatePick.ascx" TagName="jDatePick" TagPrefix="uc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/cboSitios.ascx" TagName="cboSitios" TagPrefix="uc3" %>
<%@ Register src="../Controles/CboGeneric.ascx" tagname="CboGeneric" tagprefix="uc5" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/FechayHora.ascx" tagname="FechayHora" tagprefix="uc4" %>



<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">

    <script type="text/javascript">
        $(function() {
            $('#Solapas').tabs();
        });
        </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <uc2:SolDetalle ID="SolDetalle2" runat="server" />
    <div id="Solapas">
        <ul>
            <li><a href="#divtblFalla"><span>Falla Reportada</span></a></li>
            <li><a href="#divtblServicios"><span>Servicios Afectados</span></a></li>
            <li><a href="#divtblPlazo"><span>Plazo</span></a></li>
            <li><a href="#divtblPersonal"><span>Personal Asignado</span></a></li>
            <li><a href="#divtblVehiculos"><span>Vehiculos</span></a></li>
            <li><a href="#divtblComplemento"><span>Datos Complementarios</span></a></li>
            <li><a href="#divadjuntos"><span>Adjuntos </span></a></li>
            <li><a href="#divPresupuesto"><span>Presupuesto</span></a></li>
            <li><a href="#divFin"><span>Confirmación</span></a></li>
        </ul>
        <div id="divtblFalla">
                <table  id="tResponsable" style="text-align: left; vertical-align: middle">
                <tr>
                <td valign="top" >
                    <asp:Label ID="Label15" runat="server" Text="Reportó Falla"  
                        ></asp:Label>

                </td>
                    <td valign="top">
                 <asp:TextBox ID="txtReportoFalla" runat="server" Width="281px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td valign="top" >
                    <asp:Label ID="Label17" runat="server" Text="Causa Probable"  
                        ></asp:Label>

                </td>
                    <td valign="top">
                 <asp:TextBox ID="txtCausa" runat="server" Height="64px" Width="284px" 
                            TextMode="MultiLine" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td valign="top" style="width: 81px">
                    <asp:Label ID="Label16" runat="server" Text="Fecha Reporte"></asp:Label>

                </td>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
                </table>
                
                <div> <uc4:FechayHora ID="FechayHora1" runat="server" /> </div>
                
                <br />
                
                <table>
                <tr> <td valign="top">
                        <asp:Label ID="Label1" runat="server" Text="Falla"></asp:Label>
                    </td></tr>
                <tr>
                    <td valign="top">
                        <asp:TextBox ID="txtFalla" runat="server" Height="144px" Width="470px" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divtblServicios">
            <table>
                <tr>
                    <td valign="top" style="width: 107px">
                        <asp:Label ID="Label12" runat="server" Text="Servicios Afectados"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td valign="top" style="width: 509px">
                         <asp:ListBox ID="lstServiciosAfectados" runat="server" Height="141px" SelectionMode="Multiple"
                            Width="471px"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                <td style="width: 107px">
            
                    
                    <asp:GridView ID="gvServicios"  runat="server" AutoGenerateColumns="false"
                            OnRowDeleting="gvServicios_RowDeleting" Width="166px">
                            <Columns>
                                 <asp:BoundField DataField="Id" HeaderText="Id" Visible ="false" />
                                 <asp:BoundField DataField="IdServicioAfectado" HeaderText="IdServicioAfectado" Visible ="false" />
                                 <asp:BoundField DataField="Descripcion" HeaderText="Servicios Afectados" Visible ="True" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div id="noprint0">
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/Images/delete.gif" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                </td>
                </tr>
            </table>
        </div>
        <div id="divtblPlazo">
            <table>
              <tr>
                        <td valign="top">
                            <asp:Label ID="Label13" runat="server" Text="Plazo Atencion"></asp:Label>
                        </td>
                        <td style="width: 509px" valign="top">
                            <asp:DropDownList ID="cmbPlazoAtencion" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
        </div>
        <div id="divtblPersonal">
            <asp:UpdatePanel ID="UpdatePersonas" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label14" runat="server" Text="Responsable"></asp:Label>
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="cmbResponsable_SelectedIndexChanged" Width="247px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label5" runat="server" Text="Personal"></asp:Label>
                            </td>
                            <td style="width: 509px" valign="top">
                                <asp:ListBox ID="lstEmpleadosSolicitud" runat="server" Height="139px" 
                                    SelectionMode="Multiple" Width="378px" AutoPostBack="True"></asp:ListBox>
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
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divtblVehiculos">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label6" runat="server" Text="Vehiculos"></asp:Label>
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" 
                                    Width="379px" Height="120px">
                                </asp:ListBox>
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
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top" style="width: 509px">
                                <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False"
                                    OnRowDeleting="gvSolicitudVehiculos_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"  Visible="false"/>
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
      
            <table>
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label7" runat="server" Text="Datos Complementarios"></asp:Label>
                    </td>
                    <td valign="top" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td valign="top">
                        Cliente</td>
                    <td valign="top" >
                        <asp:DropDownList ID="cmbClientes" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label9" runat="server" Text="Contacto Cliente"></asp:Label>
                    </td>
                    <td valign="top" >
                        <asp:TextBox ID="txtContactoCliente" runat="server" style="margin-left: 0px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="Label10" runat="server" Text="Nro.Orden"></asp:Label>
                        
                        
                      
                    </td>
                    <td valign="top" >
                        <asp:TextBox ID="txtNroOrdenCliente" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Telefono Contacto</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtTelefonoContacto" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        E-Mail Contacto</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtMailContacto" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPresupuesto">
            <asp:UpdatePanel ID="UpdatePanelPresupuesto" runat="server">
                <ContentTemplate>
                         <table>
                             <tr>
                                 <td >
                                     Presupuesto : $</td>
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
        <div id="divadjuntos">
            <uc4:Adjuntos ID="Adjuntos1" runat="server" TipoSol="MantenimientoCorrectivo" />
        </div>
        <div id="divFin">
        <asp:Button ID="btnAceptarSolicitud" runat="server" Text="Confirmar Solicitud" OnClick="btnAceptarSolicitud_Click" />
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
