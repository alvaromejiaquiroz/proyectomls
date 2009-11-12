<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaCapacitacion.aspx.cs" Inherits="Solicitudes_ListaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitudes de Capacitación
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td align="center">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="False" 
                    onrowdeleting="GridView1_RowDeleting" 
                    EmptyDataText="No existen Solicitudes"
                    onrowediting="GridView1_RowEditing" 
                    AllowPaging="true"
                    onpageindexchanging="GridView1_PageIndexChanging">
                    <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Solicitud" Visible="true" />
                    <asp:BoundField DataField="ID_solicitud" HeaderText="IdSolicitud" Visible="false" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaFin" HeaderText="Fecha de fin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px" />
                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Duracion" HeaderText="Duración" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="AreaEstudio" HeaderText="Area de estudio" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Instructor" HeaderText="Instructor" HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/Images/calendario.gif" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("IdSolicitud") %>' />
                            </ItemTemplate>
                         </asp:TemplateField>
                        <asp:CommandField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        ButtonType="Image" 
                        EditImageUrl="~/images/b_edit.gif" 
                        ShowCancelButton="False" 
                        ShowEditButton="True" />
                        <asp:CommandField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        ButtonType="Image" 
                        DeleteImageUrl="~/Images/basuravacio.gif" 
                        ShowCancelButton="False" 
                        ShowDeleteButton="True" />            
                    </Columns>
                </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Content>

