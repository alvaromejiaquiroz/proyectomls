<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="Intervencion.aspx.cs" Inherits="_Default" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc1" %>


<%@ Register src="../Controles/cboTipoSolicitud.ascx" tagname="cboTipoSolicitud" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
<asp:UpdatePanel ID="UpdateGeneracionSolicitud" runat="server">
                         <ContentTemplate>
    <table>
        <tr>
            <td style="width: 132px">
            </td>
            <td align="center" style="width: 833px">
                &nbsp;<asp:Label ID="Label1" runat="server" Text="Ingreso de Solicitud de Intervencion"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 132px">
            </td>
            <td style="width: 833px">
                <table style="width: 100%; height: 100%;">
                    <tr>
                        <td style="width: 3637px; height: 24px;">
                            <asp:Label ID="Label3" runat="server" Text="Tipo Solicitud"></asp:Label>
                        </td>
                        <td style="width: 3206px; height: 24px">
                            <uc2:cboTipoSolicitud ID="cboTipoSolicitud1" runat="server"  />
                        </td>
                        <td style="width: 6654px; height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 3637px" align="left" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Titulo de Referencia"></asp:Label>
                        </td>
                        <td align="left" style="width: 3206px" valign="top">
                            <asp:TextBox ID="txtTitulo" runat="server" Height="135px" Width="467px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 6654px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 3637px" valign="top">
                            <asp:Label ID="Label5" runat="server" Text="Cargada Por"></asp:Label>
                        </td>
                        <td align="left" style="width: 3206px" valign="top">
                            &nbsp;<asp:Label ID="lblUserLogueado" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 6654px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 3637px" valign="top">
                        </td>
                        <td align="left" style="width: 3206px" valign="top">
                            <asp:Label ID="lblFechaCreacionSolicitud" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 6654px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 3637px" valign="top">
                        </td>
                        <td align="left" style="width: 3206px" valign="top">
                            <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
                                Text="Siguiente" />
                        </td>
                        <td align="left" style="width: 6654px" valign="top">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 132px">
            </td>
            <td style="width: 833px">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 132px">
            </td>
            <td style="width: 833px">
            </td>
            <td>
            </td>
        </tr>
    </table>
    
                         </ContentTemplate>
</asp:UpdatePanel>
<script>
    function Validar() {
     //   alert($("#"+Fecha).val());
         
        return true;
    }
</script>
</asp:Content>
