<%@ Page Title="Solicitudes" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Solicitudes.aspx.cs" Inherits="Solicitudes_Solicitudes" %>

<%@ Register Src="../Controles/cboTipoSolicitud.ascx" TagName="cboTipoSolicitud"
    TagPrefix="uc1" %>
<%@ Register src="../Controles/CboGeneric.ascx" tagname="CboGeneric" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        Nro. Solicitud</td>
                    <td>
                        <asp:TextBox ID="txtNroSolicitud" runat="server"></asp:TextBox>
                    </td>
                </tr>
                                <tr>
                                    <td>
                                        TipoSolicitud
                                    </td>
                                    <td>
                                        <a href="~/Controles/cboTipoSolicitud.ascx">
                                        <uc1:cboTipoSolicitud ID="cboTipoSolicitud1" runat="server" />
                                        </a>
                                    </td>
                </tr>
                                <tr>
                    <td>
                        Por Responsable:
                    </td>
                    <td>
                        <uc2:CboGeneric ID="cboPersonal" runat="server" NameId="id_empleados" NameDescript="Empleado" SourceData="vw_personas" TipoOrigen="Vista" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Por Estado:
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbEstados" runat="server">
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       
                        <asp:Button ID="cmdBuscar" Text="Buscar" runat="server" OnClick="cmdBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"  OnDataBound="GridView1_DataBound">
                            <Columns>
                                <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo Solicitud" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                    <HeaderStyle Width="6000px" Wrap ="false" ></HeaderStyle>
                                    <ItemStyle width="600px"  Wrap="false"> </ItemStyle>   
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="Status" HeaderText="Estado" />
                                
                                
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="Id_Reporte" HeaderText="Report" Visible = "false" />
                                
                                <asp:TemplateField HeaderText="Modificar Solicitud" 
                                    ItemStyle-HorizontalAlign="Center" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Bind("Solicitud") %>' CommandName="Editar" 
                                            ImageUrl="~/images/b_edit.gif" ToolTip="Editar Solicitud" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visualizar" ItemStyle-HorizontalAlign="Center" 
                                    ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Bind("Solicitud") %>' CommandName="Imprimir" 
                                            ImageUrl="~/images/calendar.png" ToolTip="Imprimir la Solicitud" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cambiar Estado" 
                                    ItemStyle-HorizontalAlign="Center" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEstado" runat="server" CausesValidation="False" 
                                            CommandArgument='<%# Bind("Solicitud") %>' CommandName="CambiarEstado" 
                                            ImageUrl="~/images/engranaje.gif" ToolTip="Cambiar Estado" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Visualizar Reporte" 
                                    ItemStyle-HorizontalAlign="Center" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgReporte" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Bind("Id_Reporte") %>' CommandName="Visualizar_Reporte" 
                                            ImageUrl="~/images/Reporte.gif" ToolTip="Reporte" Visible="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" 
                                    ShowHeader="true" Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Bind("Solicitud") %>' CommandName="Eliminar" 
                                            ImageUrl="~/images/delete.gif" ToolTip="Eliminar la Solicitud"  Visible = "false"/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                        </asp:GridView>
                      
                      <div id="divMensajesGrilla" runat="server" visible= "false">
                          <asp:Label ID="lblMensaje" runat="server" ></asp:Label>
                      </div>
                      
                      </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
