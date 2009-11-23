<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MantenimientoCorrectivo.ascx.cs" Inherits="Controles_MantenimientoCorrectivo" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            SOLICITUD DE MANTENIMIENTO CORRECTIVO
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
        <td style="padding-left: 20px;width:270px">
            Número
        </td>
        <td colspan="2">
            <asp:Label ID="litNumero" runat="server" Font-Bold="true"> </asp:LABEL>
            
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
            Síntoma
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
            Sitio
        </td>
    </tr>
    <tr>
        <td  style="padding-left: 20px">
            <asp:Literal ID="litSitio" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td style="padding-left: 20px;font-weight:bold">
            Plazo de atención
        </td>
        <td colspan="2">
            <asp:Literal ID="litPlazo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            
        </td>
        <td colspan="2" >
            <asp:Literal ID="litPenaliza" runat="server"></asp:Literal>
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
                Width="100%" EmptyDataText = "No hay vehiculos asignados">
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
            Datos del Cliente
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
            Contacto del cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litContactoCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Nro. de Orden del Cliente
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
            <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No se han adjuntado archivos en la solicitud."    onrowcommand="gvFiles_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdAdjunto" HeaderText="Id" ReadOnly="True" Visible="false" />
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