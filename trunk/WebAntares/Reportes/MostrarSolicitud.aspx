<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="MostrarSolicitud.aspx.cs" Inherits="Reportes_MostrarSolicitud" Title="Untitled Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">

<br />
<br />
  
    <asp:UpdatePanel ID="pnlSolicitud" runat="server">
        <ContentTemplate>
        <hr />
            <table id="TablaCabecera" style="width:500px;">
                <tr>
                    <td>Solicitud:</td>
                    <td><asp:Label ID="lblNroSolicitud" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td>
                        Fecha de Creación:</td>
                    <td>
                        <asp:Label ID="lblFechaCreacion" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td><asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td>
                        Tipo de Solicitud:</td>
                    <td><asp:Label ID="lblTipoSolicitud" runat="server" Text=""></asp:Label></td>
                </tr>
                
                </table>
                <br />
                <hr />
               <table id="tabla_datos_generales_solicitud">
                   <tr>
                           <td> Descripcion: </td>
                           <td><asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                    <tr>
                           <td> Numero de Orden del Cliente: </td>
                           <td><asp:Label ID="lblNRO_OrdenCliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td> Cliente: </td>
                           <td><asp:Label ID="lblCliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td> Contacto en el Cliente: </td>
                           <td><asp:Label ID="lblContacto_Cliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td> Mail del Contacto: </td>
                           <td><asp:Label ID="lblContacto_Mail" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td> Telefono del Contacto: </td>
                           <td><asp:Label ID="lblContacto_Telefono" runat="server" Text=""></asp:Label> </td>
                   </tr> 
               </table>
               <br />
            
        </ContentTemplate>
       
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlPreventiva" runat="server" Visible="false">
        <ContentTemplate>
            <table id="tabla_Preventiva" style="width:100%;">
                <tr>
                    <td>Sitio:
                        &nbsp;</td>
                    <td><asp:Label ID="lblPvSitio" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Fecha Inicio:
                        &nbsp;</td>
                    <td><asp:Label ID="lblPvFechaInicio" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Fecha Fin:
                        &nbsp;</td>
                    <td><asp:Label ID="lblPvFechaFin" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                
            </table>
        </ContentTemplate>
       
    </asp:UpdatePanel>
   
    <asp:UpdatePanel ID="pnlCorrectiva" runat="server" Visible="false">
     <ContentTemplate>
        <table id="tabla_Preventiva">
            <tr>
                    <td>Falla Reportada:</td>
                    <td><asp:Label ID="lblCorrectiva_Falla_Reportada" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td>Plazo de Atención:</td>
                    <td><asp:Label ID="lblCorrectiva_Plazo_Atencion" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td>Fecha de Notificacion por Parte del Cliente:</td>
                    <td><asp:Label ID="lblCorrectiva_FechaNotificacionCliente" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td>Causa Probable:</td>
                    <td><asp:Label ID="lblCorrectiva_CausaProbable" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td>Persona que Reporto la Falla:</td>
                    <td><asp:Label ID="lblCorrectiva_Persona_ReportoFalla" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="pnlObra" runat="server" Visible="false">
        <ContentTemplate>
        <table id="tabla_obra">
            <tr>
                    <td>Descripcion de Tareas:
                        &nbsp;</td>
                    <td><asp:Label ID="lblObra_Desc_Tareas" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                    <tr>
                    <td>Fecha de Inicio:
                        &nbsp;</td>
                    <td><asp:Label ID="lblObra_FechaInicio" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                    <tr>
                    <td>Fecha Fin:
                        &nbsp;</td>
                    <td><asp:Label ID="lblObra_FechaFin" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Requisitos de Aprovacion:
                        &nbsp;</td>
                    <td><asp:Label ID="lblObra_Req_Aprovacion" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>Requisitos de Ingreso:
                        &nbsp;</td>
                    <td><asp:Label ID="lblObra_Req_Ingreso" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlComun" runat="server" Visible="false">
        <ContentTemplate>
        <table id="tabla_Comun">
            <tr>
                    <td>Presupuesto:
                        &nbsp;</td>
                    <td><asp:Label ID="lblPresupuesto" runat="server" Text=""></asp:Label>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlPersonalAsignado" runat="server" Visible="false">
        <ContentTemplate>
            <asp:GridView ID="gvPersonal" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                    <asp:BoundField DataField="Responsable" HeaderText="Responsable" /> 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlVehiculosAsignados" runat="server" Visible="false">
        <ContentTemplate>
            <asp:GridView ID="gvVehiculos" runat="server" AutoGenerateColumns="false">
            <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" Visible ="false" />
                    <asp:BoundField DataField="Id_solicitud" HeaderText="Id_Solicitud"  Visible ="false" />
                    <asp:BoundField DataField="Id_Vehiculo" HeaderText="Id_Vehiculo" Visible ="false" /> 
                    <asp:BoundField DataField="Vehiculo" HeaderText="Vehiculo" /> 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlSolicitud_Preventiva_Tareas_A_Realizar" runat="server" Visible="false">
    <ContentTemplate>
            <asp:GridView ID="gvSolicitud_Preventiva_Tareas_A_Realizar" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Tarea" HeaderText="Tarea" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" /> 
                    <asp:BoundField DataField="FechaFin" HeaderText="Fin" /> 
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlCorrectiva_ServiciosAfectados" runat="server" Visible="false">
        <ContentTemplate>
            <asp:GridView ID="gvCorrectiva_ServiciosAfectados" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Inicio" visible="false"/> 
                    <asp:BoundField DataField="idsolicitud" HeaderText="solicitud" visible="false"/> 
                    <asp:BoundField DataField="descripcion" HeaderText="Servicio Afectado" />  
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

