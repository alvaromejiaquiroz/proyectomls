<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TareasGenerales.ascx.cs" Inherits="Controles_TareasGenerales" %>

<table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px;text-align:left">
    <tr class="header_custom">
        <td align="center" colspan="3">
            TAREAS GENERALES
        </td>
    </tr>
    <tr>
        <td  style="height: 20px">
        </td>
        <td  align="right" valign="top" style="padding-right:10px;padding-bottom:20px">
            <asp:Image ID="imgAntares" runat="server" AlternateText="Antares" ToolTip="Antares" ImageUrl="~/images/LogoAntaresThumbnail.png" />
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;">
            Número
        </td>
        <td>
            <asp:Literal ID="litNumero" runat="server"></asp:Literal>
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
            Tipo
        </td>
        <td colspan="2">
            <asp:Literal ID="litTipo" runat="server"></asp:Literal>
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
            Descripción
        </td>
        <td colspan="2">
            <asp:Literal ID="litDescripcion" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="height:20px"></td>
        <td></td>
    </tr>
    
    <tr>
        <td style="padding-left:20px" valign="top">Horas</td>
        <td  colspan ="2" style="padding-bottom:20px;padding-left:20px">
                        <asp:GridView ID="gvHorasPersonal" runat="server" AutoGenerateColumns="False" AllowPaging="true"  Width="300px" >
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        </Columns>
                        </asp:GridView>
                </td>
                
    </tr>
                
    <tr>
        <td style="height:20px"></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" 
                CssClass="button_custom" onclick="btnFinalizar_Click" />
        </td>
    </tr>
    <tr>
        <td style="height: 20px">
    </tr>
</table>
<asp:Panel ID="pnlImprimir" runat="server">
    <div style="margin: 20px;text-align: center">
          <asp:Label ID="VersionSistema" runat="server"></asp:Label>
    </div>
    <div style="margin: 20px;text-align: center">
        <input type="button" class="button_custom" onclick="window.print()" value="Imprimir" />
    </div>
</asp:Panel>