<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Horas_X_TipoSol.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Personal Antares" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
     <ContentTemplate>
           
    <table>
    <tr>
    <td align="left" style="padding-left:20px"> Horas En Año Actual</td>
   <td align="left" style="padding-left:20px">  Horas En Mes Actual</td>
    </tr>
<tr>
        <td align="left" style="padding-left:20px">  
        <asp:GridView ID="gvHorasActualesXTipo" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="Sin Informacion"
          Visible="true">
          <HeaderStyle HorizontalAlign="Center" />
          <AlternatingRowStyle HorizontalAlign ="Center" />
            </asp:GridView>
            </td>    
            <td align="left" style="padding-left:20px">  
            
               <asp:GridView ID="gvHorasActualesXTipo_MES" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="Sin Informacion"
          Visible="true">
          <HeaderStyle HorizontalAlign="Center" />
          <AlternatingRowStyle HorizontalAlign ="Center" />
            </asp:GridView>
            </td>
</tr>
    </table>

         


     </ContentTemplate>
     </asp:UpdatePanel>
     
    
     
</asp:Content>

