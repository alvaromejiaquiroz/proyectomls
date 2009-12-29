<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListasCorreo.aspx.cs" 
Inherits="Usuarios_ListasCorreo" Title="Lista Correos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
<asp:UpdatePanel ID="upSolicitudes" runat="server" >
<ContentTemplate>
        <table>
    <tr>
        <td>Nombre </td>
        <td><asp:TextBox ID="txtNombreLista" runat="server" Columns="30"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Descripcion</td>
        <td><asp:TextBox ID="txtDescripcion" runat="server" Columns="30"></asp:TextBox></td>
    </tr>
    <tr>
    <td colspan="2"><asp:Button ID="btnAceptar" Text="Agregar" runat="server" CssClass="button_custom"  CausesValidation="true" ValidationGroup="listas"
            onclick="btnAceptar_Click" /></td>
    </tr>
</table>
        <asp:RequiredFieldValidator id="rqvNombre" runat = "server" ControlToValidate="txtNombreLista" Display="None" ValidationGroup="listas" ErrorMessage="Debe ingresar un Nombre"></asp:RequiredFieldValidator>
        <%--<asp:RequiredFieldValidator id="rqvDescripcion" runat = "server" ControlToValidate="txtDescripcion" Display="None" ValidationGroup="listas" ></asp:RequiredFieldValidator>--%>
        <asp:ValidationSummary ID="vsListas" runat="server" ValidationGroup="listas" />
<br />
        <table>
    <tr>
        <td>Listas de Email</td>
    </tr>
    <tr>
        <td>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" >
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" Visible="false"/>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Descripcion"  HeaderText="Descripcion" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Bind("id") %>'
                                            CommandName="Editar" ImageUrl="~/images/editarsolicitud.gif" ToolTip="Editar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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

