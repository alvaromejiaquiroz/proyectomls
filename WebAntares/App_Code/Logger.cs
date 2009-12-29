using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
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

    private static  void LogToFile (TipoEvento tipoEvento, string detalle)
        {
        StreamWriter st;
        if (!Directory.Exists(HttpContext.Current.Server.MapPath ( "~/Logs")))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath ( "~/Logs"));
        }

        string unPath = HttpContext.Current.Server.MapPath ( "~/Logs/Log_" + DateTime.Today.ToString("yyyyMMdd") + ".log");

        if (!File.Exists(unPath ))
            {
                File.CreateText(unPath).Close();

            }
        
        st = File.AppendText(unPath);

        if (detalle == null)
        {
            detalle = string.Empty;
        }
        st.WriteLine( DateTime.Now.ToString() + " - [" + TipoEvento.Login.ToString() + "] - " + BiFactory.User.LoginName + " - " + detalle);
        st.Close();

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
        LogToFile(tipoEvento, detalle);

    }
    
    public static void LogToFile(string tipo ,string detalle)
    {
        StreamWriter st;
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
        }

        string unPath = HttpContext.Current.Server.MapPath("~/Logs/Log_" + DateTime.Today.ToString("yyyyMMdd") + ".log");

        if (!File.Exists(unPath))
        {
            File.CreateText(unPath).Close();
        }

        st = File.AppendText(unPath);

        if (detalle == null)
        {
            detalle = string.Empty;
        }
        st.WriteLine(DateTime.Now.ToString() + " - [" + tipo + "] - " + BiFactory.User.LoginName + " - " + detalle);
        st.Close();

    }
       
}

public enum TipoEvento
{
    Login = 1,
    AsignaPermisos = 2,
    QuitaPermisos = 3,
    CreaSolicitud = 4,
    EditaSolicitud = 5,
    ApruebaSolicitudCalidad =  6,
    ApruebaSolicitudCoordinacion =  7
}
