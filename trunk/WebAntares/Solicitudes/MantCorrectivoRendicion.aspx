<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantCorrectivoRendicion.aspx.cs" Inherits="Solicitudes_MantPreventivo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc2" %>
<%@ Register Src="../Controles/Adjuntos.ascx" TagName="Adjuntos" TagPrefix="uc4" %>
<%@ Register src="../Controles/Solicitud_Gastos.ascx" tagname="Solicitud_Gastos" tagprefix="uc3" %>

<%@ Register src="../Controles/MantenimientoCorrectivoRendicion.ascx" tagname="MantenimientoCorrectivoRendicion" tagprefix="uc1" %>

<asp:Content ID="cMantenimientoCorrectivoRendicion" ContentPlaceHolderID="PageContainer" runat="Server">
<asp:Panel ID="pnlMantenimientoCorrectivo" runat="server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                REPORTE DE MANTENIMIENTO CORRECTIVO
            </td>
        </tr>
        <tr>
            <td>
                <uc2:SolDetalle ID="ucSolDetalle" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="tcMantenimientoPreventivoRendicion" runat="server" Height="450px">
                    <cc1:TabPanel ID="tpFalla" HeaderText="Datos Principales" runat="server">
                        <HeaderTemplate>
                            Datos Principales
                        </HeaderTemplate>
                        <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat ="server">
                        <ContentTemplate>
                        
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Reportó falla
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReportoFalla" runat="server" CssClass="text_custom" 
                                            Width="250px" OnChange="CopiaValor()"  ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReportoFalla" runat="server" ErrorMessage="Debe ingresar quien reportó la falla."
                                            ControlToValidate="txtReportoFalla" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                                     <tr>
                                    <td style="padding-left: 20px">
                                        Fecha de Reporte
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaReporte" runat="server" MaxLength="10" Width="80px" CssClass="text_custom"></asp:TextBox>
                                        <asp:ImageButton ID="imgFechaReporte" runat="server" CausesValidation="False" 
                                            ImageUrl="~/Images/calendario.gif" />
                                        <cc1:CalendarExtender ID="ceFechaReporte" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgFechaReporte" CssClass="cal_Theme1" 
                                            TargetControlID="txtFechaReporte" Enabled="True">
                                        </cc1:CalendarExtender>
                                        <asp:DropDownList ID="ddlHoraReporte" runat="server" CssClass="text_custom">
                                            <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMinutosReporte" runat="server" CssClass="text_custom">
                                            <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                            <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                            <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                            <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                            <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                            <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                            <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                            <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                            <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                            <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                            <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                            <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                            <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                            <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                            <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                            <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                            <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                            <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                            <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                            <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                            <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                            <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                            <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                            <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                            <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                            <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFechaReporte" runat="server" ErrorMessage="Debe ingresar la fecha de reporte de la falla."
                                            ControlToValidate="txtFechaReporte" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvFechaReporte" runat="server" ErrorMessage="La fecha de reporte de la falla no es válida."
                                            ControlToValidate="txtFechaReporte" Display="None" Operator="DataTypeCheck" Type="Date"
                                            ValidationGroup="solicitud"></asp:CompareValidator>
                        
                                    </td>
                                </tr>
           
                                <tr>
                                    <td style="padding-left: 20px">
                                        Síntomas
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFalla" runat="server" Height="64px" Width="470px" TextMode="MultiLine"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFalla" runat="server" ErrorMessage="Debe ingresar algun Sintoma."
                                            ControlToValidate="txtFalla" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
                                                           <tr>
                                    <td style="padding-left: 20px">
                                        Causa probable
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCausa" runat="server" Height="64px" Width="470px" TextMode="MultiLine"
                                            CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCausa" runat="server" ErrorMessage="Debe ingresar el Sintoma probable de la falla."
                                            ControlToValidate="txtCausa" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        </ContentTemplate>
                    </cc1:TabPanel>
                    
                    <cc1:TabPanel ID="tpDescripcion" HeaderText="Detalles" runat="server">
                            <HeaderTemplate>
                                Detalles
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <table class="text_custom" width="600px">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Descripción del Trabajo Realizado</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDescripcionTrabajo" runat="server" TextMode="MultiLine" Height="160px" Width="100%" CausesValidation="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="La descripción del trabajo realizado es obligatoria"
                                                ControlToValidate="txtDescripcionTrabajo" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>

                                        </td>
                                        <td></td>
                                  
                                    </tr>
                                    </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </cc1:TabPanel>
                     
                    <cc1:TabPanel ID="tpConfirmacionCliente" runat="server">
                    <HeaderTemplate>
                                Conforme Cliente
                    </HeaderTemplate>
                            <ContentTemplate>
                                    <table class="text_custom" width="600px">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-left: 20px;font-weight:bold">Nombre y Apellido de quien dio Conformidad de la Reparación por parte del Cliente</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <asp:TextBox ID="txtContactoConformidad" Width="500px" runat="server" CausesValidation="true" ></asp:TextBox>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe especificar qué persona dio el conforme sobre el Mantenimiento Correctivo Realizado"
                                                ControlToValidate="txtContactoConformidad" Display="None" ValidationGroup="solicitud"></asp:RequiredFieldValidator>

                                        </td>
                                        <td></td>
                                  
                                    </tr>
                                    </table>
                                
                            </ContentTemplate>
                        </cc1:TabPanel>
           
                    <cc1:TabPanel ID="tpSitio" HeaderText="Sitio" runat="server">
                        <ContentTemplate>
                        <table class="text_custom" width="600px">
                         <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                        <tr>
                          <td style="padding-left: 20px">
                          Sitio de Referencia
                          </td>
                          <td style="padding-left: 20px">
                          <br />    
                          </td>
                          <td>
                              <asp:DropDownList ID="cboSitios" runat="server" CssClass="text_custom">
                              </asp:DropDownList>
                              
                          </td>
                      </tr>
                      </table>
                        
                        </ContentTemplate>
                        </cc1:TabPanel>
 
                    <cc1:TabPanel ID="tpPersonal" HeaderText="Personal" runat="server">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="upPersonas" runat="server">
                                    <ContentTemplate>
                                        <table width="600px" class="text_custom">
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 20px">
                                                    Responsable
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbResponsable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbResponsable_SelectedIndexChanged"
                                                        CssClass="text_custom">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 20px">
                                                    <asp:Literal ID="litPersonal" runat="server" Text="Personal" Visible="false"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lstEmpleadosSolicitud" runat="server" SelectionMode="Multiple" Height="150px"
                                                        Visible="false" CssClass="text_custom"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btnAsignaEmpleadoSolicitud" runat="server" Text="Asignar empleado"
                                                        OnClick="btnAsignaEmpleadoSolicitud_Click" Visible="false" CssClass="button_custom" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvSolicitudPersonas" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="true" onpageindexchanging="gvSolicitudPersonas_PageIndexChanging" PageSize="5"
                                                        OnRowDeleting="gvPersonas_RowDeleting" OnRowEditing="gvPersonas_RowEditing" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                            <asp:BoundField DataField="Empleado" HeaderText="Empleado" HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:CheckBoxField DataField="Responsable" HeaderText="Responsable" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center" />
                                                            <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                                                ShowEditButton="True" />
                                                            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                        ImageUrl="~/Images/basuravacio.gif" Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlHorasPersonal" runat="server" Style="display: none" CssClass="modalPopup">
                                            <table width="700px" class="text_custom" style="border-style: solid; border-color: #000000; border-width: 1px">
                                                <tr class="header_custom">
                                                    <td align="center" colspan="2">
                                                        Reporte de horas de personal
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Persona
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litHorasPersonalPersona" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hfHorasPersonalPersona" runat="server" />
                                                        <asp:HiddenField ID="hfHorasPersonalSolicitud" runat="server" />
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
                                                            PopupButtonID="imgHorasPersonalDia" TargetControlID="txtHorasPersonalDia">
                                                        </cc1:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvHorasPersonalDia" runat="server" ErrorMessage="Debe ingresar el día."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" ValidationGroup="horasPersonal"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasPersonalDia" runat="server" ErrorMessage="El día no es válido."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" Operator="DataTypeCheck"
                                                            Type="Date" ValidationGroup="horasPersonal"></asp:CompareValidator>
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
                                                    <td style="padding-left: 20px">
                                                        Descripción
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasPersonalDescripcion" runat="server" Width="250px" Height="64px"
                                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasPersonalDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                                                            Display="None" ValidationGroup="horasPersonal" ControlToValidate="txtHorasPersonalDescripcion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding:10px">
                                                        <asp:Button ID="btnHorasPersonalGuardar" runat="server" OnClick="btnHorasPersonalGuardar_Click"
                                                            Text="Guardar" ValidationGroup="horasPersonal" CssClass="button_custom" />
                                                        <asp:Button ID="btnHorasPersonalCerrar" runat="server" Text="Cerrar" CausesValidation="false"
                                                            CssClass="button_custom" />
                                                        <asp:CompareValidator ID="cvHorasPersonal" runat="server" ErrorMessage="La fecha debe ser mayor o igual a la fecha de inicio de la solicitud."
                                                            ControlToValidate="txtHorasPersonalDia" Display="None" Operator="GreaterThanEqual"
                                                            Type="Date" ValidationGroup="horasPersonal" ValueToCompare="01/01/2000"></asp:CompareValidator>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvHorasPersonal" runat="server" AutoGenerateColumns="False" AllowPaging="true"  
                                                            onrowdeleting="gvHorasPersonal_RowDeleting" Width="640px"
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
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:ValidationSummary ID="vsHorasPersonal" runat="server" ValidationGroup="horasPersonal" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnHorasPersonalPopUp" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="mpeHorasPersonal" runat="server" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnHorasPersonalCerrar" PopupControlID="pnlHorasPersonal"
                                            TargetControlID="btnHorasPersonalPopUp">
                                        </cc1:ModalPopupExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </cc1:TabPanel>
     
                    <cc1:TabPanel ID="tpVehiculos" HeaderText="Vehículos" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upVehiculos" runat="server">
                                <ContentTemplate>
                                    <table width="600px" class="text_custom">
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="padding-left: 20px">
                                                Vehículos
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstVehiculos" runat="server" SelectionMode="Multiple" Height="150px"
                                                    CssClass="text_custom"></asp:ListBox>
                                                <asp:CustomValidator ID="cvVehiculos" runat="server" ErrorMessage="Debe seleccionar al menos un vehículo."
                                                    Display="None" ValidationGroup="vehiculos" OnServerValidate="cvVehiculos_ServerValidate"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnAsignaVehiculoSolicitud" runat="server" Text="Asignar vehículo"
                                                    OnClick="btnAsignaVehiculoSolicitud_Click" ValidationGroup="vehiculos" CssClass="button_custom" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="height: 20px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvSolicitudVehiculos" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="true" onpageindexchanging="gvSolicitudVehiculos_PageIndexChanging" PageSize="5"
                                                    OnRowDeleting="gvSolicitudVehiculos_RowDeleting" OnRowEditing="gvSolicitudVehiculos_RowEditing" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="Solicitud" HeaderText="Solicitud" Visible="False" />
                                                        <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:CommandField ButtonType="Image" HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                                                                ShowEditButton="True" />
                                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                    ImageUrl="~/Images/basuravacio.gif" Text="Delete" ToolTip="Eliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlHorasVehiculos" runat="server" Style="display: none" CssClass="modalPopup">
                                            <table width="700px" class="text_custom" style="border-style: solid; border-color: #000000; border-width: 1px">
                                                <tr class="header_custom">
                                                    <td align="center" colspan="2">
                                                        Reporte de horas de vehículos
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Vehículo
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litHorasVehiculosVehiculo" runat="server"></asp:Literal>
                                                        <asp:HiddenField ID="hfHorasVehiculosVehiculo" runat="server" />
                                                        <asp:HiddenField ID="hfHorasVehiculosSolicitud" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Día
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosDia" runat="server" MaxLength="10" Width="80px"
                                                            CssClass="text_custom"></asp:TextBox>
                                                        <asp:ImageButton ID="imgHorasVehiculosDia" runat="server" CausesValidation="false"
                                                            ImageUrl="~/Images/calendario.gif" />
                                                        <cc1:CalendarExtender ID="ceHorasVehiculosDia" runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1" 
                                                            PopupButtonID="imgHorasVehiculosDia" TargetControlID="txtHorasVehiculosDia">
                                                        </cc1:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosDia" runat="server" ErrorMessage="Debe ingresar el día."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" ValidationGroup="horasVehiculos"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasVehiculosDia" runat="server" ErrorMessage="El día no es válido."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" Operator="DataTypeCheck"
                                                            Type="Date" ValidationGroup="horasVehiculos"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Horas
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHorasVehiculosHoras" runat="server" CssClass="text_custom">
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
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Kilómetros
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosKilometros" runat="server" MaxLength="10" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosKilometros" runat="server" ErrorMessage="Debe ingresar los kilómetros."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosKilometros"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvHorasVehiculosKilometros" runat="server" ErrorMessage="Los kilómetros no son válidos."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosKilometros"
                                                            Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 20px">
                                                        Descripción
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtHorasVehiculosDescripcion" runat="server" Width="250px" Height="64px"
                                                            TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvHorasVehiculosDescripcion" runat="server" ErrorMessage="Debe ingresar la descripción."
                                                            Display="None" ValidationGroup="horasVehiculos" ControlToValidate="txtHorasVehiculosDescripcion"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding:10px">
                                                        <asp:Button ID="btnHorasVehiculosGuardar" runat="server" OnClick="btnHorasVehiculosGuardar_Click"
                                                            Text="Guardar" ValidationGroup="horasVehiculos" CssClass="button_custom" />
                                                        <asp:Button ID="btnHorasVehiculosCerrar" runat="server" Text="Cerrar" CausesValidation="false"
                                                            CssClass="button_custom" />
                                                        <asp:CompareValidator ID="cvHorasVehiculos" runat="server" ErrorMessage="La fecha debe ser mayor o igual a la fecha de inicio de la solicitud."
                                                            ControlToValidate="txtHorasVehiculosDia" Display="None" Operator="GreaterThanEqual"
                                                            Type="Date" ValidationGroup="horasVehiculos" ValueToCompare="01/01/2000"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView ID="gvHorasVehiculos" runat="server" AutoGenerateColumns="False" 
                                                        AllowPaging="true" onpageindexchanging="gvHorasVehiculos_PageIndexChanging" PageSize="5"
                                                            onrowdeleting="gvHorasVehiculos_RowDeleting" Width="640px">
                                                        <Columns>
                                                            <asp:BoundField DataField="Id" HeaderText="id" Visible="false" />
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="Kilometros" HeaderText="Kilómetros" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Horas" HeaderText="Duración" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
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
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:ValidationSummary ID="vsHorasVehiculos" runat="server" ValidationGroup="horasVehiculos" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Button ID="btnHorasVehiculosPopUp" runat="server" Style="display: none" />
                                        <cc1:ModalPopupExtender ID="mpeHorasVehiculos" runat="server" BackgroundCssClass="modalBackground"
                                            CancelControlID="btnHorasVehiculosCerrar" PopupControlID="pnlHorasVehiculos"
                                            TargetControlID="btnHorasVehiculosPopUp">
                                        </cc1:ModalPopupExtender>
                                    <div class="text_custom">
                                        <asp:ValidationSummary ID="vsVehiculos" runat="server" ValidationGroup="vehiculos" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="tpComplemento" HeaderText="Datos Cliente" runat="server">
                        <ContentTemplate>
                            <table class="text_custom" width="600px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Contrato
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbClientes" runat="server" CssClass="text_custom">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Contacto de cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactoCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContactoCliente" runat="server" ErrorMessage="Debe ingresar el contacto de cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtContactoCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        N° orden del Cliente
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNroOrdenCliente" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNroOrdenCliente" runat="server" ErrorMessage="Debe ingresar el nro. de orden del cliente."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtNroOrdenCliente"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        Teléfono de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefonoContacto" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTelefonoContacto" runat="server" ErrorMessage="Debe ingresar el teléfono de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtTelefonoContacto"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        E-mail de contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMailContacto" runat="server" Width="210px" ValidationGroup="solicitud" CssClass="text_custom"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMailContacto" runat="server" ErrorMessage="Debe ingresar el e-mail de contacto."
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMailContacto"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMailContacto" runat="server" ErrorMessage="El e-mail de contacto no es válido"
                                            Display="None" ValidationGroup="solicitud" ControlToValidate="txtMailContacto"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    
                    <cc1:TabPanel ID="tpAdjuntos" HeaderText="Adjuntos" runat="server">
                        <ContentTemplate>
                            <div style="width: 600px">
                                <uc4:Adjuntos ID="ucAdjuntos" runat="server" TipoSol="MantenimientoPreventivo" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="tpPresupuesto" HeaderText="Presupuesto" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <table class="text_custom" width="400px">
                                <tr>
                                    <td colspan="2" style="height: 20px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                     <uc3:Solicitud_Gastos ID="ucSolicitudGastos" runat="server" />
                                    </td>
                                </tr>
                            </table>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="tpConfirmacion" HeaderText="Confirmación" runat="server">
                        <ContentTemplate>
                            <div style="width:100%;text-align:center;margin-top:30px">
                                <asp:Button ID="btnAceptarSolicitud" runat="server" OnClick="btnAceptarSolicitud_Click"
                                    Text="Confirmar Reporte" ValidationGroup="solicitud" CssClass="button_custom" />
                                <asp:CustomValidator ID="cvPersonalIngresoHoras" runat="server" ErrorMessage="" 
                                    Display="None" ValidationGroup="solicitud" OnServerValidate="cvPersonalIngresoHoras_ServerValidate"></asp:CustomValidator>
                                      <asp:CustomValidator ID="cvGastosEnSolicitud" runat="server" ErrorMessage="" Display="None" ValidationGroup="solicitud" OnServerValidate="cvGastosEnSolicitud_ServerValidate"></asp:CustomValidator>
                            </div>
                            <div class="text_custom">
                                <asp:BulletedList ID="blErrores" runat="server" ForeColor="Red" BulletStyle="NotSet">
                                </asp:BulletedList>
                                <asp:ValidationSummary ID="vsSolicitud" runat="server" ValidationGroup="solicitud" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <uc1:MantenimientoCorrectivoRendicion ID="ucMantenimientoCorrectivoRendicion" runat="server" Imprimible="false" Visible="false" />
</asp:Content>
