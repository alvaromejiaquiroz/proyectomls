using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Antares.model;
using System.Net;
using WebAntares;

/// <summary>
/// Summary description for Logger
/// </summary>
public static class Logger
{
    public static void Log(TipoEvento tipoEvento)
    {
        Log(tipoEvento, null);
    }

    public static void Log(TipoEvento tipoEvento, string detalle)
    {
        Evento evento = new Evento();
        evento.IdUsuario = BiFactory.User.IdUsuario;
        evento.IdTipoEvento = (int)tipoEvento;
        evento.Fecha = DateTime.Now;
        string host = string.Empty;
        try
        {
            host = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' })[0].ToString();
        }
        catch (Exception)
        {
            host = HttpContext.Current.Request.UserHostAddress;
        }
        evento.Host = host;
        evento.Detalle = detalle;
        evento.Save();
    }
}

public enum TipoEvento
{
    Login = 1
}
