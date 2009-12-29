<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TiemposPersonal.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Personal Antares" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
     <ContentTemplate>
           
    <table style="width:100%;">
        <tr>
            <td>Fecha : </td>
            <td>  
            <asp:TextBox ID="txtDesde" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vFecha" runat="server" ValidationGroup="tiempos"
            ControlToValidate="txtDesde" Display="None" ErrorMessage="Debe Seleccionar una Fecha"
            ></asp:RequiredFieldValidator>
            <asp:ImageButton ID="imgDesde" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
            <cc1:CalendarExtender ID="ceDesde" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDesde" CssClass="cal_Theme1" 
                TargetControlID="txtDesde">
            </cc1:CalendarExtender>
          
            </td>
            <td>    </td>
                         
        </tr>
        <tr>
            <td> Semana del Año:</td>
            <td> 
                <asp:TextBox ID="txtSemanaAño" runat="server" CssClass="text_custom" 
                    MaxLength="10" Width="141px"></asp:TextBox>
            </td>
            <td> &nbsp;</td>
                         
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  CssClass="button_custom"
                    Text="Aceptar"  ValidationGroup="tiempos" CausesValidation="true"/>
                    <asp:ValidationSummary ID="vSumary" runat="server" 
                    DisplayMode="BulletList" 
                    ValidationGroup="tiempos" />
            </td>
            <td>
            </td>
        </tr>
            
    </table>

<asp:Panel ID="pnlSolicitudesPreventivas" runat="server" visible="false">
Solicitudes Preventivas 
<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">

    <asp:GridView ID="gvTiemposPreventivo" runat="server" 
                    AutoGenerateColumns="true"        
                    Visible="false"        
                    CaptionAlign="Top" Width="80%" 
                    EmptyDataText=""
                    HorizontalAlign="Left"
                    RowStyle-HorizontalAlign ="Center">
        <RowStyle HorizontalAlign="Center" />
    </asp:GridView>
</div>
</asp:Panel>
<br />
<asp:Panel ID="pnlSolicitudesCorrectivas" runat="server" visible="false">
Solicitudes Correctivas 
<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
    <asp:GridView ID="gvTiemposCorrectivo" runat="server" 
                    AutoGenerateColumns="true"                
                    Visible="false"
                    EmptyDataText=""
                    RowStyle-HorizontalAlign ="Center"
                    CaptionAlign="Top" Width="80%">
                    </asp:GridView>

</div>
</asp:Panel>
<br />
<asp:Panel ID="pnlObras" runat="server" visible="false">
Obras e Instalaciones
<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
    <asp:GridView ID="gvTiemposObra" runat="server" 
                    AutoGenerateColumns="true"         
                    Visible="false"       
                    EmptyDataText=""
                    RowStyle-HorizontalAlign ="Center"
                    CaptionAlign="Top" Width="80%">
                    </asp:GridView>

</div>
</asp:Panel>
<br />
<asp:Panel ID="pnlCapa" runat="server" visible="false">

Capacitacion

<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
          <asp:GridView ID="gvCapacitacion" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="No se registraron horas segun los parametros ingresados"
          RowStyle-HorizontalAlign ="Center"
          Visible="true">
            </asp:GridView>
</div>
</asp:Panel>
<br />
<asp:Panel ID="pnlTG" runat="server" visible="false">
Tareas Generales
<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
          <asp:GridView ID="gvTareasGenerales" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="No se registraron horas segun los parametros ingresados"
          RowStyle-HorizontalAlign ="Center"
          Visible="true">
            </asp:GridView>
</div>
</asp:Panel>
<br />
<asp:Panel ID="pnlLicencias" runat="server" visible="False">
Licencias
<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
          <asp:GridView ID="gvLicencias" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="No se registraron horas segun los parametros ingresados"
          Visible="true" 
          RowStyle-HorizontalAlign="Center"
          
          >
            </asp:GridView>
</div>
    
    </asp:Panel>

     </ContentTemplate>
     </asp:UpdatePanel>
     
    
     
</asp:Content>

