using System;
using System.Collections.Generic;
using System.Text;
using Castle.ActiveRecord;

namespace Gos.Events
{
    public partial class Events : ActiveRecordBase<Events>
    {

        public static void Registrer(string pModulo, string pDetalle, string pUsuario)
        {
            Gos.Events.Events e = new Events();
            e.Detalle = pDetalle;
            e.Usuario = pUsuario;
            e.Modulo = pModulo;
            e.Fecha = System.DateTime.Now;
   
            e.Save();
        }
    }
}
