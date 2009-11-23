<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TiemposPersonal.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
     <ContentTemplate>
           
    <table style="width:100%;">
        <tr>
            <td> Empleado :</td>
            <td>
                           <asp:DropDownList ID="cmbPersonal" runat="server" CssClass="text_custom">
                         </asp:DropDownList></td>
          
            <td></td>
        </tr>
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
                <asp:CustomValidator ID="CustomValidator1" runat="server"  ControlToValidate="cmbPersonal"
                    ValidationGroup="tiempos"
                    ErrorMessage="Seleccionar un empleados" 
                    onservervalidate="CustomValidator1_ServerValidate"  Visible="false" ></asp:CustomValidator>                     
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

<div style="padding-left:20px;padding-top:20px;padding-bottom:20px">
          <asp:GridView ID="gvTiempos" runat="server" 
          AutoGenerateColumns="true"                
          EmptyDataText="No ser registraron horas en segun los parametros ingresados"
          Visible="true">
            </asp:GridView>
</div>
        
     </ContentTemplate>
     </asp:UpdatePanel>
     
    
     
</asp:Content>

