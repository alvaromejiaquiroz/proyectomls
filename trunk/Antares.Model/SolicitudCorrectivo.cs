using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudCorrectivo : ActiveRecordBase<SolicitudCorrectivo>
    {

        

        public static SolicitudCorrectivo GetById(Int32 pid)
        {
            return SolicitudCorrectivo.FindByPrimaryKey(pid);
        }

        public TipoSolicitudCorrectivo Tipo
        {
            get
            {
                if (_tipo == null)
                {
                    _tipo = TipoSolicitudCorrectivo.FindByPrimaryKey(this.IdTipoSolicitudCorrectivo);
                }
                if (this.IdTipoSolicitudCorrectivo != _tipo.IdTipoSolicitudCorrectivo)
                {
                    _tipo = TipoSolicitudCorrectivo.FindByPrimaryKey(this.IdTipoSolicitudCorrectivo);

                }
                return _tipo;
            }
        }

        public static DbDataReader GetReader()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCorrectivo));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();

            #region DeefSP
            /*
            create proc Proc_getSolicitudCorrectivoes
            as
            select  
                s.Id_SolicitudCorrectivo as  SolicitudCorrectivo
                ,s.Descripcion
                ,ts.Descripcion as TipoSolicitudCorrectivo
                ,Fecha_Inicio as Inicio
                ,Fecha_Fin as Fin
                ,Status as  Estado
                from SolicitudCorrectivo s
                join usuarios u on s.idusuariocreador = u.idusuario
                join Tipo_SolicitudCorrectivo ts on s.IdTipoSolicitudCorrectivo = ts.Id_TipoSolicitudCorrectivo
                left join empresas e on s.idcliente = e.id_empresa
            where id_SolicitudCorrectivo not in ( select idSolicitudCorrectivo from SolicitudCorrectivo_relacion)
                order by fechaCreacion desc
            */
            #endregion

            string sSQL = "Proc_getSolicitudCorrectivoes";

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public static DbDataReader GetReader(string idTipoSolicitudCorrectivo, string idResponsable)
        {
            string qry = "Proc_getSolicitudCorrectivoes @idTipo=" + idTipoSolicitudCorrectivo + ", @idResponsable=" + idResponsable;
            Console.WriteLine( qry);

            return ExecuteDbReader(qry);

        }

        public DbDataReader GetAdjuntos()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCorrectivo));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select a.IdAdjunto , FileName, Date, Size from adjunto a inner join SolicitudCorrectivoAdjuntos sa
                            on a.idadjunto = sa.idadjunto
                            where sa.idSolicitudCorrectivo = " + this.Id_SolicitudCorrectivo;
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public DbDataReader GetTareasRendidas()
        {
            #region DefSP
            //create proc proc_getTareasHorasFromSolicitudCorrectivo
            //@idSolicitudCorrectivo int
            //as
            //select th.id, 
            // t.tarea,
            //th.fecha,
            //th.duracion,
            //isnull(th.comentarios,'--') Comentario
            //from SolicitudCorrectivo_tareas st
            //inner join  tareas_horas th
            //inner join  tareas t on th.id_tipotarea = t.id
            //on st.id = th.id_tarea
            //where id_SolicitudCorrectivo = @idSolicitudCorrectivo
            #endregion

            // Expects a root type
            string sSQL = @"proc_getTareasHorasFromSolicitudCorrectivo " + this.Id_SolicitudCorrectivo;
            return ExecuteDbReader(sSQL);

        }

        public DbDataReader GetVehiculosRendidos()
        {
            #region defSP
            /*
             * create proc Proc_getVehiculosRendicion
@idSolicitudCorrectivo int 
as
select vk.idVehiculosKilometros, Fecha, Marca +' '+  patente  vehiculo,
Kilometros,
Tiempo
  from vehiculosKilometros	 vk
inner join SolicitudCorrectivo_recursos_vehiculos sv
on
sv.id = vk.idvehiculoSolicitudCorrectivo
inner join Vehiculos v 
on sv.id_vehiculo = v.id_vehiculos
where sv.Id_SolicitudCorrectivo = @idSolicitudCorrectivo
             * */
#endregion
            string sSQL = @"Proc_getVehiculosRendicion " + this.Id_SolicitudCorrectivo;
            return ExecuteDbReader(sSQL);
        }

        private static DbDataReader ExecuteDbReader(string SSQLQuery)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCorrectivo));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            oConn.CommandText = SSQLQuery;
            return oConn.ExecuteReader();
        }

        public string RelacionadaCon
        {
            get
            {
                SolicitudCorrectivo_Relacion sr = SolicitudCorrectivo_Relacion.FindOne(Expression.Eq("IdSolicitudCorrectivo", this.Id_SolicitudCorrectivo));

                if (sr != null)
                {
                    return sr.IdSolicitudCorrectivo_Relacionada.ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        public static DbDataReader GetMisSolicitudCorrectivoes(string idEmpleado)
        {
            #region SPDEF
            /*
create Proc Prod_GetMisSolicitudCorrectivoes
@idPersona int
as
select 
distinct
s.Id_SolicitudCorrectivo
,FechaCreacion Creado
,p.apellido +', '+ p.nombres Creador
,Fecha_inicio Inicio
,Fecha_fin Fin
,ts.descripcion Tipo
,s.Descripcion Referencia
,'--' Cargada_por
,'--' Responsable
,s.status Estado
 

from dbo.SolicitudCorrectivo_Recursos_Empleados se
inner join dbo.SolicitudCorrectivo_Relacion sr on sr.IdSolicitudCorrectivo = se.id_SolicitudCorrectivo
inner join SolicitudCorrectivo s  on sr.IdSolicitudCorrectivo_relacionada  = s.id_SolicitudCorrectivo
left join SolicitudCorrectivo_tareas st on s.id_SolicitudCorrectivo = st.Id_SolicitudCorrectivo
left join tipo_SolicitudCorrectivo ts on s.idTipoSolicitudCorrectivo = ts.Id_TipoSolicitudCorrectivo 
left join Personal p on s.IdUsuarioCreador = p.id_empleados
where se.id_empleado = @idPersona
    */
            #endregion

            return ExecuteDbReader("Prod_GetMisSolicitudCorrectivoes " + idEmpleado);
        }
    }
}
