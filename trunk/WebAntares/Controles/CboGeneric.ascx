<%@ Control Language="C#" CodeFile="CboGeneric.ascx.cs" Inherits="Controles_cboGeneric" %>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left"  Wrap="False">
<asp:DropDownList ID="ddlCBO" runat="server" CssClass="input-text" OnSelectedIndexChanged="ddlCBO_SelectedIndexChanged"
 AutoPostBack="True">
</asp:DropDownList>
<asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
</asp:Panel>
    
