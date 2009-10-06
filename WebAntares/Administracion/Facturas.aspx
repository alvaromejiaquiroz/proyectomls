<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Facturas.aspx.cs" Inherits="Administracion_Facturas" Title="Untitled Page" UICulture="es" Culture="es-MX" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">

<style type="text/css">

#TablaFacturas     {    border-width: 1; border: solid;text-align: Left; width:800px;    }

#tdlabel { width:150px; vertical-align:top;}

#div_nrofactura { vertical-align:middle; padding-left:-5px; word-spacing:0;}
#div_proveedor { vertical-align:middle; padding-left:-5px; word-spacing:0;}
#tabla_nrofactura { width:50px;heigth:100px;}
#col_cabecerafactura {width:50px;}
#col_guion      {width:3px;}
#col_nrofactura {width:50px;}
.info {
    border: 1px solid;
    margin: 10px 0px;
    padding:15px 10px 15px 50px;
    background-repeat: no-repeat;
    background-position: 10px center;
}



    .style5
    {
        width: 332px;
    }



</style>


    <table id="Cabecera">
        <tr>
            <td id="tdlabel" class="style5">Fecha Factura : 
                </td>
            <td><uc1:jDatePick ID="dtFechaFactura" runat="server" />
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Numero Factura: </td>
            <td> 
            <div id="div_nrofactura" > 
                <table id="tabla_nrofactura"> 
                    <tr>
                        <td id="col_cabecerafactura"><asp:TextBox ID="txtCabecera_NroFactura" runat="server" Width="49px" ></asp:TextBox></td>
                        <td id="col_guion">-</td>
                        <td id="col_nrofactura"><asp:TextBox ID="txtNroFactura" runat="server" ></asp:TextBox></td>
                    </tr>
                    </table>
            </div>
            </td>
            <td> </td>
        </tr>
        <tr>
  
    <td> </td>
    <td> </td>
</tr>                 
     </table>
    <table id="tbl_proveedor">
            <tr>
                  <td>  <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                        <table >
                        <tr>
                            <td id="td2">Tipo Proveedor:</td>
                            <td> <asp:DropDownList ID="cmbTipoProveedor" runat="server" Width="600px"   AutoPostBack="true" onselectedindexchanged="cmbTipoProveedor_SelectedIndexChanged">     </asp:DropDownList> </td>
                            <td> </td>
                        </tr>
                        <tr>
                        <td id="td3" valign="top">Proveedor:</td>
                        <td valign="top"> <asp:DropDownList ID="cmbProveedor" runat="server" Width="100%"  Enabled=false>     </asp:DropDownList>
                            </td>
                        <td>
                                     <div ID="div_adelantos">
                                         <asp:GridView ID="gvAdelantos" runat="server" Visible="false">
                                         </asp:GridView>
                                     </div>
                        </td>
                        </tr>
                        </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>  </td>
            </tr>
        </table>
    <table id="TablaFacturas" >
        <tr>
            <td id="tdlabel" class="style5">Tipo Factura:</td>
            <td> <asp:DropDownList ID="cmbTipoFactura" runat="server">     </asp:DropDownList>                 </td>
            <td> </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Fecha Recepcion:</td>
            <td> <uc1:jDatePick ID="dtFechaRecepcion" runat="server" />
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Descripcion :</td>
            <td>
            <div id="div_Decripcion">
                 <asp:TextBox ID="txtDescripcion" runat="server" Height="120px" 
                     TextMode="MultiLine" Width="100%"></asp:TextBox>
                 </div>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Plazo de pago:
                </td>
            <td> <asp:TextBox ID="txtPlazoPago" runat="server"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Importe Gravado:
                </td>
            <td> <asp:TextBox ID="txtImporteGravado" runat="server" Text = "0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Importe No Gravado:
                </td>
            <td> <asp:TextBox ID="txtImporteNOGravado" runat="server" Text="0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Porcentaje Iva:
                </td>
            <td> <asp:TextBox ID="txtPorcentajeIVa" runat="server" ></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        <tr>
            <td id="tdlabel" class="style5">Importe Total:
                </td>
            <td> <asp:TextBox ID="txtImporteTotal" runat="server" Text = "0,00"></asp:TextBox>
                </td>
            <td>
                </td>
        </tr>
        
    </table>
    <br />
    <asp:Button ID="btnAceptar" runat="server" Text="Confirma"  onclick="btnAceptar_Click" />
    
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

 <%--<div id="info" class="info" runat="server" visible="false">      <p> Ok</p>  </div>--%>

</asp:Content>
