<%@ Page Language="C#" MasterPageFile="~/site.master" 
AutoEventWireup="true" CodeFile="Capacitacion.aspx.cs" Inherits="Solicitudes_Capacitacion"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controles/SolDetalle.ascx" tagname="SolDetalle" tagprefix="uc2" %>
<%@ Register src="../Controles/Capacitacion.ascx" tagname="Capacitacion" tagprefix="uc1" %>

<asp:Content ID="cCapacitacion" ContentPlaceHolderID="PageContainer" Runat="Server">


    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center" colspan="2">
                SOLICITUD CAPACITACION
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr class="text_custom">
            <td>
            <cc1:TabContainer ID="tcSol" runat="server"   ActiveTabIndex="0"  >
                <cc1:TabPanel ID="tpTareas" HeaderText="Tareas" runat="server">
                    <HeaderTemplate>Tarea </HeaderTemplate>
                    <ContentTemplate> 
                    <table>
                        <tr>
            <td style="padding-left: 20px">
                Nivel
            </td>
            <td>
                <asp:DropDownList ID="cmbNivelCapacitacion" runat="server" CssClass="text_custom">
                </asp:DropDownList>
            </td>
        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                Área de estudios
                            </td>
                            <td>
                                <asp:TextBox ID="txtAreaEstudios" runat="server" CssClass="text_custom" 
                                    Width="220px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAreaEstudios" runat="server" 
                                    ControlToValidate="txtAreaEstudios" Display="None" 
                                    ErrorMessage="Debe ingresar el área de estudios." ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                Instructor
                            </td>
                            <td>
                                <asp:TextBox ID="txtInstructor" runat="server" Width="220px" 
                                    CssClass="text_custom"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvInstructor" runat="server" ErrorMessage="Debe ingresar el instructor."
                                    ControlToValidate="txtInstructor" Display="None" 
                                    ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                Entidad educativa
                            </td>
                            <td>
                                <asp:TextBox ID="txtEntidad" runat="server" Width="220px" 
                                    CssClass="text_custom"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEntidad" runat="server" ErrorMessage="Debe ingresar la entidad educativa."
                                    ControlToValidate="txtEntidad" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px">
                                Puntuación de examen
                            </td>
                            <td>
                                <asp:TextBox ID="txtPuntuacionExamen" runat="server" CssClass="text_custom"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPuntuacionExamen" runat="server" ErrorMessage="Debe ingresar la puntuación de examen."
                                    ControlToValidate="txtPuntuacionExamen" Display="None" 
                                    ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:20px"></td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    
                </cc1:TabPanel>
  
                <cc1:TabPanel ID="tpDetalle"  runat="server">
        <HeaderTemplate>Detalle</HeaderTemplate>
        <ContentTemplate>
                    <asp:UpdatePanel id="updHoras" runat ="server">
        <ContentTemplate>

           <table width="100%">
            <tr class="header_custom">
                <td align="center" colspan="2">
        Carga de Horas
        </td>
        </tr>
        </table>
           <table class="text_custom">  
                    <tr >
                                    <td colspan="2" style="height: 20px">         </td>
                    </tr>
                    <tr>
                       <td style="padding-left: 20px">
        Persona
        </td>
                       <td >
        <asp:Literal ID="litHorasPersonalPersona" runat="server" ></asp:Literal>

        </td>
        </tr>
                    <tr>
                       <td style="padding-left: 20px">
        Día
        </td>
                       <td>
        <asp:TextBox ID="txtHorasPersonalDia" runat="server" MaxLength="10" Width="80px"
        CssClass="text_custom"></asp:TextBox>
        <asp:ImageButton ID="imgHorasPersonalDia" runat="server" CausesValidation="false"
        ImageUrl="~/Images/calendario.gif" />
        <cc1:CalendarExtender ID="ceHorasPersonalDia" runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1" 
        PopupButtonID="imgHorasPersonalDia" TargetControlID="txtHorasPersonalDia" >
        </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="rfvHorasPersonalDia" runat="server" ErrorMessage="Debe ingresar el día."
        ControlToValidate="txtHorasPersonalDia" Display="None" ValidationGroup="horasPersonal"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvHorasPersonalDia" runat="server" ErrorMessage="El día no es válido."
        ControlToValidate="txtHorasPersonalDia" Display="None" Operator="DataTypeCheck"
        Type="Date" ValidationGroup="horasPersonal"></asp:CompareValidator>
        </td>
        </tr>
                    <tr>
                      <td style="padding-left: 20px" valign="top">
        Descripción
        </td>
                      <td>
        <asp:TextBox ID="txtHorasPersonalDescripcion" runat="server" Width="250px" Height="64px"
        TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
        
        <asp:RegularExpressionValidator  runat ="server" 
                    ID="RegularExpressionValidator1" ControlToValidate="txtHorasPersonalDescripcion"
                    ErrorMessage = "Ha ingresado algun caracter invalido"
                    ValidationGroup="intervencion" ValidationExpression="(?<=^|>)[^><]+?(?=<|$)" 
                    Display="None">
                </asp:RegularExpressionValidator>                   
        </td>
        </tr>
                    <tr>
                       <td style="padding-left: 20px">
        Horas
        </td>
                       <td>
        <asp:DropDownList ID="ddlHorasPersonalHoras" runat="server" CssClass="text_custom">
        <asp:ListItem Selected="True" Text="0,5" Value="0,5"></asp:ListItem>
        <asp:ListItem Text="1,0" Value="1"></asp:ListItem>
        <asp:ListItem Text="1,5" Value="1,5"></asp:ListItem>
        <asp:ListItem Text="2,0" Value="2"></asp:ListItem>
        <asp:ListItem Text="2,5" Value="2,5"></asp:ListItem>
        <asp:ListItem Text="3,0" Value="3"></asp:ListItem>
        <asp:ListItem Text="3,5" Value="3,5"></asp:ListItem>
        <asp:ListItem Text="4,0" Value="4"></asp:ListItem>
        <asp:ListItem Text="4,5" Value="4,5"></asp:ListItem>
        <asp:ListItem Text="5,0" Value="5"></asp:ListItem>
        <asp:ListItem Text="5,5" Value="5,5"></asp:ListItem>
        <asp:ListItem Text="6,0" Value="6"></asp:ListItem>
        <asp:ListItem Text="6,5" Value="6,5"></asp:ListItem>
        <asp:ListItem Text="7,0" Value="7"></asp:ListItem>
        <asp:ListItem Text="7,5" Value="7,5"></asp:ListItem>
        <asp:ListItem Text="8,0" Value="8"></asp:ListItem>
        <asp:ListItem Text="8,5" Value="8,5"></asp:ListItem>
        <asp:ListItem Text="9,0" Value="9"></asp:ListItem>
        <asp:ListItem Text="9,5" Value="9,5"></asp:ListItem>
        <asp:ListItem Text="10,0" Value="10"></asp:ListItem>
        <asp:ListItem Text="11,0" Value="11"></asp:ListItem>
        <asp:ListItem Text="12,0" Value="12"></asp:ListItem>
        <asp:ListItem Text="13,0" Value="13"></asp:ListItem>
        <asp:ListItem Text="14,0" Value="14"></asp:ListItem>
        </asp:DropDownList>
        <asp:CustomValidator ID="cvCheckHorasRestantes" runat="server" ErrorMessage="" ControlToValidate="ddlHorasPersonalHoras"
        Display="None" ValidationGroup="horasPersonal" OnServerValidate="cvCheckHorasRestantes_ServerValidate"></asp:CustomValidator>
        
       
         </td>
        </tr>

                    <tr>
                        <td align="center" colspan="2" style="padding-left:10px">
                <asp:Button ID="btnHorasPersonalGuardar" runat="server" OnClick="btnHorasPersonalGuardar_Click" 
                Text="Agregar" ValidationGroup="horasPersonal" CssClass="button_custom" />
                
                <asp:CompareValidator ID="cmpVFecha" runat="server" Type="Date"
                ControlToValidate = "txtHorasPersonalDia" Operator="LessThanEqual" ValueToCompare = "01/01/2000" 
                ErrorMessage="Solo se pueden cargar Horas en Fechas anteriores a hoy"
                ValidationGroup="horasPersonal" Display="None">
                </asp:CompareValidator>


        </td>
        </tr>
        <tr>
                       <td align="center" colspan="2">         &nbsp;</td>
        </tr>
        </table>
        <table>
        <tr>
                  <td class="text_custom">
                     <asp:CustomValidator ID="cvValidaFechas" runat="server"  ErrorMessage="" ControlToValidate="ddlHorasPersonalHoras"
                    Display="None" ValidationGroup="horasPersonal" OnServerValidate="cvValidaFechas_ServerValidate"></asp:CustomValidator>
          
                <asp:ValidationSummary ID="vsHorasPersonal" runat="server" ValidationGroup="horasPersonal" />
            </td>
         </tr>
        
        </table>
           <table>
                <tr>
                <td>
            <div style="padding-left:20px">
                <table width="100%" >
                    <tr>
                        <td style="padding-bottom:20px">
                        <asp:GridView ID="gvHorasPersonal" runat="server" AutoGenerateColumns="False" AllowPaging="true"  
                            onrowdeleting="gvHorasPersonal_RowDeleting"  Width="300px"
                            onpageindexchanging="gvHorasPersonal_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Horas" HeaderText="Horas" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" 
                                        CommandName="Delete" ImageUrl="~/Images/basuravacio.gif" Text="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                    </td>
                    </tr>
                </table>
        </div>
        </td>
        </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>

        </ContentTemplate>
     </cc1:TabPanel>
       
                <cc1:TabPanel ID="tpConfirmacion" runat ="server" >
                 <HeaderTemplate>Confirmación</HeaderTemplate>
                 <ContentTemplate>
                 
                  <asp:UpdatePanel id="upVisualizar" runat ="server">
                 <ContentTemplate>
                 <div style ="height:100%">
                 <table >
                 <tr>
                     <td>
                        <uc1:Capacitacion ID="ucCapacitacion" runat="server" Visible="false" Imprimible="false" />
                     </td>
                 </tr>
                 <tr>
                            <td align="center" colspan="2" style="height:100px">
                                <asp:Button ID="btnConfirmar" runat="server" CssClass="button_custom" 
                                    OnClick="btnConfirmarCarga_Click" Text="Confirmar"
                                    ValidationGroup="solicitud" Visible ="false"
                                    />
                                
                                
                                <asp:BUTTON ID="btnVisualizar" runat="server" CssClass="button_custom" 
                                     Text="Visualizar" ONclick="btnVisualizar_Click"
                                    CausesValidation="true" />
                        
                                
                                <asp:CustomValidator ID="cvValidaHorasCargadas" runat="server"  Display="None"
                                ValidationGroup="solicitud"  
                                OnServerValidate="cvValidaHorasCargadas_ServerValidate"></asp:CustomValidator>
       
                                    
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <div class="text_custom">
                                    <asp:ValidationSummary ID="vsSolicitud" runat="server"  ValidationGroup="solicitud" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 20px">
                            </td>
                        </tr>
                 </table>
                 </div>
                 </ContentTemplate>
                 
                 </asp:UpdatePanel>
                 
                 </ContentTemplate>
                 </cc1:TabPanel>
            </cc1:TabContainer>
            </td>
        </tr>
</table>
</asp:Content>

