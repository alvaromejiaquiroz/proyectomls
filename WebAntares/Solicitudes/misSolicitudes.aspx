<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="misSolicitudes.aspx.cs" Inherits="Solicitudes_misSolicitudes" %>

<%@ Register Src="../Controles/CboGeneric.ascx" TagName="CboGeneric" TagPrefix="uc1" %>
<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>
<%@ Register src="../Controles/Tiempo.ascx" tagname="Tiempo" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
Personal
    <uc1:CboGeneric ID="cboPersonal" runat="server" NameId="id_empleados" NameDescript="Empleado"
        SourceData="vw_personas" TipoOrigen="Vista" />
    <asp:GridView ID="gvMisSolicitudes" runat="server" OnSelectedIndexChanged="gvMisSolicitudes_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <div id="divtareasHoras" runat="server" style="display: none">
        <table>
            <tr>
                <td>
                    Id Tareas
                </td>
                <td>
                    <asp:TextBox ID="idTareas"></asp:TextBox>
                    <asp:TextBox ID="txtdetalletarea"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Empleado
                </td>
                <td>
                    <uc1:cbogeneric id="cboEmpleados" runat="server" tabla="personal" nameid="Id_empleados"
                        namedescript="Apellido" tipoorigen="Tabla" />
                </td>
            </tr>
            <tr>
                <td>
                    Inicio
                </td>
                <td>
                    <uc2:jdatepick ID="jdpFechaTarea" runat="server" Name="FechaTareaHora" />
                </td>
            </tr>
            <tr>
                <td>
                    Duracion
                </td>
                <td>
                    <uc3:tiempo id="Tiempo1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Comentario
                </td>
                <td>
                    <asp:TextBox ID="txtComentario" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btmSaveTarea" runat="server" Text="Guarda Tiempo Insumido" Style="margin-bottom: 0px"
                          />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="gvTareasRendidas" runat="server">
        <EmptyDataTemplate>
            No se encontraron datos para esta solicitud
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
