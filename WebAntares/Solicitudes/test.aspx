<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Solicitudes_test" MasterPageFile="~/site.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controles/Solicitud_Gastos.ascx" tagname="Solicitud_Gastos" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">    

    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal">
        <DynamicMenuStyle CssClass="IE8Fix" />
    </asp:Menu>
    <asp:Button runat="server" ID="btnSend" onclick="btnSend_Click" Text="mande"  />
    
    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
        <asp:ListItem>si</asp:ListItem>
        <asp:ListItem Selected="True">no</asp:ListItem>
    </asp:RadioButtonList>
    
<asp:TextBox ID="txtHasta" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
<asp:ImageButton ID="imgHasta" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
<cc1:CalendarExtender ID="ceHasta" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgHasta" CssClass="cal_Theme1"     TargetControlID="txtHasta">
</cc1:CalendarExtender>
                                               
    <uc1:Solicitud_Gastos ID="ucGastos" runat="server" />
                                               
</asp:Content>