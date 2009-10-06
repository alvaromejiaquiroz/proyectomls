﻿<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaTareasGenerales.aspx.cs"
    Inherits="Solicitudes_ListaTareasGenerales" %>

<asp:Content ID="cListaTareasGenerales" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitudes de Tareas Generales
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
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting"
                            OnRowEditing="GridView1_RowEditing"
                            onpageindexchanging="GridView1_PageIndexChanging"
                            EmptyDataText="No existen Solicitudes"
                            >

                        <Columns>
                            <asp:BoundField DataField="iDSolicitud" HeaderText="Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FechaFin" HeaderText="Fecha de fin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="300px" />
                            <asp:BoundField DataField="Duracion" HeaderText="Duración" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar" ImageUrl="~/images/calendar.png" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("IdSolicitud") %>' />
                                </ItemTemplate>
                             </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/delete.gif" DeleteText="Eliminar" HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
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
