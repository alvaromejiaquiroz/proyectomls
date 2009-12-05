<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Capacitacion.ascx.cs" Inherits="Controles_Capacitacion" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            SOLICITUD DE CAPACITACION
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 20px">
        </td>
        <td rowspan="3" align="right" valign="top" style="padding-right:10px">
            <asp:Image ID="imgAntares" runat="server" AlternateText="Antares" ToolTip="Antares" ImageUrl="~/images/LogoAntaresThumbnail.png" />
        </td>
    </tr>
        <tr>
        <td style="height:20px">
            
        </td>
        <td>
        
        </td>
    </tr>

    <tr>
        <td style="padding-left: 20px;width:150px">
            Número
        </td>
        <td>
            <asp:label ID="litNumero" runat="server" Font-Bold="true" ></asp:label>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Título
        </td>
        <td colspan="2">
            <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Nivel
        </td>
        <td colspan="2">
            <asp:Literal ID="litNivel" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha de inicio
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaInicio" runat="server" ></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Fecha de fin
        </td>
        <td colspan="2">
            <asp:Literal ID="litFechaFin" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Duración
        </td>
        <td colspan="2">
            <asp:Literal ID="litDuracion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Área de estudios
        </td>
        <td colspan="2">
            <asp:Literal ID="litAreaEstudios" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Instructor
        </td>
        <td colspan="2">
            <asp:Literal ID="litInstructor" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Entidad educativa
        </td>
        <td colspan="2">
            <asp:Literal ID="litEntidadEducativa" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px">
            Puntuación de examen
        </td>
        <td colspan="2">
            <asp:Literal ID="litPuntuacionExamen" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
    <td style="padding-top:20px;">
        <br />
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px" valign="top">
            Detalle
        </td>
    
        <td colspan="2" style="padding-bottom:20px">
            <asp:GridView ID="gvHorasPersonal" runat="server" AutoGenerateColumns="False" AllowPaging="true"   Width="300px">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="left" dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="left" DataFormatString="{0:0.0}" />
                </Columns>
                </asp:GridView>
        </td>
        </tr>
        
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" 
                CssClass="button_custom" onclick="btnFinalizar_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 20px">
    </tr>
</table>
<asp:Panel ID="pnlImprimir" runat="server">
    <div style="margin: 20px;text-align: center">
        Sistema de gestión de tareas - V 1.0 - ANTARES COM S.A
    </div>
    <div style="margin: 20px;text-align: center">
        <input type="button" class="button_custom" onclick="window.print()" value="Imprimir" />
    </div>
</asp:Panel>