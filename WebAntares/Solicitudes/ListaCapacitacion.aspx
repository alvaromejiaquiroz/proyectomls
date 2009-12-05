<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaCapacitacion.aspx.cs" Inherits="Solicitudes_ListaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
<asp:UpdatePanel ID="updPanelCapacitacion" runat= "server" >
<ContentTemplate>

    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitudes de Capacitación
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            <asp:Panel runat="server" ID="pnlBuscar" visible="false">
            <table>
            <tr>
            <td style="height:20px"></td>
            
            </tr>
                <tr>
                    <td style="padding-left:20px;">
                     Empleado
                    </td>
                    <td>
                     <asp:DropDownList ID="cmbEmpleados" runat="server" CssClass="text_custom" Width="250px"> </asp:DropDownList>
                    </td>
                </tr>            
            <tr>
                    <td style="padding-left:20px;height:30px">
                      <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click"  Text="Buscar" CssClass="button_custom" />
                    </td>
                    <td>
                     
                    </td>
                </tr>      
            </table>
            </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-left:20px;padding-right:20px">
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
                    <asp:BoundField DataField="IdSolicitud" HeaderText="Solicitud" Visible="true" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FechaFin" HeaderText="Fecha de fin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px" />
                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Horas" HeaderText="Duración" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/Images/versolicitud.gif" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("IdSolicitud") %>' />
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
    
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

