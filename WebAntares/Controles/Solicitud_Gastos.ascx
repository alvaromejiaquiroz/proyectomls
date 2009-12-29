<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Solicitud_Gastos.ascx.cs" Inherits="Controles_Gastos" %>


<asp:UpdatePanel ID="UpdateGastos" runat="server">
<ContentTemplate>

    <asp:Panel ID="pnlGastos" runat = "server" >
    
        <table width="400px" class="text_custom">
        
            <tr>
            <td valign="top" style="padding-left: 20px" >Tipo</td>
                <td>
                    <asp:DropDownList ID="cmbTipo" runat="server"  CssClass="text_custom"></asp:DropDownList>
                </td>
                <td valign="top">
                    Detalle</td>
                <td>
                    <asp:TextBox ID="txtDetalle" runat="server" CssClass="text_custom" 
                        ValidationGroup="Gastos"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td style="padding-left: 20px" valign="top">
                    Total</td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" ValidationGroup ="Gastos" CssClass="text_custom" ></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td  style="padding-left: 20px" valign="top">
                </td>
                <td></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
  
                <asp:Button ID="Button1" runat="server" Text="Agregar" ValidationGroup="Gastos" 
                onclick="Button1_Click"  CssClass="button_custom"/>
                <asp:ValidationSummary ID="vsGastos" runat="server" ValidationGroup="Gastos" />
                
                 <asp:RegularExpressionValidator  runat ="server" 
                    ID="RegularExpressionValidator1" ControlToValidate="txtDetalle"
                    ErrorMessage = "Ha ingresado algun caracter invalido"
                    ValidationGroup="intervencion" ValidationExpression="(?<=^|>)[^><]+?(?=<|$)" 
                    Display="None">
                </asp:RegularExpressionValidator>          
                
                <asp:CompareValidator ID="cpValidadorTipo" runat="server" 
                ErrorMessage="Debe Seleccionar un Tipo de Gasto"
                Display="None" 
                ControlToValidate="cmbTipo" Operator="GreaterThan"  ValueToCompare ="0"
                ValidationGroup="Gastos"            >

                <asp:CompareValidator ID="cvPresupuesto" runat="server" 
                ErrorMessage="El monto del presupuesto no es válido."
                Display="None" ValidationGroup="Gastos" 
                ControlToValidate="txtTotal"
                Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="El importe del gasto a cargar"
                Display="None"
                ControlToValidate = "txtTotal" ValidationGroup ="Gastos">
                </asp:RequiredFieldValidator>
                </asp:CompareValidator>
            </asp:Panel>
            <br />
            <br />
            
            <table>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlGastosSolInicial" runat="server" Visible = "false">
                    <table>
                    <tr>
                        <td style="font-weight:bold">Gastos Presupuestados</td>
                    </tr>
                         <tr>
               <td colspan="2">
                <asp:GridView ID="gvGastosSolicitudInicial" runat="server" 
                    AutoGenerateColumns="false" 
                    AllowPaging="true" 
                    onpageindexchanging="gvGastosSolicitudInicial_PageIndexChanging" PageSize="5"
                    Width="100%"
                    RowStyle-BackColor="GrayText">
                    <Columns>
                      <asp:BoundField DataField="Id" Visible="false"/>
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="20px"/>
                        <asp:BoundField DataField="Detalle" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"/>
                        <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="50px"/>
                    </Columns>
                </asp:GridView>
            </td>
            </tr>
                        <tr>
                            <td>Total:</td>
                            <td align="left"><asp:Label ID="lblGastosSolicitudInicial" runat="server"></asp:Label></td>
                        </tr>
                        
            </table>
            </asp:Panel>
                </td>
                <tr>
                    <td style="height:20px"> </td>
                </tr>
            </tr>
            <tr>
                        <td style="font-weight:bold">Gastos Incurridos</td>
                    </tr>
                       
              <tr>
                <td colspan="2">
                    <asp:GridView ID="gvGastos" runat="server" 
                        AutoGenerateColumns="false" 
                        AllowPaging="true" 
                        onpageindexchanging="gvGastos_PageIndexChanging" PageSize="5"
                        OnRowDeleting="gvGastos_RowDeleting"
                        Width="100%" 
                        onrowcreated="gvGastos_RowCreated">
                        <Columns>
                          <asp:BoundField DataField="Id" Visible="false"/>
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px"/>
                            <asp:BoundField DataField="Detalle" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"/>
                            <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="50px"/>
                            
                            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="True">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEliminar" runat="server" 
                                    CausesValidation="False" 
                                    CommandName="Delete"
                                    ImageUrl="~/Images/basuravacio.gif" Text="Eliminar" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>          
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>Total:</td>
                <td align="left"><asp:Label ID="lblTotal" runat="server"></asp:Label></td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>