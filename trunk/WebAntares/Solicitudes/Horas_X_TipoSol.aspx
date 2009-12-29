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
        <td align="left" style="padding-left:20px" valign="top">  
        <asp:GridView ID="gvHorasActualesXTipo" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="Sin Informacion"
          Visible="true">
          <HeaderStyle HorizontalAlign="Center" />
          <AlternatingRowStyle HorizontalAlign ="Center" />
            </asp:GridView>
            </td>    
         
            <td align="left" style="padding-left:20px" valign="top">  
            
               <asp:GridView ID="gvHorasActualesXTipo_MES" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="Sin Informacion"
          Visible="true">
          <HeaderStyle HorizontalAlign="Center" />
          <AlternatingRowStyle HorizontalAlign ="Center" />
            </asp:GridView>
            </td>
</tr>
<tr>
    <td style="height:20px"></td>
</tr>
<tr>
        <td align="left" style="padding-left:20px" valign="top">Ranking de Horas Insumidas por el Personal </td>
        <td></td>
    </tr>
     <tr>
        <td align="left" style="padding-left:20px" valign="top">
        <asp:GridView ID="gvRankingHoras" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField   DataField="id_persona"  HeaderText="idPersona" Visible="false" />
            <asp:BoundField   DataField="legajo"  HeaderText="Legajo" Visible="true" />
            <asp:BoundField   DataField="Nombre"  HeaderText="Nombre" Visible="true" />
            <asp:BoundField   DataField="Horas"  HeaderText="Horas" Visible="true" />
        </Columns>
        </asp:GridView>
        
        </td>
        <td></td>
    </tr>

    </table>

         


     </ContentTemplate>
     </asp:UpdatePanel>
     
    
     
</asp:Content>

