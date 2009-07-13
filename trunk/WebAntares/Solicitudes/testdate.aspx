<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="testdate.aspx.cs" Inherits="Solicitudes_testdate" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



<%@ Register src="../Controles/FechayHora.ascx" tagname="FechayHora" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    
    <uc2:FechayHora ID="FechayHora1" runat="server" />
    
</asp:Content>

