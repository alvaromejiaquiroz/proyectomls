<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Solicitud_Horas.ascx.cs" Inherits="Controles_Gastos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdateHoras" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlHorasPersonal" runat="server" Visible="false" >
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
                                <asp:label id="lblDes" runat="server"  Text ="Descripción" Visible="true"> </asp:label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHorasPersonalDescripcion" runat="server" Width="250px" Height="64px"
                                    TextMode="MultiLine" CssClass="text_custom"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="padding:10px">
                                <asp:Button ID="btnHorasPersonalGuardar" runat="server" OnClick="btnHorasPersonalGuardar_Click"
                                    Text="Guardar" ValidationGroup="horasPersonal" CssClass="button_custom" />
                                <asp:Button ID="btnHorasPersonalCerrar" runat="server" Text="Cerrar" CausesValidation="false"
                                    CssClass="button_custom" onclick="btnHorasPersonalCerrar_Click" />
                            <asp:RangeValidator ID="rngHorasPersonalValidator"  runat="server" 
                            Type="Date"
                            
                            ControlToValidate="txtHorasPersonalDia"
                            minimumvalue="01/01/2000" maximumvalue="31/12/2099"
                             ErrorMessage="Solo se pueden cargar Horas dentro de las Fechas en las que la solicitud fue creada"
                             Display="None" ValidationGroup ="horasPersonal"> 
                             </asp:RangeValidator>
                                  
                                
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
    </ContentTemplate>
</asp:UpdatePanel>