<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListadoFacturas.aspx.cs" Inherits="Administracion_ListadoFacturas" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div style="position:absolute;">

<table id="titulo" width="700px" style="border-style: solid; border-color: #000000; border-width: 0px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                Listado de Facturas Pendientes
            </td>
        </tr>
</table>
<asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="false" 
        onrowcommand="gvFacturas_RowCommand">
     <Columns>
    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="id" HeaderText="id" Visible ="false"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Fecha" HeaderText="Fecha" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="n_factura" HeaderText="Nro.Factura" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Tipo" HeaderText="FC" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Id_Factura_Estados" HeaderText="Id_Factura_Estados " Visible ="false"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Estado" HeaderText="Estado " Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="descripcion" HeaderText="Descripción" HeaderStyle-Width="300px" ItemStyle-Width="300px" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
    <asp:BoundField DataField="Fecha_Recepcion_Factura" HeaderText="Fecha de Recepcion" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Plazo_Pago" HeaderText="Plazo Pago" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Importe_Gravado" HeaderText="Importe Gravado" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Importe_no_Gravado" HeaderText="Importe No Gravado" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Porcentaje_Iva" HeaderText="Porcentaje Iva" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Importe_Total" HeaderText="Importe Total" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Monto_Retencion" HeaderText="Monto Retencion" Visible ="true"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
    <asp:BoundField DataField="Id_Proveedor" HeaderText="Id_Proveedor" Visible ="False"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
 <asp:TemplateField  HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="Editar" ImageUrl="~/images/b_edit.gif" ToolTip="Editar Factura" />
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
 <asp:TemplateField HeaderText="Pagar" ItemStyle-HorizontalAlign="Center" ShowHeader="true">
        <ItemTemplate>
            <asp:ImageButton ID="imgEstado" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>' CommandName="CambiarEstado" ImageUrl="~/images/engranaje.gif" ToolTip="Pagar Factura" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
 <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Visible="True">
  <ItemTemplate>
    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="false" CommandArgument='<%# Bind("id") %>' CommandName="Eliminar" ImageUrl="~/Images/basuravacio.gif" ToolTip="Eliminar Factura" Visible="true" />
   </ItemTemplate>
   <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>                                
    
 </Columns>
</asp:GridView>
</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
