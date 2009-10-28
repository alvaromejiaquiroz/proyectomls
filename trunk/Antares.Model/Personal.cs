using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class Personal : ActiveRecordBase<Personal>
    {



        public static DbDataReader FindAllMenoTalPersona(int unId)
        {
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Personal));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = "select  *  from WebAntares.dbo.Personal where id_empleados <> " + unId.ToString();

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();

        }

        public static DbDataReader FindAllMenoTalPersona()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();

            #region DeefSP
            /*
            create proc Proc_getSolicitudes
            as
            select  
                s.Id_Solicitud as  Solicitud
                ,s.Descripcion
                ,ts.Descripcion as TipoSolicitud
                ,Fecha_Inicio as Inicio
                ,Fecha_Fin as Fin
                ,Status as  Estado
                from solicitud s
                join usuarios u on s.idusuariocreador = u.idusuario
                join Tipo_Solicitud ts on s.IdTipoSolicitud = ts.Id_TipoSolicitud
                left join empresas e on s.idcliente = e.id_empresa
            where id_solicitud not in ( select idSolicitud from solicitud_relacion)
                order by fechaCreacion desc
            */
            #endregion

            string sSQL = "Proc_getSolicitudes";

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public static Personal GetById(string Id)
        {
            try
            {
                return Personal.FindFirst(Expression.Eq("IdEmpleados", int.Parse(Id)));
            }
            catch (Exception e)
            {
                return new Personal();
            }
        }
        
        public static DbDataReader GetHorasSolicitudes()
        {

            string sSql = @"exec dbo.Proc_GetTiempos_Personal";// @idSolicitud = " + IdSolicitud.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
            
        }

        public static DbDataReader GetHorasSolicitudes(string IdEmpleado,string Fecha)
        {

            string sSql = @"exec dbo.Proc_GetTiempos_Personal  @Id_Empleado =" + IdEmpleado + " , @Fecha =" + Fecha;
            return CommonFunctions.ExecuteDbReader(sSql);

        }

        public static Personal[] GetPersonalActivo()
        {
            return Antares.model.Personal.FindAll(Expression.Eq("Activo", "si"));

        }

    }
}
