<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="PersonalListaCorreo.aspx.cs" 
Inherits="Usuarios_ListasCorreo" Title="Lista Correos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:UpdatePanel ID="upSolicitudes" runat="server" >
<ContentTemplate>
        <table>
    <tr>
        <td>Lista de Correo</td>
        <td><asp:Label ID="lblLista" runat="server" Font-Bold="true" ></asp:Label></td>
    </tr>
    <tr>
        <td>Personal</td>
        <td>
            <asp:DropDownList ID="cmbPersonal" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td colspan="2"><asp:Button ID="btnAceptar" Text="Agregar" runat="server" CssClass="button_custom"  CausesValidation="true" ValidationGroup="listas"
            onclick="btnAceptar_Click" />
    <asp:Button ID="btnSalir" Text="Volver" runat="server" CssClass="button_custom"  CausesValidation="false" 
            onclick="btnSalir_Click" /></td>
    </tr>
</table>
        <asp:CompareValidator ID="cvPersonal" runat="server" ErrorMessage="Debe seleccionar un empleado" ValidationGroup="listas" 
        ControlToValidate="cmbPersonal"
        ValueToCompare="0" Display ="None" Operator="GreaterThan">
        </asp:CompareValidator>
        <asp:CustomValidator ID="cvPersona" runat="server" ValidationGroup="listas"  Display="None"
            onservervalidate="cvPersona_ServerValidate"></asp:CustomValidator>
        <asp:ValidationSummary ID="vsListas" runat="server" ValidationGroup="listas" />
<br />
        <table>
    <tr>
        <td style="height:20px" valign="top">Listas de Email</td>
    </tr>
    <tr>
        <td>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" >
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" Visible="false"/>
                                <asp:BoundField DataField="idempleados" HeaderText="idempleados" Visible="false"/>
                                <asp:BoundField DataField="estado" HeaderText="estado" Visible="false"/>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="email"  HeaderText="e-mail" />
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40px"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="false" CommandArgument='<%# Bind("id") %>'
                                            CommandName="Eliminar" ImageUrl="~/Images/basuravacio.gif" ToolTip="Eliminar"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

