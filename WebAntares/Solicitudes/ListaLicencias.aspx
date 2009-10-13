<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaLicencias.aspx.cs" Inherits="Solicitudes_ListaLicencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                Solicitudes de licencia
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td align="center">
                Empleado
            </td>
            <td>
                <asp:DropDownList ID="cmbEmpleado" runat="server" CssClass="text_custom" AutoPostBack="True" 
                    onselectedindexchanged="cmbEmpleado_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            
                <asp:UpdatePanel ID="upLicencias" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" onrowdeleting="GridView1_RowDeleting" 
                            onrowediting="GridView1_RowEditing"
                            EmptyDataText="No se han encontrado licencias para el empleado seleccionado." 
                            onpageindexchanging="GridView1_PageIndexChanging">
                            <Columns>
                            <asp:BoundField DataField="id" Visible="false"/>
                            <asp:BoundField DataField="IdSolicitud" HeaderText="Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FechaFin" HeaderText="Fin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px"/>
                            <asp:BoundField DataField="Duracion" HeaderText="Duración" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/Images/calendario.gif" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("IdSolicitud") %>' />
                            </ItemTemplate>
                         </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/basuravacio.gif" DeleteText="Eliminar" HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                ShowCancelButton="False" ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbEmpleado" />
                    </Triggers>
                </asp:UpdatePanel> 
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
    </table>
</asp:Content>

