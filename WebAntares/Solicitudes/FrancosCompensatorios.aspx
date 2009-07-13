<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="FrancosCompensatorios.aspx.cs" Inherits="Solicitudes_FrancosCompensatorios" Title="Untitled Page" %>

<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc1" %>
<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <uc1:SolDetalle ID="SolDetalle1" runat="server" />
            </td>
        </tr>
        </table>
    <table >
        <tr>
            <td style="width: 116px; height: 8px">
                <asp:Label ID="Label1" runat="server" Text="Fecha "></asp:Label>
            </td>
            <td style="height: 8px; width: 139px;">
                <uc2:jDatePick ID="dtpInicio" name="desde"  runat="server"  CustomScript="customRange" />
            </td>
        </tr>
        <tr>
            <td style="width: 116px">
                <asp:Label ID="Label3" runat="server" Text="Duracion(Horas)" ></asp:Label>
            </td>
            <td style="width: 139px">
                <asp:TextBox ID="txtDuracion" runat="server" CssClass="numeric"></asp:TextBox>
            </td>
        </tr>
        </table>
    <table>
        <tr>
            <td style="width: 114px" valign="top">
                <asp:Label ID="Label4" runat="server" Text="Descripcion"></asp:Label>
            </td>
            <td style="width: 312px">
                <asp:TextBox ID="txtDescripcion" runat="server" Height="169px" Width="372px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 114px; height: 66px;" valign="top">
                <asp:Label ID="Label5" runat="server" Text="Con El Consentimiento de"></asp:Label>
            </td>
            <td style="width: 312px; height: 66px;">
                <table >
                    <tr>
                        <td>
                            <asp:TextBox ID="txtConsentimiento" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                RepeatDirection="Horizontal" TextAlign="Left">
                                <asp:ListItem>Si</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 114px">
                &nbsp;</td>
            <td style="width: 312px">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Aceptar" />
                <asp:Button ID="Button2" runat="server" Text="Cancelar" />
            </td>
        </tr>
        <tr>
            <td style="width: 114px">
            </td>
            <td style="width: 312px">
            </td>
        </tr>
    </table>
    
        

    </script>

</asp:Content>

