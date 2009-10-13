<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SolDetalle.ascx.cs" Inherits="Controles_SolDetalle" %>


        <ul id="solDetalle">
            <li class="text_custom">
                <span>Solicitud:    </span>
                <span class="Numero_Solicitud"><%Response.Write(WebAntares.BiFactory.Sol.Id_Solicitud.ToString() ); %> </span> 
            </li>
            <li class="text_custom">
                <span class="text_custom">Tipo:</span>
                        <%try { Response.Write(WebAntares.BiFactory.Sol.Tipo.Descripcion); }catch (Exception) { };%>
            </li>
            <li class="text_custom">
                <span>Detalle:</span>
                        <%Response.Write(WebAntares.BiFactory.Sol.Descripcion); %>
            </li>



        </ul>
