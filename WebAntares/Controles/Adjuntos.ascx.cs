using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antares.model;
using NHibernate.Expression;
using WebAntares;

public partial class Controles_Adjuntos : System.Web.UI.UserControl
{
    public Solicitud sol;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (sol != null)
        {
            FillAdjuntos();
        }
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


        long lMaxFileSize = 300000;
        string sFileDir = Server.MapPath("~/upload/");


        if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
        {
            //determine file name
            string sFileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);
            try
            {
                if (File1.PostedFile.ContentLength <= lMaxFileSize)
                {
                    //Save File on disk
                    sFileName = System.Guid.NewGuid().ToString();
                    File1.PostedFile.SaveAs(sFileDir + sFileName);
                    //relacionar el adjunto
                    adj.PathFile = sFileDir + sFileName;
                    adj.Date = System.DateTime.Now;
                    adj.FileName = File1.FileName;
                    adj.Size = File1.PostedFile.ContentLength;
                    adj.ContentType = File1.PostedFile.ContentType;

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
    protected void cmdUpload_Click(object sender, EventArgs e)
    {
        Guardar();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Adjunto t = Adjunto.FindFirst(Expression.Eq("IdAdjunto", int.Parse(gvFiles.DataKeys[e.RowIndex].Value.ToString())));
        SolicitudAdjuntos sadj = SolicitudAdjuntos.FindFirst(Expression.Eq("IdAdjunto", t.IdAdjunto));

        t.Delete();
        sadj.Delete();

        FillAdjuntos();
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

}
