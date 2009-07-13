<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Capacitacion.aspx.cs" Inherits="Solicitudes_Capacitacion" Title="Untitled Page" %>

<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc1" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <table style="width:400px;">
        <tr>
            <td>
                <uc1:SolDetalle ID="SolDetalle1" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <table style="width: 100%;">
        <tr>
            <td class="style4" style="width: 105px">
                Nivel</td>
            <td style="width: 231px">
            
                <asp:DropDownList ID="cmbNivelCapacitacion" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Fecha Inicio</td>
            <td style="width: 231px">
            
                <uc2:jDatePick ID="dtpDesde" name="desde" runat="server" CustomScript="customRange"  />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Fecha Fin</td>
            <td style="width: 231px">
                
                <uc2:jDatePick ID="dtpHasta" name = "hasta" runat="server" CustomScript="customRange"  />
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Duracion</td>
            <td style="width: 231px">
                <asp:TextBox ID="txtHoras" runat="server" CssClass="numeric" 
                    Width="69px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Area de Estudios</td>
            <td style="width: 231px">
                <asp:TextBox ID="txtAreaEstudios" runat="server" Width="224px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Instructor</td>
            <td style="width: 231px">
                <asp:TextBox ID="txtInstructor" runat="server" Width="223px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Entidad Educativa</td>
            <td style="width: 231px">
                <asp:TextBox ID="txtEntidad" runat="server" Width="221px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4" style="width: 105px">
                Puntuacion Examen</td>
            <td style="width: 231px">
                <asp:TextBox ID="txtPuntuacionExamen" runat="server" Width="83px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
     
    <table>
        <tr>
            <td style="width: 74px" >
                <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
                    Text="Aceptar" />
            </td>
            <td >
                <asp:Button ID="btnCancelar" runat="server" onclick="btnCancelar_Click" 
                     Text="Cancelar" />
            </td>
            <td >
                </td>
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

