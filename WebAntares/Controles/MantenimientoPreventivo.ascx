<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MantenimientoPreventivo.ascx.cs" Inherits="Controles_MantenimientoPreventivo" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            SOLICITUD DE MANTENIMIENTO PREVENTIVO
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 20px">
        </td>
        <td rowspan="3" align="right" valign="top" style="padding-right:10px">
            <asp:Image ID="imgAntares" runat="server" AlternateText="Antares" ToolTip="Antares" ImageUrl="~/images/LogoAntaresThumbnail.png" />
        </td>
    </tr>
        <tr>
        <td colspan="3" style="height: 30px"></td>
    </tr>
    <tr>
        <td style="padding-left: 20px;width:200px">
            Número
        </td>
        <td colspan="2">
            <asp:Literal ID="litNumero" runat="server"  ></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Título
        </td>
        <td colspan="2">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Estado
        </td>
        <td colspan="2">
            <asp:Literal ID="litEstado" runat="server" ></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Sitio
        </td>
        <td colspan="2">
            <asp:Literal ID="litSitio" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
      <tr>
        <td style="padding-left: 20px;width:400px">
        <div id="divReprogramacion" runat="server" visible="false" style="position:inherit;">
        <table >
            <tr>
                <td><asp:Literal ID="litReprogramacion" runat="server"></asp:Literal></td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td>Aprobador de la Reprogramación:</td>
                <td colspan="2"><asp:Literal ID="litAprobadorReprogramacion" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Fecha de Reprogramación:</td>
                <td colspan="2"><asp:Literal ID="litFechaReprogramacion" runat="server"></asp:Literal></td>
            </tr>
        
        </table>
            
            <br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Tareas
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="tarea" HeaderText="Tarea" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Inicio" HeaderText="Inicio" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Fin" HeaderText="Fin" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Personal
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvPersonal" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                    <asp:CheckBoxField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Vehículos
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvVehiculos" runat="server" AutoGenerateColumns="False"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Datos Cliente
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Contrato
        </td>
        <td colspan="2">
            <asp:Literal ID="litCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Contacto del Cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litContactoCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Nro. Orden del Cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litNroOrden" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Teléfono de Contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litTelefonoContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            E-mail de Contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litMailContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
    <td style="padding-left: 20px;">
    <div ID="pnlCalidad" runat="server" Visible="false">
                <table >
                <tr>
                    <td colspan="3" style="font-weight:bold;" >
                    Archivo de Calidad
                    </td>
                </tr>
                <tr>
                    <td >
                       <asp:GridView ID="gvCalidad" runat="server" AutoGenerateColumns="False"  EmptyDataText="No Existen Registros de Calidad Asignados" Width="100%" onrowcommand="gvCalidad_RowCommand">
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
                    </td>
                </tr>
                </table>
        </div>
    </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Adjuntos
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" 
                Width="100%" EmptyDataText="No se han adjuntado archivos" 
                onrowcommand="gvAdjuntos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="Archivo" Visible="true" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Date" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="size" HeaderText="Tamaño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAdjunto" runat="server" CausesValidation="False" CommandName="download" CommandArgument='<%# Bind("IdAdjunto") %>'
                                        ImageUrl="~/Images/guardar.gif" Text="download" ToolTip="Descargar el archivo" />
                                </ItemTemplate>
                               <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                          
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Presupuesto
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Gastos Estimados
        </td>
        <td colspan="2">
            <asp:Literal ID="litMonto" runat="server"></asp:Literal>
        </td>
    </tr>
     <tr>
    <td style="padding-left: 20px;padding-right:20px" colspan="3">
       <asp:GridView ID="gvGastos" runat="server" AutoGenerateColumns="false" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Detalle" HeaderText="Concepto" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px"/>
                            <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                    
    </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" 
                CssClass="button_custom" onclick="btnFinalizar_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
</table>
<asp:Panel ID="pnlImprimir" runat="server">
    <div style="margin: 20px;text-align: center">
       <asp:Label ID="VersionSistema" runat="server"></asp:Label>
    </div>
    <div style="margin: 20px;text-align: center">
        <input type="button" class="button_custom" onclick="window.print()" value="Imprimir" />
    </div>
</asp:Panel>