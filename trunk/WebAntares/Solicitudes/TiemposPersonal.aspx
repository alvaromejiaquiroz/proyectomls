<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TiemposPersonal.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Untitled Page" %>

<%@ Register src="../Controles/FechayHora.ascx" tagname="FechayHora" tagprefix="uc1" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel" runat="server">
     <ContentTemplate>
           
    <table style="width:100%;">
        <tr>
            <td> Empleado :</td>
            <td>
                           <asp:DropDownList ID="cmbPersonal" runat="server">
                         </asp:DropDownList></td>
          
            <td></td>
        </tr>
        <tr>
            <td>Fecha : </td>
            <td>    <uc2:jDatePick ID="cmbFecha" runat="server"  /></td>
            <td>    </td>
                         
        </tr>
        <tr>
            <td> </td>
            <td> <asp:Button ID="Button1" runat="server" Text="Aceptar" 
                    onclick="Button1_Click" /></td>
            <td> </td>
                         
        </tr>
        <tr>
            <td></td>
            <td>
             <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="true" >
                                    <Columns>
                                    
                                    </Columns>
                </asp:GridView></td>
            <td></td>
        </tr>
    </table>
    
     </ContentTemplate>
     </asp:UpdatePanel>
     
</asp:Content>

