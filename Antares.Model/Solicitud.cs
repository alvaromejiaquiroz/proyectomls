using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Globalization;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class Solicitud : ActiveRecordBase<Solicitud>
    {

        private TipoSolicitud _tipo = null;

        public static Solicitud GetById(Int32 pid)
        {
            return Solicitud.FindByPrimaryKey(pid);
        }

        public TipoSolicitud Tipo
        {
            get
            {
                if (_tipo == null)
                {
                    _tipo = TipoSolicitud.FindByPrimaryKey(this.IdTipoSolicitud);
                }
                if (this.IdTipoSolicitud != _tipo.IdTiposolicitud)
                {
                    _tipo = TipoSolicitud.FindByPrimaryKey(this.IdTipoSolicitud);

                }
                return _tipo;
            }
        }

        public static DbDataReader GetReader()
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

        public static DbDataReader GetReader(string idSol ,string idTipoSolicitud,string Perfil,int idEmpleado , string estado , string Fecha)
        {
            if (idSol == string.Empty)
            {
                idSol = "null";
            }

            if (idTipoSolicitud == "-1")
            {
                idTipoSolicitud = "null";
            }
            
            if (estado == "-1")
            {
                estado = "null";
            }

            string qry = "Proc_getSolicitudes @idSolicitud=" + idSol + 
                ", @idTipoSolicitud =" + idTipoSolicitud + 
                ", @idEstado =" + estado;
            
            if (Fecha != string.Empty)
            {
                CultureInfo nfo = new CultureInfo("es-ES");
                DateTime date = DateTime.Parse(Fecha, nfo);
                //Mando la fecha con formato ISO 112 , por la cultura del browser, es lo unico que se me ocurrio
                qry = qry + ",@Fecha ='" + date.ToString("yyyyMMdd") + "'";
            }
            //si el perfil es menor a 4 significa que es gerente, ve todas las solicitides, sino busca por usuario o responsable
            if (int.Parse(Perfil) < 4)
            {
                Perfil = "null";
                qry = qry + ",@IdEmpleado = null";
            }
            else
            {
                qry = qry + ",@IdEmpleado = " + idEmpleado.ToString();
            }
            return ExecuteDbReader(qry);
        }

   

        public DbDataReader GetAdjuntos()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
//            string sSQL = @"select a.IdAdjunto , FileName, Date, Size from adjunto a inner join solicitudAdjuntos sa
//                            on a.idadjunto = sa.idadjunto
//                            where sa.idsolicitud = " + this.Id_Solicitud;

            string sSQL = @"exec dbo.Prod_GetSolicitud_Adjuntos @idSolicitud = " + this.Id_Solicitud.ToString();
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public DbDataReader GetTareasRendidas()
        {
            #region DefSP
            //create proc proc_getTareasHorasFromSolicitud
            //@idSolicitud int
            //as
            //select th.id, 
            // t.tarea,
            //th.fecha,
            //th.duracion,
            //isnull(th.comentarios,'--') Comentario
            //from solicitud_tareas st
            //inner join  tareas_horas th
            //inner join  tareas t on th.id_tipotarea = t.id
            //on st.id = th.id_tarea
            //where id_solicitud = @idsolicitud
            #endregion

            // Expects a root type
            string sSQL = @"proc_getTareasHorasFromSolicitud " + this.Id_Solicitud;
            return ExecuteDbReader(sSQL);

        }

        public DbDataReader GetVehiculosRendidos()
        {
            #region defSP
            /*
             * create proc Proc_getVehiculosRendicion
@idSolicitud int 
as
select vk.idVehiculosKilometros, Fecha, Marca +' '+  patente  vehiculo,
Kilometros,
Tiempo
  from vehiculosKilometros	 vk
inner join solicitud_recursos_vehiculos sv
on
sv.id = vk.idvehiculoSolicitud
inner join Vehiculos v 
on sv.id_vehiculo = v.id_vehiculos
where sv.Id_solicitud = @idSolicitud
             * */
            #endregion
            string sSQL = @"Proc_getVehiculosRendicion " + this.Id_Solicitud;
            return ExecuteDbReader(sSQL);
        }

        private static DbDataReader ExecuteDbReader(string SSQLQuery)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            oConn.CommandText = SSQLQuery;
            return oConn.ExecuteReader();
        }

        public string RelacionadaCon
        {
            get
            {
                Solicitud_Relacion sr = Solicitud_Relacion.FindOne(Expression.Eq("IdSolicitud", this.Id_Solicitud));

                if (sr != null)
                {
                    return sr.IdSolicitud_Relacionada.ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        public static DbDataReader GetMisSolicitudes(string idEmpleado)
        {
            #region SPDEF
            /*
create Proc Prod_GetMisSolicitudes
@idPersona int
as
select 
distinct
s.Id_solicitud
,FechaCreacion Creado
,p.apellido +', '+ p.nombres Creador
,Fecha_inicio Inicio
,Fecha_fin Fin
,ts.descripcion Tipo
,s.Descripcion Referencia
,'--' Cargada_por
,'--' Responsable
,s.status Estado
 

from dbo.Solicitud_Recursos_Empleados se
inner join dbo.Solicitud_Relacion sr on sr.Idsolicitud = se.id_solicitud
inner join solicitud s  on sr.Idsolicitud_relacionada  = s.id_solicitud
left join solicitud_tareas st on s.id_solicitud = st.Id_solicitud
left join tipo_solicitud ts on s.idTipoSolicitud = ts.Id_TipoSolicitud 
left join Personal p on s.IdUsuarioCreador = p.id_empleados
where se.id_empleado = @idPersona
    */
            #endregion

            return ExecuteDbReader("Prod_GetMisSolicitudes " + idEmpleado);
        }
        public static Boolean ExisteCorrectiva(string idSolicitud)
        {
            bool existe = false;

            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select count(0) C
                            from WebAntares.dbo.Solicitud s
                            join WebAntares.dbo.Solicitud_Correctivo c on s.id_solicitud = c.IdSolicitud ";
            sSQL += " where s.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.HasRows) && (int.Parse(dr["C"].ToString()) > 0))
                {
                    existe = true;
                }
            }
            return existe;

        }
        public static Boolean ExisteObra(string idSolicitud)
        {
            bool existe = false;

            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select count(0) C
                            from WebAntares.dbo.Solicitud s
                            join WebAntares.dbo.Solicitud_Obra c on s.id_solicitud = c.Id_Solicitud ";
            sSQL += " where s.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.HasRows) && (int.Parse(dr["C"].ToString()) > 0))
                {
                    existe = true;
                }
            }
            return existe;

        }

        public static DbDataReader GetSolicitudCorrectiva(string idSolicitud)
        {
            DataTable dt = new DataTable();
            DbDataReader dr;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();


            string sSQL = "Prod_GetSolicitud_Correctiva @idSolicitud = " + idSolicitud;

            oConn.CommandText = sSQL;
            dr = oConn.ExecuteReader();
            //while (dr.Read())
            //{
            //    DataRow row = dt.NewRow();
            //    row = dr[
            //    dt.Rows.Add(row);

            //};
            return dr;






        }
        public static DbDataReader GetSolicitudEstados()
        {
            DataTable dt = new DataTable();
            DbDataReader dr;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();


            string sSQL = "select id ,descripcion from dbo.Solicitud_Estados";

            oConn.CommandText = sSQL;
            dr = oConn.ExecuteReader();
            return dr;






        }
        public static Boolean TieneResponsable(string idSolicitud)
        {
            bool existe = false;

            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select count(0) C
                            from WebAntares.dbo.Solicitud s
                            join WebAntares.dbo.Solicitud_Recursos_Empleados c on s.id_solicitud = c.Id_Solicitud
                            where c.responsable = 1";
            sSQL += " and  s.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.HasRows) && (int.Parse(dr["C"].ToString()) > 0))
                {
                    existe = true;
                }
            }
            return existe;

        }
        public static Boolean TieneVehiculosAsignados(string idSolicitud)
        {
            bool existe = false;

            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select count(0) C
                            from WebAntares.dbo.Solicitud s
                            join WebAntares.dbo.Solicitud_Recursos_Vehiculos c on s.id_solicitud = c.Id_Solicitud";
            sSQL += " where  s.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.HasRows) && (int.Parse(dr["C"].ToString()) > 0))
                {
                    existe = true;
                }
            }
            return existe;

        }
        public static Boolean TieneServiciosAfectados(string idSolicitud)
        {
            bool existe = false;

            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select count(0) C
                            from WebAntares.dbo.Solicitud s
                            join WebAntares.dbo.Solicitud_Servicios_Afectados c on s.id_solicitud = c.IdSolicitud";
            sSQL += " where  s.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if ((dr.HasRows) && (int.Parse(dr["C"].ToString()) > 0))
                {
                    existe = true;
                }
            }
            return existe;

        }

        /// <summary>
        /// Calcula la fecha de inicio y de fin de la solicitud
        /// </summary>
        /// <param name="idSolicitud"></param>
        public static FechaRango PeriodoDesdeHasta(int idSolicitud)
        {

            FechaRango r = new FechaRango();

            string sSQL = @"select min(fechaInicio) inicio, max(fechafin) fin from solicitud_tareas
                           where id_solicitud =  " + idSolicitud.ToString();


            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["inicio"] != System.DBNull.Value)
                    {
                        r.Inicio = DateTime.Today.AddMonths(-1);
                            
                    }
                    else
                    {
                        r.Inicio = DateTime.Today;
                    }
                    if (dr["fin"] != System.DBNull.Value)
                    {
                        r.Fin = DateTime.Parse(dr["fin"].ToString());
                    }
                    else
                    {
                        r.Fin = DateTime.Today;
                    }
                }
            }
            return r;
        }

        public DbDataReader GetAdjuntos(string IdSolicitud)
        {
            string sSql = @"exec dbo.Prod_GetSolicitud_Adjuntos @idSolicitud = " + IdSolicitud.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }

    }
    /// <summary>
    /// Devuelve un rango de fecha inicio fin
    /// </summary>
    public class FechaRango
    {
        public DateTime Inicio;
        public DateTime Fin;

       
    }


}

