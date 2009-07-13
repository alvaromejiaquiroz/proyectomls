<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Licencias.aspx.cs" Inherits="Solicitudes_Licencias" Title="Untitled Page" %>

<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc1" %>
<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                <uc1:SolDetalle ID="SolDetalle1" runat="server" />
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
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:434px;">
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label1" runat="server" Text="Tipo Licencia"></asp:Label>
            </td>
            <td style="width: 112px">
                <asp:DropDownList ID="cmbTipoLicencia" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width: 60px">
                &nbsp;</td>
            <td style="width: 258px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label2" runat="server" Text="Fecha Inicio"></asp:Label>
            </td>
            <td style="width: 112px">
                <uc2:jDatePick ID="dtpInicio"  name="desde" runat="server"  CustomScript="customRange" />
            </td>
            <td style="width: 60px">
                <asp:Label ID="Label3" runat="server" Text="Fecha Fin"></asp:Label>
            </td>
            <td style="width: 258px">
                <uc2:jDatePick ID="dtpFin" name="hasta"  runat="server"  CustomScript="customRange" />
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label4" runat="server" Text="Duracion"></asp:Label>
            </td>
            <td style="width: 112px">
                <asp:TextBox ID="txtDuracion" runat="server" CssClass="numeric"></asp:TextBox>
            </td>
            <td style="width: 60px">
                &nbsp;</td>
            <td style="width: 258px">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td valign="top">
                <asp:Label ID="Label5" runat="server" Text="Descripcion"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Height="126px" Width="395px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                    onclick="btnAceptar_Click" style="text-decoration: underline" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </td>
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
        <script>
        //funcion adicional para controlar el rango de fechas

        function customRange(input) {
            return { minDate: (input.id == hasta ? $("#" + desde).datepicker("getDate") : null),
                maxDate: (input.id == desde ? $("#" + hasta).datepicker("getDate") : null)
            };
        } 


    </script>

</asp:Content>

