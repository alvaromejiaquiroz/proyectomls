using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

public class gridPosition
{
    public int RowIndex;
    public int ColIndex;
    public string Value;
    public string Text;
    public TableCell tc;
    public DataTable dt;
    public XmlNode xNode;
    public Boolean Header;
    public DataRow dr;

}

public delegate void RenderRow(TableRow r);
public delegate void RenderCell(gridPosition oPos);

class myGrid
{
    DataSet ds;

    public RenderRow RowHandler;
    public RenderCell HandlerHeadCell;
    public RenderCell HandlerBodyCell;
    private DataTable _dt;

    private gridPosition oPos = new gridPosition();
    public DataTable Data
    {
        get { return _dt; }
    }
    public Table Render(DataTable dt, string sStored)
    {
        _dt = dt;
        oPos.dt = dt;
        Table myTabla = new Table();
        myTabla.Attributes["class"] = "lightGrid";
        myTabla.Attributes["cellspacing"] = "0";
        myTabla.Attributes["cellpadding"] = "2";
        myTabla.Rows.Add(new TableRow());
        myTabla.Rows[0].Cells.Add(new TableCell());
        myTabla.Rows[0].Cells[0].Text = "SQL:" + sStored;
        myTabla.Rows[0].Attributes["id"] = "idSpText";
        myTabla.Rows[0].Attributes["style"] = "display:none";
        myTabla.Rows[0].Cells[0].Attributes["colspan"] = dt.Columns.Count.ToString();

        XmlDocument xDoc = new XmlDocument();
        XmlNode xhead;
        XmlNode xCell;
        xhead = xDoc.CreateElement("header");


        int iDeph = 0;
        //header
        #region HeaderXML
        for (int c = 0; c < dt.Columns.Count; c++)
        {
            string[] partes = dt.Columns[c].ColumnName.Split(new string[] { "||" }, StringSplitOptions.None);
            xCell = (XmlElement)xhead.SelectSingleNode("//td[@caption='" + partes[0] + "']");
            if (xCell == null)
            {
                xCell = xDoc.CreateElement("td");
                XmlAttribute attrib = xDoc.CreateAttribute("childs");
                attrib.Value = "0";
                xCell.Attributes.Append(attrib);


                attrib = xDoc.CreateAttribute("level");
                attrib.Value = "0";
                xCell.Attributes.Append(attrib);

                attrib = xDoc.CreateAttribute("caption");
                attrib.Value = partes[0];
                xCell.Attributes.Append(attrib);

                if (c < 2)
                {
                    attrib = xDoc.CreateAttribute("display");
                    attrib.Value = "none";
                }
                xCell.Attributes.Append(attrib);
            }
            else
            {
                xCell.Attributes["childs"].Value = (int.Parse(xCell.Attributes["childs"].Value.ToString()) + 1).ToString();
            }


            if (partes.Length > 1)
            {
                XmlElement xChild = xDoc.CreateElement("td");
                xChild.SetAttribute("caption", partes[1]);
                xChild.SetAttribute("level", "1");
                xCell.AppendChild(xChild);
                xChild = null;
            }
            xhead.AppendChild(xCell);

        }

        #endregion

        TableRow tr;
        int ifirstCell = 0;
        #region Header
        //armamos el header
        for (int r = 0; r < 2; r++)
        {
            tr = new TableRow();

            XmlNodeList xNodes = xhead.SelectNodes("//td[@level='" + r + "']");
            foreach (XmlNode xNode in xNodes)
            {
                TableCell tc = new TableCell();

                RenderHeadCell(ifirstCell, r, xNode, tc);

                tr.Cells.Add(tc);
            }

            myTabla.Rows.Add(tr);

        }
        #endregion

        #region Datos
        //datos
        foreach (DataRow dr in dt.Rows)
        {
            tr = new TableRow();

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                TableCell tc = new TableCell();
                RenderBodyCell(dt, xhead, tr, dr, c, tc);
            }
            myTabla.Rows.Add(tr);
        }
        #endregion

        return myTabla;
    }

    private void RenderBodyCell(DataTable dt, XmlNode xhead, TableRow tr, DataRow dr, int c, TableCell tc)
    {
        try
        {
            if (xhead.ChildNodes[c].Attributes["display"] != null)
            {
                tc.Attributes["style"] = "display:" + xhead.ChildNodes[c].Attributes["display"].Value;
            }
        }
        catch (Exception e)
        {
        }

        tc.Text = dr[c].ToString().Trim();
        //va con el borde
        tc.Attributes["class"] += " bord0XX0";
        if (tc.Text.Length == 0) tc.Text = "&nbsp;";
        tc.Attributes["col"] = c.ToString();


        if (HandlerBodyCell != null)
        {

            oPos.tc = tc;
            oPos.xNode = xhead;
            oPos.ColIndex = c;
            oPos.RowIndex = 0;
            oPos.dr = dr;
            oPos.Header = false;
            HandlerBodyCell(oPos);

        }
        tr.Cells.Add(tc);
    }

    private void RenderHeadCell(int ifirstCell, int r, XmlNode xNode, TableCell tc)
    {
        if (xNode.Attributes["display"] != null)
        {
            tc.Attributes["style"] = "display:" + xNode.Attributes["display"].Value;
        }
        else
        {
            if (ifirstCell == 0)
            {
                tc.Attributes["onclick"] = "var id=document.getElementById('idSpText'); if(id.style.display==''){id.style.display='none'}else{id.style.display=''}";
                ifirstCell++;
            }
        }

        tc.Attributes["class"] = "G_HD" + r.ToString() + " bordXXX0";

        tc.Text = xNode.Attributes["caption"].Value.ToString();
        try
        {
            if (xNode.Attributes["childs"].Value.ToString() == "0")
            {
                tc.Attributes["rowspan"] = "2";
            }

            if (xNode.ChildNodes.Count > 0)
            {
                tc.Attributes["colspan"] = (xNode.ChildNodes.Count).ToString();
            }
        }
        catch (Exception e)
        {
        }

        if (HandlerHeadCell != null)
        {

            oPos.tc = tc;
            oPos.xNode = xNode;
            oPos.ColIndex = r;
            oPos.RowIndex = 0;
            oPos.Header = true;
            HandlerHeadCell(oPos);
        }


    }
}

