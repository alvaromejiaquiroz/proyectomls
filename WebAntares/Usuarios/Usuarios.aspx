<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs"
    Inherits="Admin_Usuarios" %>

<%@ Register Src="../Controles/CboGeneric.ascx" TagName="CboGeneric" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <fieldset>
        <table>
            <tr style="display: none;">
                <td class="input-caption">
                    IdUsuario
                </td>
                <td>
                    <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="input-text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="input-caption">
                    Nombre
                </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input-text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="input-caption">
                    Perfil
                </td>
                <td>
                    <uc1:CboGeneric ID="cboPerfil" runat="server" NameDescript="Detalle" NameId="IdPerfil"
                        SourceData="perfiles" TipoOrigen="Tabla" />
                </td>
            </tr>
            <tr>
                <td class="input-caption">
                    Login
                </td>
                <td>
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="input-text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="input-caption">
                    Pass
                </td>
                <td>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="input-text"></asp:TextBox>
                </td>
            </tr>
                   <tr>
                <td class="input-caption">
                    Relacionado con Empleado
                </td>
                <td>
                    <uc1:CboGeneric ID="cboEmpleados" runat="server" NameDescript="Empleado" NameId="Id_empleados"
                        SourceData="v_personal" TipoOrigen="Vista" />
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                </td>
            </tr>
            <tr>
                <td colspan='2' class='barButtons'>
                    <asp:Button ID='cmdAccion' runat='server' Text='Aceptar' CssClass='input-button'
                        OnClick="cmdAccion_Click" />
                    <asp:Button ID='cmdCancelar' runat='server' CssClass='input-button' Text='Cancelar' />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay registros de datos para mostrar."
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting"
        OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" ReadOnly="True" SortExpression="IdUsuario" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="IdPerfil" HeaderText="IdPerfil" />
            <asp:BoundField DataField="loginname" HeaderText="Login" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <div id="noprint">
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                            ImageUrl="~/Images/b_edit.gif" Text="Ver" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <div id="noprint">
                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                            ImageUrl="~/Images/delete.gif" Text="Delete" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
