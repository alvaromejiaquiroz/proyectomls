<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Facturas.aspx.cs" Inherits="Administracion_Facturas" Title="Untitled Page" UICulture="es" Culture="es-MX" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">

<style type="text/css">

#TablaFacturas     {    border-width: 1; border: solid;text-align: Left; width:800px;    }

#tdlabel { width:150px; vertical-align:top;}

#div_nrofactura { vertical-align:middle; padding-left:-5px; word-spacing:0;}
#tabla_nrofactura { width:50px;}
#col_cabecerafactura {width:50px;}
#col_guion      {width:3px;}
#col_nrofactura {width:50px;}



</style>


    <table id="TablaFacturas" >
        <tr>
            <td id="tdlabel">Fecha Factura : 
                </td>
            <td><uc1:jDatePick ID="dtFechaFactura" runat="server" />
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Numero Factura:
                </td>
            <td>
            <div id="div_nrofactura"> 
                <table id="tabla_nrofactura"> 
                    <tr>
                        <td id="col_cabecerafactura"><asp:TextBox ID="txtCabecera_NroFactura" 
                                runat="server" Width="49px" ></asp:TextBox></td>
                        <td id="col_guion">-</td>
                        <td id="col_nrofactura"><asp:TextBox ID="txtNroFactura" runat="server" ></asp:TextBox></td>
                    </tr>
                    </table>
            </div>
            
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Proveedor:
                </td>
            <td> <asp:DropDownList ID="cmbProveedor" runat="server">     </asp:DropDownList>
                </td>
            <td>
                         <div id="div_adelantos">
                             <asp:GridView ID="gvAdelantos" runat="server" Visible="false">
                             
                             </asp:GridView>
                        </div>
            </td>
        </tr>
        <tr>
            <td id="tdlabel">Fecha Recepcion :  
                </td>
            <td> <uc1:jDatePick ID="dtFechaRecepcion" runat="server" />
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">    Descripcion : 
                </td>
            <td>
            <div id="div_Decripcion">
                 <asp:TextBox ID="txtDescripcion" runat="server" Height="120px" 
                     TextMode="MultiLine" Width="260px"></asp:TextBox>
                 </div>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Plazo de pago:
                </td>
            <td> <asp:TextBox ID="txtPlazoPago" runat="server"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Importe Gravado:
                </td>
            <td> <asp:TextBox ID="txtImporteGravado" runat="server" Text = "0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Importe No Gravado:
                </td>
            <td> <asp:TextBox ID="txtImporteNOGravado" runat="server" Text="0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Porcentaje Iva:
                </td>
            <td> <asp:TextBox ID="txtPorcentajeIVa" runat="server" ></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel">Importe Total:
                </td>
            <td> <asp:TextBox ID="txtImporteTotal" runat="server" Text = "0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        
    </table>
    <asp:Button ID="btnAceptar" runat="server" Text="Confirma" 
    onclick="btnAceptar_Click" />
    
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtImporteTotal" ErrorMessage="Error al ingresar el Importe" 
        SetFocusOnError="True" 
        ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ControlToValidate="txtImporteNOGravado" ErrorMessage="Error al ingresar el Importe NO GRAVADO" 
        SetFocusOnError="True" 
        ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
        ControlToValidate="txtImporteGravado" ErrorMessage="Error al ingresar el Importe Gravado" 
        SetFocusOnError="True" 
        ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"></asp:RegularExpressionValidator>
    
<br />
<hr />

</asp:Content>

