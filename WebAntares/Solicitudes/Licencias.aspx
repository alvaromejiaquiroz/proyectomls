<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Licencias.aspx.cs" Inherits="Solicitudes_Licencias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc2" %>

<%@ Register src="../Controles/Licencias.ascx" tagname="Licencias" tagprefix="uc1" %>

<asp:Content ID="cLicencias" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:Panel ID="pnlHeaderLicencias" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="4">
                Solicitud de Licencia
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        </table>
        <br />
        
        <table>
        <tr>
            <td style="padding-left: 20px">
                Empleado</td>
            <td colspan="3">
            <asp:DropDownList ID="cmbEmpleado" runat="server" CssClass="text_custom" 
                
                    onselectedindexchanged="cmbEmpleado_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvCompareEmpleado" runat="server" 
                ControlToValidate="cmbEmpleado" 
                Operator="GreaterThan" ValueToCompare="0" Display="None"
                ErrorMessage="Debe seleccionar un Empleado"
                ValidationGroup="solicitud" ></asp:CompareValidator>
             </td>
                 
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Tipo
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cmbTipoLicencia" runat="server" CssClass="text_custom">
                </asp:DropDownList>
                 <asp:CompareValidator ID="cvCompareTipoLicencia" runat="server" 
                ControlToValidate="cmbTipoLicencia" 
                Operator="GreaterThan" ValueToCompare="0" Display="None"
                ErrorMessage="Debe determinar el tipo de licencia"
                ValidationGroup="solicitud" ></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px" valign="top">
                Motivo
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDescripcion" runat="server" Height="64px" Width="368px" TextMode="MultiLine"
                    CssClass="text_custom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                    ControlToValidate="txtDescripcion" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr >
            <td colspan="2" align="center">
            <asp:Button ID="btnIniciarCarga" runat="server" Text="Aceptar" 
            ValidationGroup="solicitud" CssClass="button_custom" 
            onclick="btnIniciarCarga_Click" />
        
            </td>
            <td>    
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"  CssClass="button_custom" 
            onclick="btnCancelarCarga_Click" />
        
            </td>
        </tr>
        </table>
                <asp:ValidationSummary  ID="vsSolicitud" runat="server" DisplayMode="BulletList" ValidationGroup="solicitud" />        
        </asp:Panel>
        
    <br />
    
    <asp:Panel runat="server" ID = "pnlContenido" Visible="false">
        <table>
        <tr>
            <td style="padding-left: 20px">
                Fecha
            </td>
            <td>
                <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                <asp:ImageButton ID="imgInicio" runat="server" CausesValidation="false" ImageUrl="~/Images/calendario.gif" />
                <cc1:CalendarExtender ID="ceInicio" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgInicio" CssClass="cal_Theme1" 
                    TargetControlID="txtInicio">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvInicio" runat="server" ErrorMessage="Debe ingresar la Fecha."
                    ControlToValidate="txtInicio" Display="None" ValidationGroup="licencia"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvInicio" runat="server" ErrorMessage="La fecha  no es válida."
                    ControlToValidate="txtInicio" Display="None" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="licencia"></asp:CompareValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding-left: 20px">
                Duración
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDuracion" runat="server" CssClass="text_custom" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDuracion" runat="server" ErrorMessage="Debe ingresar la cantidad de horas de la licencia."
                    ControlToValidate="txtDuracion" Display="None" ValidationGroup="licencia"></asp:RequiredFieldValidator>
                 
                <asp:CompareValidator ID="cvDuracion" runat="server" ErrorMessage="La duración no es válida."
                    Display="None" ValidationGroup="licencia" ControlToValidate="txtDuracion" Operator="DataTypeCheck"
                    Type="Currency"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="La duración no es válida."
                    Display="None" ValidationGroup="licencia" ControlToValidate="txtDuracion" Operator="GreaterThan" ValueToCompare="0"
                    Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td align="left" colspan="4">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAceptar_Click"
                    ValidationGroup="licencia" CssClass="button_custom" />
                <asp:Button ID="btnFinalizar" runat="server" CssClass="button_custom" 
                    OnClick="btnFinalizar_Click" Text="Finalizar" 
                     />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="text_custom">
                    <asp:ValidationSummary ID="vsLicencia" runat="server" ValidationGroup="licencia" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
            </td>
        </tr>
    </table>
    
     <asp:GridView ID="gvLicencias" runat="server"
    AutoGenerateColumns="false" 
    AllowPaging="true" 
    onpageindexchanging="gvLicencias_PageIndexChanging" PageSize="5"
    OnRowDeleting="gvLicencias_RowDeleting"
    Width="50%" >
    <Columns>
    <asp:BoundField HeaderText="id" DataField="id" Visible ="false"/>
    <asp:BoundField HeaderText="Fecha" DataField="FechaInicio" DataFormatString="{0:dd/MM/yyyy}" Visible ="true"/>
    <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" Visible ="true"/>
    <asp:BoundField HeaderText="Horas" DataField="Duracion" Visible ="true"/>
    <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="True">
        <ItemTemplate>
            <asp:ImageButton ID="imgEliminar" runat="server" 
            CausesValidation="False" 
            CommandName="Delete"
            ImageUrl="~/Images/basuravacio.gif" Text="Eliminar" ToolTip="Eliminar" />
        </ItemTemplate>
    </asp:TemplateField>    
    </Columns>
    </asp:GridView>
    </asp:Panel>
   
    <uc1:Licencias ID="ucLicencias" runat="server" Visible="false" Imprimible="false" />
</asp:Content>

