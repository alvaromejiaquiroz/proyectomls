<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaLicencias.aspx.cs" Inherits="Solicitudes_ListaLicencias" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
 
   
    
    <table style="width:100%;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <tr>
            <td>
                Empleado</td>
            <td>
                <asp:DropDownList ID="cmbEmpleado" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="cmbEmpleado_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
              
                
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
 
 
    <asp:GridView ID="GridView1" runat="server" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        AutoGenerateColumns="False" onrowdeleting="GridView1_RowDeleting" 
        onrowediting="GridView1_RowEditing">
        <Columns>
        <asp:BoundField DataField="id" HeaderText="id"  Visible="false"/>
        <asp:BoundField DataField="IdSolicitud" HeaderText="Solicitud" />
        <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" />
        <asp:BoundField DataField="FechaFin" HeaderText="Fin" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
        <asp:BoundField DataField="Duracion" HeaderText="Duracion" />
        
        
            <asp:CommandField 
            ButtonType="Image" 
            DeleteImageUrl="~/images/delete.gif" 
            EditImageUrl="~/images/b_edit.gif" 
            ShowCancelButton="False" 
            ShowDeleteButton="True" 
            ShowEditButton="True" />

        </Columns>
    </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
      
    </table>
    </ContentTemplate>
     </asp:UpdatePanel>
 
    </table>
</asp:Content>

