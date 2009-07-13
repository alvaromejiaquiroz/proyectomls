<%@ WebHandler Language="C#" Class="personas" %>

using System;
using System.Web;
using Antares.model;
using NHibernate.Expression;

public class personas : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //   context.Response.Write("Hello World");

        if (context.Request.QueryString["q"] != null)
        {
            //Persona[] aa = Persona.FindAll(Expression.Like("Apellido", context.Request.QueryString["q"].ToString() + "%"));
            //foreach (Persona a in aa)
            //{
            //    context.Response.Write(a.Apellido + "," + a.Nombre + "|" + a.IdPersona + "\n");
            //}

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}