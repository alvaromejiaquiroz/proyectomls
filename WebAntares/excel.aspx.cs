using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using System.Data.Common;
using System.Globalization;

public partial class excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gv.DataSource = Personal.FindAll();
        gv.DataBind();
    }
    protected void lkn_Click(object sender, EventArgs e)
    {
        GridViewExportUtil.Export("Customers.pdf", this.gv);

    }
}
