<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Reporte_Obras.aspx.cs" Inherits="Solicitudes_Reporte_Obras" Title="Reporte Obra" %>

<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc2" %>
<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>
<%@ Register src="../Controles/uploadFile.ascx" tagname="uploadFile" tagprefix="uc3" %>
<%@ Register src="../Controles/Adjuntos.ascx" tagname="Adjuntos" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
   <script type="text/javascript">
        $(function() {
            $('#Solapas').tabs();
              
            $(".numeric").numeric();
             

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
          

        function RendirPersona(Id) {
            var oWin = window.open("personaHoras.aspx?id=" + Id ,"Horapersona", "width=600,height=500");
        }   
        function RendirVehiculo(idSolicitud,idVehiculo) {
            var oWin = window.open("VehiculosHoras.aspx?id=" + idSolicitud + "&idV=" + idVehiculo,"Kilometros", "width=600,height=500");
        } 
     </script>
        
        

<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <uc2:SolDetalle ID="SolDetalle1" runat="server" />
    
   <div id="Solapas">
        <ul>
            
            <li><a href="#divtblTareas"><span>Tareas</span></a></li>
            <li><a href="#divTraslados"><span>Traslados</span></a></li>
            <li><a href="#divtblPersonas"><span>Personal</span></a></li>
            <li><a href="#divConfirmacionCliente"><span>Conformidad Cliente</span></a></li>
            <li><a href="#divGastos"><span>Presupuesto</span></a></li>
            <li><a href="#divAdjuntos"><span>Adjuntos</span></a></li>
            <li><a href="#divFin"><span>Confirmación</span></a></li>
        </ul>
       
        <div id="divtblTareas">
        <asp:UpdatePanel ID="UpdatePanelTareas" runat="server">
        <ContentTemplate>
            <table id="tblTareas">
                <tr><td valign="top">Tareas Solicitadas</td><td>
                        <asp:TextBox ID="txtTareasSolicitadas_Desc" runat="server" Height="110px" 
                            Width="350px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                    </td></tr>
                
                <tr>
                    <td valign="top">
                        Tarea Realizada</td>
                    <td>
                        <asp:TextBox ID="txtTareaRealizada" runat="server" Height="56px" 
                            TextMode="MultiLine" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                
                <tr><td valign="top">Descripcion</td><td>
                   
                    <asp:TextBox ID="txtDescripcionTarea" runat="server" Height="82px" 
                        TextMode="MultiLine" Width="350px"></asp:TextBox>
                    </td></tr>
                
                <tr>
                    <td>
                        <asp:Button ID="btnAgregaTareaAObra" runat="server" Text="Agregar Tarea" onclick="btnAgregaTareaAObra_Click" 
                           />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:GridView ID="gvTareasObras" runat="server" 
                            AutoGenerateColumns="False" onrowdeleting="gvTareasObras_RowDeleting" 
                            onselectedindexchanged="gvTareasObras_SelectedIndexChanged" >
                            
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                <asp:BoundField DataField="Tarea" HeaderText="Tarea" />
                                <asp:BoundField DataField="DescripcionTareas" HeaderText="Descripcion" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div ID="noprint2">
                                            <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" 
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
        <div id="divTraslados">
            <asp:UpdatePanel ID="UpdateVehiculos" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 104px" valign="top">
                                Traslados</td>
                            <td style="width: 509px" valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 104px" valign="top">
                                Vehiculo</td>
                            <td style="width: 509px" valign="top">
                                <asp:DropDownList ID="cmbVehiculo" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 104px">
                                &nbsp;
                                <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" 
                                    OnClick="btnAsignaVehiculoSolicitud_Click" Text="Asignar Vehiculo" />
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
                        <tr>
                            <td valign="top" style="width: 104px">
                            
                                </td>
                            <td valign="top" style="width: 509px">

                                </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 104px">
                                Vehiculos</td>
                            <td style="width: 509px" valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 104px" valign="top">
                                &nbsp;</td>
                            <td style="width: 509px" valign="top">
                 
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
        <div id="divtblPersonas">
            <asp:UpdatePanel ID="UpdatePersonas" runat="server">
                <ContentTemplate>
                    <table id="carga_empleado">
                       
                      <tr>
                                        <td>
                                            Empleado
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbEmpleado" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </Tr>
                      <tr>
                                        
                                                   <td>
                                                       <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" 
                                                           OnClick="btnAsignaEmpleadoSolicitud_Click" Text="Asignar Empleados" />
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                        
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
        <div id="divConfirmacionCliente">
      <asp:UpdatePanel ID="updatePanel_Conformidad" runat="server">
                <ContentTemplate>
                    <table>
                        <tr> <td >
                            
                            Recibio Conforme Cliente?</td>
                            <td>
                               
                                <asp:RadioButtonList ID="rdbConformeCli" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem >Si</asp:ListItem>
                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Se Cumplieron los requisitos de&nbsp; aprobacion?</td>
                            <td>
                               
                               
                                <asp:RadioButtonList ID="rdbCumplieronReq" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem >Si</asp:ListItem>
                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                               
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Entrega al Cliente</td>
                            <td>
                                <uc1:jDatePick ID="dtpFechaEntregaCliente" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
         </div>
        <div id="divGastos">
            <asp:UpdatePanel ID="PanelPresupuesto" runat="server">
                <ContentTemplate>
                    <table>
                        <tr> <td>
                            <table style="width:118%;">
                                <tr>
                                    <td>
                                        Gastos&nbsp; </td>
                                    <td>
                                        <asp:TextBox ID="txtGastos" runat="server" Width="254px" CssClass="numeric"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        Ingrese solo numeros separados por . o coma</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            </td></tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divAdjuntos">
            <table id="tblAdjuntos">
            <tr><td>
                    <uc3:uploadFile ID="uploadFile1" runat="server" />
                                
                    <uc4:Adjuntos ID="Adjuntos1" runat="server" />
                    </td></tr>
            </table>
        </div>
        <div id="divFin">
          <table>
        <tr>
            <td valign="top">
                &nbsp;
            </td>
            <td style="width: 509px" valign="top">
                <asp:Button ID="btnAceptarSolicitud" runat="server" 
                    Text="Confirmar Solicitud" onclick="btnAceptarSolicitud_Click" 
                     />
            </td>
            <td valign="top">
               <asp:Button ID="btnCancelarSolicitud" runat="server" 
                    Text="Cancelar Solicitud" onclick="btnCancelarSolicitud_Click" 
                     />
            </td>
            <td valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td style="width: 509px" valign="top">
            </td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
        </tr>
    </table>
         </div>
         
        </div>
        
       
   <script>
        //funcion adicional para controlar el rango de fechas

        function customRange(input) {
            return { minDate: (input.id == hasta ? $("#" + desde).datepicker("getDate") : null),
                maxDate: (input.id == desde ? $("#" + hasta).datepicker("getDate") : null)
            };
        } 


    </script>

</asp:Content>

