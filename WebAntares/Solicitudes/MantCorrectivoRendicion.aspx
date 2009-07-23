<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantCorrectivoRendicion.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

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

        function RendirPersona(Id) {
            var oWin = window.open("personaHoras.aspx?id=" + Id ,"Horapersona", "width=600,height=500");
        }   
        function RendirVehiculo(idSolicitud,idVehiculo) {
            var oWin = window.open("VehiculosHoras.aspx?id=" + idSolicitud + "&idV=" + idVehiculo,"Kilometros", "width=600,height=500");
        } 
        </script>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <uc2:SolDetalle ID="SolDetalle2" runat="server" />
    <div id="Solapas">
        <ul>
            <li><a href="#divtblFalla"><span>Falla Reportada</span></a></li>
            <li><a href="#divtblServicios"><span>Servicios Afectados</span></a></li>
            <li><a href="#divtblPlazo"><span>Plazo</span></a></li>
            <li><a href="#divtblPersonas"><span>Personal</span></a></li>
            <li><a href="#divTraslados"><span>Traslados</span></a></li>
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
                 <asp:TextBox ID="txtCausa" runat="server" Height="64px" Width="284px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td valign="top" style="width: 81px">
                    <asp:Label ID="Label16" runat="server" Text="Fecha Reporte"></asp:Label>

                </td>
                    <td valign="top">
                        <table style="width:100%;">
                            <tr>
                                <td>
                        <uc4:FechayHora ID="FechayHora1" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
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
          <asp:UpdatePanel ID="UpdateServicios" runat="server">
          <ContentTemplate>
            <table>
                <tr>
                    <td valign="top" >
                      
                        
                        <asp:Label ID="Label12" runat="server" Text="Servicios Afectados"></asp:Label>
                    </td>
                    <td  >
                        <asp:DropDownList ID="cmbServiciosAfectados" runat="server">
                         </asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td valign="top">
                       
                        &nbsp;</td>
                    <td valign="top" >
                         
                        <asp:Button ID="btnAgregarServicio" runat="server" Text="Agregar Servicio" 
                            onclick="btnAgregarServicio_Click" />
                         
                       </td>
                </tr>
                <tr>
                <td >
                    
                </td>
                <td >
                    <asp:GridView ID="gvServicios"  runat="server" AutoGenerateColumns="false"
                            OnRowDeleting="gvServicios_RowDeleting" >
                            <Columns>
                             <asp:BoundField DataField="Id" HeaderText="Id" Visible ="false" />
                             <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div id="noprint0">
                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/Images/delete.gif" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
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
        <div id="divtblPersonas">
            <asp:UpdatePanel ID="UpdatePersonas" runat="server">
                <ContentTemplate>
                    <table id="carga_empleado">
                       
                      <tr>
                                        <td style="width: 124px">
                                            Empleado
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbEmpleado" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </Tr>
                      <tr>
                                        
                                                   <td style="width: 124px">
                                                       
                                                   </td>
                                                   <td>
                                                       <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" 
                                                           OnClick="btnAsignaEmpleadoSolicitud_Click" Text="Asignar Empleados" 
                                                           Width="140px" /></td>
                                        
                                            </tr>
              
                    </table>
                    <table id="grilla_empleados">
                         <tr>
                            <td valign="top" style="width: 154px">
                                Personal Asignado</td>
                            <td valign="top" style="width: 509px">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 154px">
                                &nbsp;</td>
                            <td valign="top" style="width: 509px">
                                <asp:GridView ID="gvSolicitudPersonas" runat="server" 
                                    AutoGenerateColumns="False" 
                                    OnRowDeleting="gvPersonas_RowDeleting" >
                                    <Columns>
                                        
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                        <asp:BoundField DataField="Id_Solicitud" HeaderText="Id_Solicitud" Visible="False" />
                                        <asp:BoundField DataField="Id_Empleado" HeaderText="Empleado" Visible="False" />
                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div id="noprint" onclick = "RendirPersona(  <%# Eval("Id") %>) ">
                                                    
                                                    <asp:ImageButton ID="ImageButton1" 
                                                        runat="server" 
                                                        CausesValidation="False"  
                                                       
                                                        ImageUrl="~/Images/b_edit.gif" 
                                                        Text="Editar" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 154px">
                                &nbsp;
                            </td>
                            <td valign="top" style="width: 509px">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 154px">
                                &nbsp;
                            </td>
                            <td style="width: 509px" valign="top">
                                                        &nbsp;</td>
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
        <div id="divTraslados">
            <asp:UpdatePanel ID="UpdateVehiculos" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 58px" valign="top">
                                Traslados</td>
                            <td style="width: 165px" valign="top" class="style4">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 58px" valign="top">
                                Vehiculos</td>
                            <td style="width: 165px" valign="top" class="style4">
                                <asp:DropDownList ID="cmbVehiculo" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                               
                                </td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 58px">
                                &nbsp;
                                
                            </td>
                            <td style="width: 165px" valign="top" class="style4">
                                 <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" 
                                    OnClick="btnAsignaVehiculoSolicitud_Click" Text="Asignar Vehiculo" /></td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 58px" valign="top">
                                &nbsp;</td>
                            <td style="width: 165px" valign="top" class="style4">
                 
                            </td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table>
                    <tr>
                    <td>
               <asp:GridView ID="gvSolicitudVehiculos" runat="server" 
                                    AutoGenerateColumns="False" OnRowDeleting="gvSolicitudVehiculos_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                                        <asp:BoundField DataField="Id_Solicitud" HeaderText="Id_Solicitud" Visible="false" />
                                        <asp:BoundField DataField="Id_Vehiculo" HeaderText="id_Vehiculo" Visible="false" />
                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div ID="noprint3" onclick='RendirVehiculo( <%# Eval("Id") %>)'>
                                                    <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" 
                                                        ImageUrl="~/Images/b_edit.gif" Text="Editar" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div ID="noprint1">
                                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" 
                                                        CommandName="Delete" ImageUrl="~/Images/delete.gif" Text="Delete" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>                    
                    </td>
                    </tr></table>
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
        <div id="divadjuntos">
            <asp:UpdatePanel ID="UpdateAdjuntos" runat="server">
                <ContentTemplate>
                         </ContentTemplate>
            </asp:UpdatePanel>
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
                                <uc4:Adjuntos ID="Adjuntos1" runat="server" TipoSol="MantenimientoCorrectivoRendicion" />
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
