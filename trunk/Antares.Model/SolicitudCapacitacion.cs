using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudCapacitacion : ActiveRecordBase<SolicitudCapacitacion>
    {
        public static DbDataReader GetReader()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCapacitacion));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"
                            select 
                            s.ID_Solicitud
                            ,c.FechaInicio
                            ,c.FechaFin
                            ,c.Descripcion
                            ,c.Nivel
                            ,c.Duracion
                            ,c.Area_Estudio
                            ,c.Instructor
                            from WebAntares.dbo.Solicitud s
                            join Solicitud_capacitacion c on s.Id_Solicitud = c.idsolicitud
                            order by fechaInicio desc
                            ";
            
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public static DbDataReader Get_Capacitacion_X_Persona(int IdPersona)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCapacitacion));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            DbParameter p = oConn.CreateParameter();
            
            p.DbType = System.Data.DbType.Int32;
            p.Value = IdPersona;
            p.ParameterName = "@IdEmpleado";
            
            oConn.Parameters.Add(p);
            oConn.CommandText = "Proc_Get_Capacitacion_X_Persona";
            oConn.CommandType = System.Data.CommandType.StoredProcedure;
            return oConn.ExecuteReader();
        }

        public static DbDataReader Get_TotalHoras_X_Persona_X_Solicitud(int IdSol , int IdPersona)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCapacitacion));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            DbParameter pPersona = oConn.CreateParameter();
            DbParameter pSolicitud = oConn.CreateParameter();

            pPersona.DbType = System.Data.DbType.Int32;
            pPersona.Value = IdPersona;
            pPersona.ParameterName = "@IdEmpleado";
            
            pSolicitud.Value = IdSol;
            pSolicitud.DbType = System.Data.DbType.Int32;
            pSolicitud.ParameterName = "@IdSolicitud";

            oConn.Parameters.Add(pSolicitud);
            oConn.Parameters.Add(pPersona);
            oConn.CommandText = "Proc_Get_TotalHoras_X_Persona_X_Solicitud";
            oConn.CommandType = System.Data.CommandType.StoredProcedure;
            return oConn.ExecuteReader();
        }
    }
}
