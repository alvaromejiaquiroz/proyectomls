using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ServiceModel.Channels;
using System.Data.Common;
using Castle.ActiveRecord;

public delegate void cboSelected1(string param1, string param2);

public partial class Controles_cboGeneric : System.Web.UI.UserControl
{
    public event cboSelected1 seleciona;
    private string mTabla;
    private string mNameId;
    private string mNameDescript;
    private TipoSource mTipoOrigen = TipoSource.Tabla;
    private bool _autoSel = false;
    private bool showID = false;

    public Unit Width
    {
        set { ddlCBO.Width = value; }
        get { return ddlCBO.Width; }
    }


    public bool AutoSelect
    {
        set { _autoSel = value; }

        get { return _autoSel; }
    }

    public bool Enabled
    {
        set { ddlCBO.Enabled = value; }
        get { return ddlCBO.Enabled; }
    }

    public string Value
    {
        set
        {
            try
            {

                if (ddlCBO.Items.FindByValue(value) != null)
                {
                    ddlCBO.SelectedValue = value;
                }

            }
            catch (Exception e)
            {

                ddlCBO.SelectedIndex = 0;

            }


        }
        get { return ddlCBO.SelectedValue; }
    }
    public int ValueInt
    {
        get
        {
            try
            {
                return Convert.ToInt32(ddlCBO.SelectedValue);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }

    public string Text
    {
        get { return ddlCBO.SelectedItem.Text; }
    }

    public TipoSource TipoOrigen { set { mTipoOrigen = value; } }



    public string tabla
    {
        set { mTabla = value; }
    }
    public string SourceData
    {
        set { mTabla = value; }
    }
    public string Caption
    {
        set { lblCaption.Text = value; }
    }

    public string NameId
    {
        set { mNameId = value; }
    }

    public string NameDescript
    {
        set { mNameDescript = value; }
    }
    /// <summary>
    /// Devuelve la cantidad de items
    /// </summary>
    public int Count
    {
        get
        {
            return ddlCBO.Items.Count - 1;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindCBO();
    }

    private DataTable getTable(string sSql)
    {
        // Expects a root type
        NHibernate.ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Antares.model.Adjunto));
        DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
        DbCommand oConn = db.CreateCommand();

        oConn.CommandText = sSql;
        IDataReader dr = oConn.ExecuteReader();
        CustomAdapter da = new CustomAdapter();
        DataTable dtOut = new DataTable();
        da.FillFromReader(dtOut, dr); //converts a datareader into a datatable
        return dtOut;

    }



    public void BindCBO()
    {
        try
        {
            DataTable table;
            if (mTipoOrigen == TipoSource.Procedure)
            {
                table = getTable(mTabla);
            }
            else
            {
                table = getTable("select " + mNameId + " ," + mNameDescript + " from " + mTabla + " order by " + mNameDescript);
            }
            DataRow dr;
            dr = table.NewRow();
            dr[mNameId] = "-1";
            dr[mNameDescript] = "Seleccione...";
            table.Rows.InsertAt(dr, 0);

            ddlCBO.Items.Clear();
            ddlCBO.DataSource = table;
            ddlCBO.DataTextField = mNameDescript;
            ddlCBO.DataValueField = mNameId;
            ddlCBO.DataBind();


            if (_autoSel == true && table.Rows.Count == 2)
            {
                if (ddlCBO.Items.Count > 1)
                {
                    ddlCBO.SelectedIndex = 1;
                    if (showID) lblCaption.Text = ddlCBO.SelectedValue;
                }
            }
        }
        catch (Exception e)
        {
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add("Error " + e.Message);

            ddlCBO.DataTextField = mNameDescript;
            ddlCBO.DataValueField = mNameId;
            ddlCBO.DataBind();
        }
    }
    protected void ddlCBO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (seleciona != null)
        { // El evento se lanza como cualquier delegado
            if (showID) lblCaption.Text = ddlCBO.SelectedValue;
            seleciona.Invoke(ddlCBO.SelectedValue, ddlCBO.SelectedItem.Text);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ddlCBO_SelectedIndexChanged(sender, e);
    }
    protected void ddlCBO_TextChanged(object sender, EventArgs e)
    {
        ddlCBO_SelectedIndexChanged(sender, e);
    }
    public enum TipoSource
    {
        Tabla, Vista, Procedure

    }
}
public class CustomAdapter : System.Data.Common.DbDataAdapter
{
    public int FillFromReader(DataTable dataTable, IDataReader dataReader)
    {
        return this.Fill(dataTable, dataReader);
    }
    protected override System.Data.Common.RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow a, IDbCommand b, StatementType c, System.Data.Common.DataTableMapping d)
    {
        return (System.Data.Common.RowUpdatedEventArgs)new EventArgs();
    }
}