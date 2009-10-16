<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="AprobacionSolicitudesGestion.aspx.cs" Inherits="Solicitudes_AprobacionSolicitudesGestion" 
EnableEventValidation="false"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
        <%--<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">--%>
        <table width="100%" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="2">
            LISTADO DE SOLICITUDES REALIZADAS
            <hr />
            </td>
        </tr>
        <tr>
            <td>
              <asp:UpdatePanel ID="upSSolicitudes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                            OnRowCommand="GridView1_RowCommand"
                            AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" 
                            onpageindexchanging="GridView1_PageIndexChanging" 
                            onload="GridView1_Load"  >
                            
                            
                            <Columns>
                                <asp:BoundField DataField="Solicitud" HeaderText="Nro.Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Left" ></asp:BoundField>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" dataformatstring="{0:dd/MM/yyyy}" htmlencode="false" ItemStyle-Font-Bold="true"  />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Id_Reporte" HeaderText="Report" Visible="false" HeaderStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="imgEstado" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Solicitudcx" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkVisualizar" runat="server" ToolTip="Visualizar Solicitud" ImageUrl="~/images/versolicitud.gif" Target="_blank" NavigateUrl='<%# "VisualizarSolicitud.aspx?id=" + Eval("Solicitud") %>' />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporte" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkReporte" runat="server" ToolTip="Visualizar Reporte" ImageUrl="~/images/verreporte.gif" Target="_blank" NavigateUrl='<%# "VisualizarReporte.aspx?id=" + Eval("Id_Reporte") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aprobar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAprobar" runat="server" CausesValidation="false" CommandArgument='<%# Bind("Solicitud") %>'
                                            CommandName="Aprobar" ImageUrl="~/images/aprobar.gif" ToolTip="Aprobar Solicitud" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rechazar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgRechazar" runat="server" CausesValidation="False" CommandArgument='<%# Bind("Solicitud") %>'
                                            CommandName="Rechazar" ImageUrl="~/images/borrar.gif" ToolTip="Rechazar Solicitud" />
                                  
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                     
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
      
    </table>
               

</asp:Content>

