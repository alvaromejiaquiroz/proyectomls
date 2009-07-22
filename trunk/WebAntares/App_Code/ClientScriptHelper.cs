using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for ClientScriptHelper
/// </summary>
public class ClientScriptHelper
{
    public static string UploadFrameLoad(string uploadFrameName, string uploadButtonID)
    {
        return @"<script language=""javascript"" type=""text/javascript"">
                    function iUploadFrameLoad() {
                    if (window." + uploadFrameName + @".document.body.innerHTML == ""Cargando..."") {
                        document.getElementById('" + uploadButtonID + @"').click();
                        }
                    }
                </script>";
    }
}
