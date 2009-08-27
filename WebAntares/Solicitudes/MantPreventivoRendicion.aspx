<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantPreventivoRendicion.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/jDatePick.ascx" TagName="jDatePick" TagPrefix="uc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/cboSitios.ascx" TagName="cboSitios" TagPrefix="uc3" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register Src="../Controles/CboGeneric.ascx" TagName="CboGeneric" TagPrefix="uc5" %>
<%@ Register Src="../Controles/Tiempo.ascx" TagName="Tiempo" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">

    <script type="text/javascript">
        $(function() {
            $('#Solapas').tabs();
        });


        function RendirPersona(Id) {
            var oWin = window.open("PersonaHoras.aspx?id=" + Id ,"Horapersona", "width=600,height=350");
        }   
        function RendirVehiculo(idSolicitud,idVehiculo) {
            var oWin = window.open("VehiculosHoras.aspx?id=" + idSolicitud + "&idV=" + idVehiculo,"Kilometros", "width=600,height=350");
        }         
     </script>

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

            <cc1:TabContainer ID="tcMantenimientoPreventivo_Rendicion" runat="server" Height="450px">
                <cc1:TabPanel ID="tpTareas" HeaderText="Tareas" runat="server">
                <ContentTemplate>
                                            
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

                </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="tpPersonal" HeaderText="Personal" runat="server">
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
                <cc1:TabPanel ID="tpVehiculos" HeaderText="Vehículos" runat="server">
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
                <cc1:TabPanel ID="tpComplemento" HeaderText="Complemento" runat="server">
                                    <ContentTemplate>
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
                  
                  </ContentTemplate>
                </cc1:TabPanel>  
                <cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
                                    <ContentTemplate>
                       <table>
                            <tr>
                                <td valign="top" style="width: 80px" class="style4">
                                    Total de Gastos</td>
                                <td valign="top" style="width: 257px">
                                    <asp:TextBox ID="txtGastos" runat="server" CssClass="numeric" Width="197px"></asp:TextBox>
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
                <cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 600px">
                                            <uc4:Adjuntos ID="Adjuntos1" runat="server" TipoSol="MantenimientoPreventivoRendicion" />
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                                    <ContentTemplate>
                                        <div style="width:100%;text-align:center;margin-top:30px">
                                            <asp:Button ID="Button2" runat="server" OnClick="btnAceptarSolicitud_Click"
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
        
       
    
    

    <script  type="text/javascript">
        //funcion adicional para controlar el rango de fechas

        function customRange(input) {
            return { minDate: (input.id == hasta ? $("#" + desde).datepicker("getDate") : null),
                maxDate: (input.id == desde ? $("#" + hasta).datepicker("getDate") : null)
            };
        } 


    </script>

</asp:Content>
