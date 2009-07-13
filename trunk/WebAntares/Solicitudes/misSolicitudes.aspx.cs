using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAntares;
using Antares.model;

public partial class Solicitudes_misSolicitudes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack)
            fillMisSolicitudes();
    }

    private void fillMisSolicitudes()
    {
        gvMisSolicitudes.DataSource = Antares.model.Solicitud.GetMisSolicitudes( cboPersonal.Value);
        gvMisSolicitudes.DataKeyNames = new string[] { "Id_solicitud" };
        gvMisSolicitudes.DataBind();
    
    }
    protected void gvMisSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idSolicitud = int.Parse(gvMisSolicitudes.SelectedDataKey.Value.ToString());
        BiFactory.Sol = Solicitud.GetById(idSolicitud);
        
        FillTareasHoras();

    }
    private void FillTareasHoras()
    {
        gvTareasRendidas.DataSource = BiFactory.Sol.GetTareasRendidas();
        gvTareasRendidas.DataBind();
    }
}
