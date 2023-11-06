using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class Empleado
    {
        public long cuil { get; set; }
        public string nombre_completo { get; set; }
        public string pin { get; set; }


        public Empleado() { }

        public Empleado(int CUIL, string Nombre_completo, string PIN)
        {
            cuil = CUIL;
            nombre_completo = Nombre_completo;
            pin = PIN;

        }

    }
}