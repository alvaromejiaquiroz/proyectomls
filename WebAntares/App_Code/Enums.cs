using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public enum TipoSolicitudEnum
{
    MantenimientoPreventivo = 1, MantenimientoCorrectivo = 2, MantenimientoPreventivoRendicion = 3, MantenimientoCorrectivoRendicion = 4, Obras = 5, ReporteObras = 6
}