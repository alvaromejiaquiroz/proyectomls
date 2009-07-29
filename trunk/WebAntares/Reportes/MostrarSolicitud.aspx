<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="MostrarSolicitud.aspx.cs" Inherits="Reportes_MostrarSolicitud" Title="Untitled Page" %>



<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server"> 

<style type="text/css">
.NroSolicitud  
{ 
    color: Red; 
    font-size:larger; 
    font-size-adjust:40px;
    font-family:Arial Greek; 
    font-family:Arial Black; 
}

#tdH 
{   color: Black;
    font-size:11,1px; 
    font-family:Arial Black;
    width:35%;
 
}
#tdH_2
{   color: Black;
    font-size:11,1px; 
    font-family:Arial Black;
    width:35%;
    padding-left:40px;
 
}

#gvPersonal{ width:100px; height:100px;} 

</style>
<br />
<br />
  
    <asp:UpdatePanel ID="pnlSolicitud" runat="server">
        <ContentTemplate>
        <hr />
            <table id="TablaCabecera" >
                <tr>
                    <td id="tdH">Solicitud:</td>
                    <td><asp:Label ID="lblNroSolicitud" CssClass="NroSolicitud" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td id="tdH_2">Fecha de Creación:</td>
                    <td>
                        <asp:Label ID="lblFechaCreacion" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td id="tdH">Estado:</td>
                    <td><asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td id="tdH_2">Tipo de Solicitud:</td>
                    <td><asp:Label ID="lblTipoSolicitud" runat="server" Text=""></asp:Label></td>
                </tr>
                
                </table>
                <br />
                <hr />
            <table id="tabla_datos_generales_solicitud">
                   <tr>
                           <td id="tdH"> Descripcion: </td>
                           <td><asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                    <tr>
                           <td id="tdH"> Numero de Orden del Cliente: </td>
                           <td><asp:Label ID="lblNRO_OrdenCliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td id="tdH"> Cliente: </td>
                           <td><asp:Label ID="lblCliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td id="tdH"> Contacto en el Cliente: </td>
                           <td><asp:Label ID="lblContacto_Cliente" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td id="tdH"> Mail del Contacto: </td>
                           <td><asp:Label ID="lblContacto_Mail" runat="server" Text=""></asp:Label> </td>
                   </tr> 
                   <tr>
                           <td id="tdH"> Telefono del Contacto: </td>
                           <td><asp:Label ID="lblContacto_Telefono" runat="server" Text=""></asp:Label> </td>
                   </tr> 
               </table>
               <br />
            
        </ContentTemplate>
       
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlPreventiva" runat="server" Visible="false">
        <ContentTemplate>
            <table id="tabla_Preventiva" >
                <tr>
                    <td id="tdH">Sitio:</td>
                    <td><asp:Label ID="lblPvSitio" runat="server" Text=""></asp:Label> </td>
                    <td></td>
                </tr>
                <tr>
                    <td id="tdH">Fecha Inicio:</td>
                    <td><asp:Label ID="lblPvFechaInicio" runat="server" Text=""></asp:Label></td>
                    <td></td>
                </tr>
                <tr>
                    <td id="tdH">Fecha Fin:</td>
                    <td><asp:Label ID="lblPvFechaFin" runat="server" Text=""></asp:Label> </td>
                    <td> </td>
                </tr>
                
            </table>
        </ContentTemplate>
       
    </asp:UpdatePanel>
   
    <asp:UpdatePanel ID="pnlCorrectiva" runat="server" Visible="false">
     <ContentTemplate>
        <table id="tabla_Correctiva">
            <tr>
                    <td id="tdH">Falla Reportada:</td>
                    <td><asp:Label ID="lblCorrectiva_Falla_Reportada" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td id="tdH">Plazo de Atención:</td>
                    <td><asp:Label ID="lblCorrectiva_Plazo_Atencion" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td id="tdH">Fecha de Notificacion por Parte del Cliente:</td>
                    <td><asp:Label ID="lblCorrectiva_FechaNotificacionCliente" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td id="tdH">Causa Probable:</td>
                    <td><asp:Label ID="lblCorrectiva_CausaProbable" runat="server" Text=""></asp:Label></td>
                    <td></td>
            </tr>
            <tr>
                    <td id="tdH">Persona que Reporto la Falla:</td>
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
                    <td id="tdH">Descripcion de Tareas:
                        </td>
                    <td><asp:Label ID="lblObra_Desc_Tareas" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
                </tr>
                    <tr>
                    <td id="tdH">Fecha de Inicio:
                        </td>
                    <td><asp:Label ID="lblObra_FechaInicio" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
                </tr>
                    <tr>
                    <td id="tdH">Fecha Fin:
                        </td>
                    <td><asp:Label ID="lblObra_FechaFin" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td id="tdH">Requisitos de Aprovacion:
                        </td>
                    <td><asp:Label ID="lblObra_Req_Aprovacion" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td id="tdH">Requisitos de Ingreso:
                        </td>
                    <td><asp:Label ID="lblObra_Req_Ingreso" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
                </tr>
                
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <hr />
    <asp:UpdatePanel ID="pnlComun" runat="server" Visible="false">
        <ContentTemplate>
        <table id="tabla_Comun">
            <tr>
                    <td id="tdH">Presupuesto:
                        </td>
                    <td><asp:Label ID="lblPresupuesto" runat="server" Text=""></asp:Label>
                        </td>
                    <td>
                        </td>
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

