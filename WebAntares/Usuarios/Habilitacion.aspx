<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Habilitacion.aspx.cs" Inherits="Usuarios_Habilitacion" Title="Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div style="top:0%;position:relative;">
    <table class="text_custom">
    <tr>
        <td >Perfil</td>
        <td ><asp:DropDownList ID="cmbPerfiles" runat="server" AutoPostBack="true" 
                onselectedindexchanged="cmbPerfiles_SelectedIndexChanged" CssClass="text_custom">
          </asp:DropDownList>
        </td>
    </tr>
    </table>
    </div>
    <br />
    <asp:Panel ID="pnlAcciones" runat = "server" Visible ="false">
    <table class="text_custom">
        <tr>
        <td>Acciones Habilitadas</td>
        
        </tr>
       <tr>
        <td colspan=2>
          <asp:GridView ID="gvAcciones" runat="server" 
            AutoGenerateColumns="False" Width="100%" 
            EmptyDataText="No Existen Registros" onrowdatabound="gvAcciones_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdAccion" HeaderText="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Objeto" HeaderText="Accion" HeaderStyle-HorizontalAlign="Left"/> 
                <asp:BoundField DataField="Valor" HeaderText="Valor" HeaderStyle-HorizontalAlign="Left"/> 
                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:CheckBox ID="chkHab" runat="server" Checked ="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
         </asp:GridView>
        </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" 
            onclick="btnConfirmar_Click" CssClass="button_custom" /></td>
    </tr>
    
    </table>
      <asp:CompareValidator ID="cvPerfiles" runat="server" 
      ErrorMessage="CompareValidator" ControlToValidate="cmbPerfiles" ValueToCompare="0" Operator ="GreaterThan" ValidationGroup="perfiles_acciones"></asp:CompareValidator>
      <asp:ValidationSummary ID="vsHabilitaciones" ValidationGroup="perfiles_acciones"  runat="server"/>     
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

