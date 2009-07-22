<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Obras.aspx.cs" Inherits="Solicitudes_Obras"  %>


<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle"    TagPrefix="uc2" %>
<%@ Register Src="../Controles/cboSitios.ascx"  TagName="cboSitios"     TagPrefix="uc3" %>
<%@ Register Src="../Controles/Adjuntos.ascx"   TagName="Adjuntos"      TagPrefix="uc4" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">

    

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
 
   <uc2:SolDetalle ID="SolDetalle2" runat="server" />
    
   <div id="Solapas">
        <ul>
            <li><a href="#divtblCliente"><span>Cliente</span></a></li>
            <li><a href="#divtblTareas"><span>Tareas</span></a></li>
            <li><a href="#divtblPersonas"><span>Personal</span></a></li>
            <li><a href="#divVehiculos"><span>Vehiculos</span></a></li>
            <li><a href="#divPresupuesto"><span>Presupuesto</span></a></li>
            <li><a href="#divAdjuntos"><span>Adjuntos</span></a></li>
            <li><a href="#divFin"><span>Confirmación</span></a></li>
        </ul>
        <div id="divtblCliente">
            <asp:UpdatePanel ID="UpdateCliente" runat="server">
                <ContentTemplate>
                    <table>
                        <tr> <td style="width: 402px">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width: 166px">
                                        Cliente</td>
                                    <td style="width: 212px">
                                        <asp:DropDownList ID="cmbCliente" runat="server" Width="254px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 166px">
                                        Numero de Orden de Compra
                                        <br />
                                        o Contrato&nbsp;
                                    </td>
                                    <td style="width: 212px">
                                        <asp:TextBox ID="txtnroOC" runat="server" Width="254px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 166px">
                                        Contacto</td>
                                    <td style="width: 212px">
                                        <asp:TextBox ID="txtContacto" runat="server" Width="254px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 166px">
                                        EMail del Contacto</td>
                                    <td style="width: 212px">
                                        <asp:TextBox ID="txtMail" runat="server" Width="254px" Height="22px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 166px">
                                        Telefono Contacto</td>
                                    <td style="width: 212px">
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="254px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 166px">
                                        &nbsp;</td>
                                    <td style="width: 212px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            </td></tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divtblTareas">
        <asp:UpdatePanel ID="UpdatePanelTareas" runat="server">
        <ContentTemplate>
            <table id="tblTareas">
                <tr><td valign="top">Descripcion Tareas
                    <br />
                    Solicitadas</td><td>
                        <asp:TextBox ID="txtDescripcionTareas" runat="server" Width="250px" 
                            Height="190px" TextMode="MultiLine"></asp:TextBox>
                    </td></tr>
                <tr>
                <td>Fecha de Inicio</td>
                    <td>
                        <uc1:jDatePick ID="dtpInicio"  Name="desde" runat="server" CustomScript="customRange"/>
                    </td>
                </tr>
                <tr>
                    <td>Fecha de Entrega</td>
                    <td>
                        <uc1:jDatePick ID="dtpFin" name = "hasta" runat="server" CustomScript="customRange"/>
                    </td>
                </tr>
                <tr><td valign="top">Requisitos de Aprobacion</td><td>
                    <asp:TextBox ID="txtRequisitoAprova" runat="server" Width="254px" Height="85px" 
                        TextMode="MultiLine"></asp:TextBox>
                    </td></tr>
                <tr><td valign="top">Requisitos de Ingreso</td><td>
                    <asp:TextBox ID="txtRequisitoIngreso" runat="server" Width="254px" Height="85px" TextMode="MultiLine" ></asp:TextBox>
                    </td></tr>
                
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            
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
                                    OnClick="btnAsignarEmpleadoSolicitud_Click" />
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
        <div id="divVehiculos">
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
                                    OnRowDeleting="gvSolicitudVehiculos_RowDeleting" >
                                  
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" />
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
        <div id="divPresupuesto">
            <asp:UpdatePanel ID="PanelPresupuesto" runat="server">
                <ContentTemplate>
                    <table>
                        <tr> <td>
                            <table style="width:118%;">
                                <tr>
                                    <td>
                                        Presupuesto&nbsp; </td>
                                    <td>
                                        <asp:TextBox ID="txtPresupuesto" runat="server" Width="254px" CssClass="numeric"></asp:TextBox>
                                    </td>
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
                            </td></tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divAdjuntos">
            <table id="Table1">
                <tr>
                        <td><uc4:Adjuntos ID="Adjuntos2" runat="server" />                     </td>
                 </tr>
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
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td style="width: 509px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtContacto" ErrorMessage="RequiredFieldValidator" 
                                    ToolTip="Ingrese el Nombre y Apellido de la Persona a Contactar en el Cliente ">Ingrese 
                                el Nombre y Apellido del Contacto en el Cliente</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtMail" ErrorMessage="RegularExpressionValidator" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Ingrese 
                                una Direccion de Correo Electronica Valida</asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                    ControlToValidate="txtPresupuesto" ErrorMessage="RangeValidator" 
                                    MaximumValue="100000" MinimumValue="0" Type="Currency">Ingrese el Monto en 
                                Pesos</asp:RangeValidator>
                            </td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
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

