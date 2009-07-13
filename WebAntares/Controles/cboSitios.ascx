<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cboSitios.ascx.cs" Inherits="Controles_cboEmpresas" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="ddlGeneric" runat="server" AutoPostBack="false" 
    onselectedindexchanged="ddlGeneric_SelectedIndexChanged">
        </asp:DropDownList>
    </ContentTemplate>
</asp:UpdatePanel>

