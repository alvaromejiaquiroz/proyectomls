using System;
using System.Collections.Generic;
using System.Text;

using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;

using Gos.Usuarios;
using System.Reflection;

using System.Web;
using System.Web.UI.WebControls;
using Antares.model;
using NHibernate.Expression;


namespace WebAntares
{
    public static class BiFactory
    {

        public static Gos.Usuarios.Usuarios User
        {
            get
            {
                if ((Gos.Usuarios.Usuarios)System.Web.HttpContext.Current.Session["user"] != null)
                {
                    return (Gos.Usuarios.Usuarios)System.Web.HttpContext.Current.Session["user"];
                }
                else
                {
                    return new Gos.Usuarios.Usuarios();
                }
            }
        }

        public static Antares.model.UsuariosEmpleados Empleado
        {
            get
            {
                UsuariosEmpleados ue = UsuariosEmpleados.FindFirst(Expression.Eq("IdUsuarios", User.IdUsuario));
                    if (ue == null)
                    {
                        ue = new UsuariosEmpleados();
                        ue.IdUsuarioEmpleado =0;                       
                        }
                    return ue;

            }
        }

        public static Antares.model.Solicitud Sol
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Solicitud"] != null)
                {
                    return (Antares.model.Solicitud)System.Web.HttpContext.Current.Session["Solicitud"];
                }
                else
                {
                    //la creo y la devuelvo
                    Antares.model.Solicitud Sol = new Antares.model.Solicitud();
                    System.Web.HttpContext.Current.Session["Solicitud"] = Sol;
                    return Sol;

                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["Solicitud"] = value;
            }
        }

        public static bool CheckLogin(string User, string Login)
        {
            if (biUsuarios.FindAll().Length == 0)
            {
                Usuarios a = new Usuarios();

                a.Nombre = "Gabriel " + System.DateTime.Now.ToString();
                a.LoginName = "gos";
                a.Password = "123";
                a.Save();
            }

            System.Web.HttpContext.Current.Session["user"] = (Gos.Usuarios.Usuarios)biUsuarios.CheckLogin(User, Login);

            return (System.Web.HttpContext.Current.Session["user"] != null);

        }
        public static void initActiveRecord()
        {

            XmlConfigurationSource config = new XmlConfigurationSource(AppDomain.CurrentDomain.BaseDirectory.ToString() + "ARConfig.xml");

            // Se crea la instancia de configuracion
            //IConfigurationSource config = ActiveRecordSectionHandler.Instance;

            // Se inicializa el framework
            //ActiveRecordStarter.Initialize(config,typeof(Gos.Usuarios.User));

            /*Assembly asm = Assembly.Load("Gos.Usuarios");
            ActiveRecordStarter.Initialize(asm, config);
            */
            Assembly asm1 = Assembly.Load("Gos.Usuarios");
            Assembly asm2 = Assembly.Load("Antares.model");
            ActiveRecordStarter.Initialize(new Assembly[] { asm1, asm2 }, config);

            // Se elimna el esquema de la base de datos
            //ActiveRecordStarter.DropSchema();

            // Se crea el equema en la base de datos y te borra todos los datos

            //ActiveRecordStarter.CreateSchema();
            //ActiveRecordStarter.GenerateCreationScripts(@"d:\webAntares.sql");
            //ActiveRecordStarter.CreateSchema(typeof(Cajas.Model.Empresas));

            CrearDefaults();
        }

        private static void CrearDefaults()
        {

            //creamos los estados
            if (Antares.model.Estados.FindAll().Length == 0)
            {
                string[] estados = { "Pendiente", "Realizado", "Reprogramado", "Suspendido" };
                foreach (string s in estados)
                {
                    Antares.model.Estados e = new Antares.model.Estados();
                    e.Detalle = s;
                    e.Save();
                }
            }

            //creamos los perfiles

            if (Gos.Usuarios.Perfiles.FindAll().Length == 0)
            {
                string[] items = { "Administrador", "Supervisor", "Usuario", "Consultor" };
                foreach (string s in items)
                {
                    Gos.Usuarios.Perfiles e = new Gos.Usuarios.Perfiles();
                    e.Detalle = s;
                    e.Save();
                }
            }

        }
    }


    //Public Estados
    public enum eEstados
    {
        Pendiente = 1,
        Realizado=2,
        Reprogramado=3,
        Suspendido=4,
        Anulada = 5

    }

    public enum ePerfil
    {
        Administrador = 1,
        Supervisor = 2,
        Usuario = 3,
        Consultor = 4
    }
}