<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TiemposPersonal.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Untitled Page" %>

<%@ Register src="../Controles/FechayHora.ascx" tagname="FechayHora" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel" runat="server">
     <ContentTemplate>
           
    <table style="width:100%;">
        <tr>
            <td> Empleado :</td>
            <td>
                           <asp:DropDownList ID="cmbPersonal" runat="server" Height="16px" Width="138px">
                         </asp:DropDownList></td>
          
            <td></td>
        </tr>
        <tr>
            <td>Fecha : </td>
            <td>  
            <asp:TextBox ID="txtDesde" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                                <asp:ImageButton ID="imgDesde" runat="server" CausesValidation="false" ImageUrl="~/images/calendar.png" />
                                                <cc1:CalendarExtender ID="ceDesde" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDesde"
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
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Aceptar" />
            </td>
            <td>
            </td>
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

