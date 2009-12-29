<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaTareasGenerales.aspx.cs"
    Inherits="Solicitudes_ListaTareasGenerales" %>

<asp:Content ID="cListaTareasGenerales" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                TAREAS GENERALES
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
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
                    <td style="padding-top:20px;padding-left:20px;height:30px">
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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting"
                            OnRowEditing="GridView1_RowEditing"
                            onpageindexchanging="GridView1_PageIndexChanging"
                            EmptyDataText="No existen Solicitudes"
                            >

                        <Columns>
                            <asp:BoundField DataField="IdSolicitud" HeaderText="Solicitud" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de inicio" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"  dataformatstring="{0:dd/MM/yyyy}"  />
                            <asp:BoundField DataField="FechaFin" HeaderText="Fecha de fin" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"   dataformatstring="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px" />
                            <asp:BoundField DataField="Horas" HeaderText="Duración" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                            <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/Images/versolicitud.gif" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("IdSolicitud") %>' />
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
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Content>
