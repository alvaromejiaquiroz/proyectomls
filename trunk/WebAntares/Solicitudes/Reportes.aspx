<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Reportes.aspx.cs" Inherits="Solicitudes_pruebacontroles" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>
<%@ Register src="../Controles/FechayHora.ascx" tagname="FechayHora" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="True" Height="50px" ReportSourceID="CrystalReportSource1" 
                Width="350px" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <report filename="Solicitudes_Correctivas.rpt">
                </report>
            </CR:CrystalReportSource>
        </td>
        <td>
            &nbsp;</td>
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
    <uc2:FechayHora ID="FechayHora1" runat="server" />
    
    
    
</asp:Content>




