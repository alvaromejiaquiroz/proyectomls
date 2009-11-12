﻿<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="TiemposPersonal.aspx.cs" Inherits="Solicitudes_TiemposPersonal" Title="Untitled Page" %>

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
                    Text="Aceptar" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            
    </table>
        
     </ContentTemplate>
     </asp:UpdatePanel>
     
          <asp:GridView ID="gvTiempos" runat="server" AutoGenerateColumns="true" 
                    onrowcreated="gvTiempos_RowCreated" onrowdatabound="gvTiempos_RowDataBound"  Visible="true">
                    <Columns>
                    </Columns>
            </asp:GridView>
    
     
</asp:Content>

