using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antares.model;
using NHibernate.Expression;
using WebAntares;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


public partial class Controles_Adjuntos : System.Web.UI.UserControl
{
    public Solicitud sol;

    private TipoSolicitudEnum _tipoSolicitud;

    public TipoSolicitudEnum TipoSol
    {
        get { return _tipoSolicitud; }
        set { _tipoSolicitud = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string html = string.Empty;
        switch (TipoSol)
        {
            case TipoSolicitudEnum.MantenimientoPreventivo:
                html = "MantPreventivoUpload";
                break;
            case TipoSolicitudEnum.MantenimientoCorrectivo:
                html = "MantCorrectivoUpload";
                break;
            case TipoSolicitudEnum.MantenimientoPreventivoRendicion:
                html = "MantPreventivoRendicionUpload";
                break;
            case TipoSolicitudEnum.MantenimientoCorrectivoRendicion:
                html = "MantCorrectivoRendicionUpload";
                break;
            case TipoSolicitudEnum.Obras:
                html = "ObrasUpload";
                break;
            case TipoSolicitudEnum.ReporteObras:
                html = "ReporteObrasUpload";
                break;
        }

        iUploadFrame.Attributes.Add("src", "../Html/" + html + ".htm");
        iUploadFrame.Attributes.Add("onload", "iUploadFrameLoad()");
        Page.ClientScript.RegisterStartupScript(GetType(), "UploadScript", ClientScriptHelper.UploadFrameLoad(iUploadFrame.ClientID, btnUpload.ClientID));
        if (sol != null)
        {
            FillAdjuntos();
        }
        if (Request.Files.Count == 1)
        {
            Guardar();
            Response.Write("Cargando...");
            Response.End();
        }
    }
            
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
    }

    public bool Guardar()
    {
        Adjunto adj = new Adjunto();
        SolicitudAdjuntos solAdj = new SolicitudAdjuntos();
        if (sol == null)
        {
            sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        }

        solAdj.IdSolicitud = sol.Id_Solicitud;


        long lMaxFileSize = 3000000;
        string sFileDir = Server.MapPath("~/upload/");


        if ((Request.Files[0] != null) && (Request.Files[0].ContentLength > 0))
        {
            //determine file name
            string OriginalName = System.IO.Path.GetFileName(Request.Files[0].FileName);
            string sFileName = string.Empty;
            try
            {
                if (Request.Files[0].ContentLength <= lMaxFileSize)
                {
                    //Save File on disk
                    sFileName = System.Guid.NewGuid().ToString();
                    Request.Files[0].SaveAs(sFileDir + sFileName);
                    //relacionar el adjunto
                    adj.PathFile = sFileDir + sFileName;
                    adj.Date = System.DateTime.Now;
                    adj.FileName = OriginalName;
                    adj.Size = Request.Files[0].ContentLength;
                    adj.ContentType = Request.Files[0].ContentType;
                    adj.Save();

                    solAdj.IdAdjunto = adj.IdAdjunto;
                    solAdj.Save();

                    lblMessage.Visible = true;
                    lblMessage.Text = "Se agrego el archivo correctamente.";
                }
                else //reject file
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "El tamaño del archivo supera el limite de " + lMaxFileSize;
                }
            }
            catch (Exception ee)//in case of an error
            {
                lblMessage.Visible = true;
                lblMessage.Text = ee.Message;
            }
        }
        FillAdjuntos();
        return true;
    }

    private void FillAdjuntos()
    {
        gvFiles.DataSource = sol.GetAdjuntos();
        gvFiles.DataKeyNames = new string[] { "IdAdjunto" };
        gvFiles.DataBind();
    }
        
    public void ListaAdjuntos(string idSol)
    {
        Solicitud sol = Solicitud.Find(int.Parse(idSol));
        if (sol != null)
        {
            gvFiles.DataSource = sol.GetAdjuntos(idSol);
            gvFiles.DataKeyNames = new string[] { "IdAdjunto" };
            gvFiles.DataBind();
        }
    }

    protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Adjunto t = Adjunto.FindFirst(Expression.Eq("IdAdjunto", int.Parse(gvFiles.DataKeys[e.RowIndex].Value.ToString())));
        SolicitudAdjuntos sadj = SolicitudAdjuntos.FindFirst(Expression.Eq("IdAdjunto", t.IdAdjunto));

        t.Delete();
        sadj.Delete();

        uniqueId = 1;
        FillAdjuntos();
    }

    int uniqueId = 1;

    protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.ID = "FileRow" + uniqueId.ToString();
        uniqueId += 1;
    }
}
