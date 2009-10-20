<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MantenimientoCorrectivoRendicion.ascx.cs" Inherits="Controles_MantenimientoCorrectivoRendicion" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            Reporte de mantenimiento correctivo
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
        <td style="padding-left: 20px;width:270px">
            Número
        </td>
        <td colspan="2">
            <asp:Literal ID="litNumero" runat="server"></asp:Literal>
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
            <asp:Literal ID="litEstado" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Falla
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Reportó falla
        </td>
        <td colspan="2">
            <asp:Literal ID="litReportoFalla" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Causa probable
        </td>
        <td colspan="2">
            <asp:Literal ID="litCausaProbable" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha de reporte
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaReporte" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Falla
        </td>
        <td colspan="2">
            <asp:Literal ID="litFalla" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Servicios
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Plazo
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Plazo de atención
        </td>
        <td colspan="2">
            <asp:Literal ID="litPlazo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Responsable
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Apellido y Nombres
        </td>
        <td colspan="2">
            <asp:Literal ID="litResponsable" runat="server"></asp:Literal>
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
            <asp:GridView ID="gvPersonal" runat="server" AutoGenerateColumns="False"
                Width="100%" EmptyDataText="No se han cargado las horas de ningún empleado.">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Empleado" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                    <asp:BoundField DataField="Detalle" HeaderText="Descripción" />
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
            <asp:GridView ID="gvVehiculos" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                Width="100%" onrowdatabound="gvVehiculos_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" style="border-style: solid; border-color: #ffffff; border-width: 1px">
                                <tr style="font-weight:bold;height:16px">
                                    <td align="center">
                                        Vehículo
                                    </td>
                                </tr>
                                <tr style="height:18px">
                                    <td>
                                        <asp:Literal ID="litVehiculo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvHorasVehiculos" runat="server" AutoGenerateColumns="False" 
                                            EmptyDataText="No se han cargado los kilómetros para este vehículo." Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="Kilometros" HeaderText="Kilómetros" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Horas" HeaderText="Duración" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
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
            Complemento
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Contacto de cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litContactoCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Nro. de orden
        </td>
        <td colspan="2">
            <asp:Literal ID="litNroOrden" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Teléfono de contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litTelefonoContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            E-mail de contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litMailContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Adjuntos
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No se han adjuntado archivos en la solicitud." onrowcommand="gvAdjuntos_RowCommand">
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
            Monto
        </td>
        <td colspan="2">
            <asp:Literal ID="litMonto" runat="server"></asp:Literal>
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
        Sistema de gestión de tareas - V 1.0 - ANTARES COM S.A
    </div>
    <div style="margin: 20px;text-align: center">
        <input type="button" class="button_custom" onclick="window.print()" value="Imprimir" />
    </div>
</asp:Panel>