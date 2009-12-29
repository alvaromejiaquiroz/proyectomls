<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Obras.ascx.cs" Inherits="Controles_Obras" %>

<style type="text/css">
    .style2
    {
        width: 263px;
    }
</style>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            SOLICITUD DE OBRAS E INSTALACIONES
        </td>
    </tr>
    <tr>
        <td class="style2" >
        </td>
        <td  align="right" valign="top" style="padding-right:10px">
            <asp:Image ID="imgAntares" runat="server" AlternateText="Antares" ToolTip="Antares" ImageUrl="~/images/LogoAntaresThumbnail.png" />
        </td>
    </tr>
        <tr>
        <td colspan="3" style="height: 30px"></td>
    </tr>
    <tr>
        <td style="padding-left: 20px;" class="style2">
            Número de Solicitud
        </td>
        <td colspan="2">
            <asp:Literal ID="litNumero" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;" class="style2">
            Codigo de Obra
        </td>
        <td colspan="2">
            <asp:Label ID="lblCodigoObra" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Título
        </td>
        <td colspan="2">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
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
        <td style="padding-left: 20px;" class="style2">
        <div id="divReprogramacion" runat="server" visible="false" 
                style="position:inherit; width:inherit">
        <table >
            <tr>
                <td><asp:Literal ID="litReprogramacion" runat="server"></asp:Literal></td>
                <td ></td>
            </tr>
            <tr>
                <td>Aprobador de la Reprogramación:</td>
                <td style="width=300px"><asp:Literal ID="litAprobadorReprogramacion" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Fecha de Reprogramación:</td>
                <td ><asp:Literal ID="litFechaReprogramacion" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Motivo de Reprogramación:</td>
                <td ><asp:Literal ID="litMotivoReprogramacion" runat="server"></asp:Literal></td>
            </tr>
        
        </table>
            
        </div>
        </td>
    </tr>
   
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Cliente
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Cliente
        </td>
        <td colspan="2">
            <asp:Literal ID="litCliente" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Nro. de orden de compra o contrato
        </td>
        <td colspan="2">
            <asp:Literal ID="litNroOrden" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            E-mail de contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litMailContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Teléfono de contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litTelefonoContacto" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Tareas
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Descripción de tareas solicitadas
        </td>
        <td colspan="2">
            <asp:Literal ID="litDescripcionTareas" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Fecha de inicio
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaInicio" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Fecha de entrega
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaEntrega" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Requisitos de aprobación
        </td>
        <td colspan="2">
            <asp:Literal ID="litRequisitosAprobacion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" class="style2">
            Requisitos de ingreso
        </td>
        <td colspan="2">
            <asp:Literal ID="litRequisitosIngreso" runat="server"></asp:Literal>
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
        <td style="padding-left: 20px" class="style2">
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
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo Gasto" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                            <asp:BoundField DataField="Detalle" HeaderText="Concepto" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"/>
                            <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
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