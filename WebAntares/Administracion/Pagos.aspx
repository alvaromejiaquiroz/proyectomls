<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Pagos.aspx.cs" Inherits="Administracion_Pagos" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">


    <table style="width:100%;">
        <tr>
            <td>Nro Cheque
                &nbsp;</td>
            <td><asp:Label ID="lblNro" runat="server" Text="Label"></asp:Label>
                &nbsp;</td>
            <td>Banco
                &nbsp;</td>
            <td><asp:DropDownList ID="cmbBancos" runat="server">                </asp:DropDownList>
                &nbsp;</td>
        </tr>
        <tr>
            <td>Monto
                &nbsp;</td>
            <td><asp:Label ID="lblMonto" runat="server" Text="Label"></asp:Label>
                &nbsp;</td>
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
            <td>
                &nbsp;</td>
        </tr>
    </table>


</asp:Content>

