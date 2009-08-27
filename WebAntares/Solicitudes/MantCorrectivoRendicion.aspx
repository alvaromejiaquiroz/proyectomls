<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantCorrectivoRendicion.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
     <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
      <tr class="header_custom">
            <td align="center">
                Reporte de Mantenimiento Preventivo
            </td>
        </tr>
      <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
      <tr>
        <td>

            <cc1:TabContainer ID="tcMantenimientoCorrectivo_Rendicion" runat="server" Height="450px">
         
<cc1:TabPanel ID="tpFalla" HeaderText="Falla" runat="server">
     <ContentTemplate>
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
      </ContentTemplate>
      </cc1:TabPanel>
<cc1:TabPanel ID="tpServicios" HeaderText="Servicios" runat="server">
     <ContentTemplate>
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
     </ContentTemplate>
     </cc1:TabPanel>
<cc1:TabPanel ID="tpPlazo" HeaderText="Plazo" runat="server">
    <ContentTemplate>
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
     </ContentTemplate>
     </cc1:TabPanel>
<cc1:TabPanel ID="tpPersonas" HeaderText="Personal" runat="server">
    <ContentTemplate>
    
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
     </ContentTemplate>
     </cc1:TabPanel>
<cc1:TabPanel ID="tpTraslados" HeaderText="Vehiculos" runat="server">
    <ContentTemplate>
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
     </ContentTemplate>
     </cc1:TabPanel>
<cc1:TabPanel ID="tpComplemento" HeaderText="Falla" runat="server">
<ContentTemplate>
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
</ContentTemplate>
     </cc1:TabPanel>
<cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 600px">
                                            <uc4:Adjuntos ID="Adjuntos1" runat="server" TipoSol="MantenimientoPreventivoRendicion" />
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
<cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
                                    <ContentTemplate>
                       <table>
                            <tr>
                                <td valign="top" style="width: 80px" class="style4">
                                    Total de Gastos</td>
                                <td valign="top" style="width: 257px">
                                    <asp:TextBox ID="txtPresupuesto" runat="server" CssClass="numeric" Width="197px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" style="width: 80px">
                                    &nbsp;</td>
                                <td valign="top" style="width: 257px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top" class="style4" style="width: 80px">
                                    &nbsp;</td>
                                <td valign="top" style="width: 257px">
                                    &nbsp;</td>
                            </tr>
                        </table>
               
                  </ContentTemplate>
                </cc1:TabPanel>  
<cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                                    <ContentTemplate>
                                        <div style="width:100%;text-align:center;margin-top:30px">
                                            <asp:Button ID="Button4" runat="server" OnClick="btnAceptarSolicitud_Click"
                                                Text="Confirmar solicitud" ValidationGroup="solicitud" CssClass="button_custom" />
                                        </div>
                                        <div class="text_custom">
                                            <asp:BulletedList ID="BulletedList1" runat="server" ForeColor="Red" BulletStyle="NotSet">
                                            </asp:BulletedList>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="solicitud" />
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
            </cc1:TabContainer>
           </td>
      </tr>
     </table>
    
  

        
    <script type="text/javascript">
        //funcion adicional para controlar el rango de fechas

        function customRange(input) {
            return { minDate: (input.id == hasta ? $("#" + desde).datepicker("getDate") : null),
                maxDate: (input.id == desde ? $("#" + hasta).datepicker("getDate") : null)
            };
        } 


    </script>

</asp:Content>
