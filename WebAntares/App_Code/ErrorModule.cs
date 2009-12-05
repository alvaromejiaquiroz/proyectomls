using System;
using System.Web;
using System.Data;
using System.Configuration;

namespace Superexpert 
{ 

/// <summary> 
/// Handles all unhanlded exceptions in an application 
/// </summary> 
public class ErrorModule : IHttpModule 
{ 
  public void Init(HttpApplication app) 
  { 
    app.Error += new EventHandler(app_Error); 
  } 
  /// <summary> 
  /// When debug mode is enabled, display 
  /// friendly error page 
  /// </summary> 
  void app_Error(object sender, EventArgs e) 
  { 
    HttpApplication app = (HttpApplication)sender; 
    HttpContext context = app.Context; 
    Exception error = context.Server.GetLastError().GetBaseException(); 
    context.Response.Clear(); 
       context.Server.Transfer("~/Errores/Error.aspx"); 
    //CompilationSection compilationConfig = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation"); 
    
    //  if (compilationConfig.Debug)
         
    //else
    //      context.Server.Transfer("~/Errores/Error.aspx"); 
  } 

  public void Dispose() 
  { 
  } 
} 
} 
