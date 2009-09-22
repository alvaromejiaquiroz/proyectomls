<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Solicitudes.aspx.cs"
    Inherits="Solicitudes_Solicitudes" %>

<%@ Register Src="~/Controles/cboTipoSolicitud.ascx" TagName="cboTipoSolicitud" TagPrefix="uc1" %>
<%@ Register Src="~/Controles/CboGeneric.ascx" TagName="CboGeneric" TagPrefix="uc2" %>
<asp:Content ID="cSolicitudes" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                Listado de Solicitudes
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Nro. de solicitud
            </td>
            <td>
                <asp:TextBox ID="txtNroSolicitud" runat="server" CssClass="text_custom"></asp:TextBox>
                <asp:CompareValidator ID="cvNroSolicitud" runat="server" Display="None" ErrorMessage="El número de solicitud no es válido." ControlToValidate="txtNroSolicitud" Operator="DataTypeCheck" Type="Integer" ValidationGroup="buscar"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Tipo de solicitud
            </td>
            <td>
                <uc1:cboTipoSolicitud ID="cboTipoSolicitud1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Responsable
            </td>
            <td>
                <uc2:CboGeneric ID="cboPersonal" runat="server" NameId="id_empleados" NameDescript="Empleado"
                    SourceData="vw_personas" TipoOrigen="Vista" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Estado
            </td>
            <td>
                <asp:DropDownList ID="cmbEstados" runat="server" CssClass="text_custom">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="cmdBuscar" Text="Buscar" runat="server" OnClick="cmdBuscar_Click"
                    CssClass="button_custom" ValidationGroup="buscar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="vsBuscar" runat="server" ValidationGroup="buscar" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upSolicitudes" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                            OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" 
                            onpageindexchanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Left" >
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="imgStatus" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Id_Reporte" HeaderText="Report" Visible="false" HeaderStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Solicitud") %>'
                                            CommandName="Editar" ImageUrl="~/images/b_edit.gif" ToolTip="Editar" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/images/calendar.png" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("Solicitud") %>' />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cambiar estado" ItemStyle-HorizontalAlign="Center"
                                    ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEstado" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Solicitud") %>'
                                            CommandName="CambiarEstado" ImageUrl="~/images/engranaje.gif" ToolTip="Cambiar estado" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporte" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkReporte" runat="server" ToolTip="Reporte" ImageUrl="~/images/calendar.png" Target="_blank" NavigateUrl='<%# "VisualizarReporte.aspx?id=" + Eval("Id_Reporte") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Solicitud") %>'
                                            CommandName="Eliminar" ImageUrl="~/images/delete.gif" ToolTip="Eliminar"
                                            Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmdBuscar" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
