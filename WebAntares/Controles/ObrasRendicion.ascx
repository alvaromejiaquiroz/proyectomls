﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObrasRendicion.ascx.cs" Inherits="Controles_ObrasRendicion" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            Solicitud de obras e instalaciones
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
        <td style="padding-left: 20px;width:350px">
            Número de Solicitud
        </td>
        <td colspan="2">
            <asp:Literal ID="litNumero" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;width:350px">
            Codigo de Obra
        </td>
        <td colspan="2">
            <asp:Label ID="lblCodigoObra" runat="server"></asp:Label>
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
            Cliente
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
            Nro. de orden de compra o contrato
        </td>
        <td colspan="2">
            <asp:Literal ID="litNroOrden" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Contacto
        </td>
        <td colspan="2">
            <asp:Literal ID="litContacto" runat="server"></asp:Literal>
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
        <td style="padding-left: 20px">
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
            Descripción del Trabajo Realizado
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;">
            <asp:label id="lblDescripcion_TrabajoRealizado" runat="server" BorderStyle="None"></asp:label>
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
        <td style="padding-left: 20px">
            Descripción de tareas solicitadas
        </td>
        <td colspan="2">
            <asp:Literal ID="litDescripcionTareas" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha de inicio
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaInicio" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha de entrega
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaEntrega" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Requisitos de aprobación
        </td>
        <td colspan="2">
            <asp:Literal ID="litRequisitosAprobacion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
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
        <td colspan="3" style="padding-left: 20px;font-weight:bold">
            Adjuntos
        </td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px;padding-right: 20px">
            <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No se han adjuntado archivos en la solicitud.">
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="Archivo" Visible="true" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Date" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="size" HeaderText="Tamaño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
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
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
    </tr>
    <tr>
        <td   style="padding-left:20px;font-weight:bolder;border-top: solid 1 black"> Firma Responsable</td>
        <td   style="padding-left:50px;font-weight:bolder;border-top: solid 1 black"> Firma Conforme Cliente</td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px"></td>
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