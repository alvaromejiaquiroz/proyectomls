using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_Tiempo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillTiempos();
    }

    private void fillTiempos()
    {
        Double lTiempo;

        for (lTiempo = 0.5; lTiempo < 10; lTiempo += .5)
        {
            ddpTiempo.Items.Add(lTiempo.ToString());
        }
    }

    public string Value
    {
        set
        {
            try
            {

                if (ddpTiempo.Items.FindByValue(value) != null)
                {
                    ddpTiempo.SelectedValue = value;
                }

            }
            catch (Exception e)
            {

                ddpTiempo.SelectedIndex = 0;

            }


        }
        get { return ddpTiempo.SelectedValue; }
    }
    public int ValueInt
    {
        get
        {
            try
            {
                return Convert.ToInt32(ddpTiempo.SelectedValue);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
    public double ValueDbl
    {
        get
        {
            try
            {
                return Convert.ToDouble(ddpTiempo.SelectedValue);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
