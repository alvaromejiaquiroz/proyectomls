<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SolDetalle.ascx.cs" Inherits="Controles_SolDetalle" %>


<ul id="solDetalle">
<li><span>Solicitud:</span><%Response.Write(WebAntares.BiFactory.Sol.Id_Solicitud.ToString()); %></li>
<li><span>Tipo:</span><%try { Response.Write(WebAntares.BiFactory.Sol.Tipo.Descripcion); }
           catch (Exception) { };%></li>
           <li><span>Detalle:</span><%Response.Write(WebAntares.BiFactory.Sol.Descripcion); %></li>
           <li><span visible="false">Relacionada:</span><%Response.Write(WebAntares.BiFactory.Sol.RelacionadaCon); %></li>
</ul>