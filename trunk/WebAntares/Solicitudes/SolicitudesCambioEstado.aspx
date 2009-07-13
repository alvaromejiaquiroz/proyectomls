<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="SolicitudesCambioEstado.aspx.cs"
    Inherits="Solicitudes_SolicitudesCambioEstado" Title="Untitled Page" %>

<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc1" %>
<%@ Register Src="../Controles/jDatePick.ascx" TagName="jDatePick" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <uc1:SolDetalle ID="SolDetalle1" runat="server" />
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td style="width: 83px">
                <asp:Label ID="Label1" runat="server" Text="El Trabajo fue :"></asp:Label>
            </td>
            <td style="width: 192px" align="left">
                <asp:DropDownList ID="cmbEstados" runat="server" Width="182px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 83px">
                &nbsp;</td>
            <td style="width: 192px" align="left">
                <asp:Button ID="btnAceptar" runat="server" OnClick="Button1_Click" Text="Aceptar" />
                                 <asp:Button ID="btnCancelar0" runat="server" 
                            Text="Cancelar" onclick="btnCancelar0_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
  
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="pnlReprogramado" runat="server">
                    <table style="width:100%;">
                    <tr>
                    <td style="width: 71px" valign="top">
                    Causa</td>
                    <td>
                    <asp:TextBox ID="txtCausa" runat="server" Height="103px" 
                    TextMode="MultiLine" Width="238px"></asp:TextBox>
                    </td>
                    <td>
                    &nbsp;</td>
                    </tr>
                    <tr>
                    <td style="width: 71px" valign="top">
                        <asp:Label ID="Label3" runat="server" Text="Aprobador"></asp:Label>
                        </td>
                    <td>
                    <asp:TextBox ID="txtAprobador" runat="server" Width="236px"></asp:TextBox>
                    </td>
                    <td>
                    &nbsp;</td>
                    </tr>
                    </table>
                    <table style="width: 100%;">
                    <tr>
                    <td style="width: 137px">
                        &nbsp;</td>
                    <td style="width: 129px">
                    &nbsp;</td>
                    <td>
                    &nbsp;
                    </td>
                    </tr>
                    <tr>
                    <td style="width: 137px">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha de Reprogramacion"></asp:Label>
                    </td>
                    <td style="width: 129px">
                    <uc2:jDatePick ID="dtpFechaProgramacion" runat="server" />
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    </tr>
                    <tr>
                    <td style="width: 137px">
                    <asp:Label ID="Label4" runat="server" Text="Proxima Fecha Realizacion"></asp:Label>
                    </td>
                    <td style="width: 129px">
                    <uc2:jDatePick ID="DatePicker_Desde" name="desde" runat="server" CustomScript="customRange"  />
                    </td>
                    <td>
                    <uc2:jDatePick ID="DatePicker_Hasta" Name="hasta" runat="server"  CustomScript="customRange" />
                    </td>
                    </tr>
                    </table>
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 89px">
                                &nbsp;</td>
                            <td style="width: 85px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 89px">
                               <asp:Button ID="btnAceptar_S0" runat="server" OnClick="btnAceptar_S_Click" 
                            Text="Aceptar" /></td>
                            <td style="width: 85px">
                                 <asp:Button ID="btnCancelar" runat="server" 
                            Text="Cancelar" onclick="btnCancelar_Click" /></td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 89px">
                                &nbsp;</td>
                            <td style="width: 85px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
 

</asp:Content>
