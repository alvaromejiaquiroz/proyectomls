<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonaHoras.aspx.cs" Inherits="Solicitudes_PersonaHoras" EnableEventValidation="true" %>

<%@ Register src="../Controles/Tiempo.ascx" tagname="Tiempo" tagprefix="uc1" %>

<%@ Register src="../Controles/jDatePick.ascx" tagname="jDatePick" tagprefix="uc2" %>
<link href="../App_Themes/Antares/cupertino/ui.datepicker.css" rel="stylesheet" type="text/css" />
<script src="../Libs/jquery.pack.js" type="text/javascript"></script>

<script src="../Libs/ui.core.packed.js" type="text/javascript"></script>
<script src="../Libs/ui.datepicker.packed.js" type="text/javascript"></script>
<script src="../Libs/ui.datepicker-es.js" type="text/javascript"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:White;">
        <table>
        <tr>
        <td>Persona Seleccionada:</td>
        <td>
            <asp:TextBox ID="txtPersona" runat="server" 
                style="margin-top: 10px; margin-bottom: 0px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
        <td class="style1">Dia</td>
        <td class="style1">
            <uc2:jDatePick ID="jDatePick1" runat="server" />
            
            </td>
        </tr>
        
        <tr>
        <td>Horas</td>
        <td>
            <uc1:Tiempo ID="Tiempo1" runat="server" />
            </td>
        </tr>
        
        <tr>
        <td>Descripcion</td>
        <td>
            <asp:TextBox ID="txtDescripcion" runat="server" 
                TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                onclick="cmdGuardar_Click" />
            <asp:Button ID="cmdSalir" runat="server" Text="Salir"  OnClientClick="self.close();" CausesValidation="false"/>
            </td>
        </tr>
        
        <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Label ID="lblMSG" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
        <tr>
        <td colspan="2">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                onrowdeleted="GridView1_RowDeleted" onrowdeleting="GridView1_RowDeleting" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" >
            <Columns>
                        <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" Visible="true" />
                        <asp:BoundField DataField="Horas" HeaderText="Horas" Visible="true" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Detalle" Visible="true" />
                         <asp:TemplateField>
                                    <ItemTemplate>
                                        <div ID="noprint2">
                                            <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="False" 
                                                CommandName="Delete" ImageUrl="~/Images/delete.gif" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
            </asp:GridView>
            </td>
        </tr>
        
        </table>
        </div>
    </form>
</body>
</html>
