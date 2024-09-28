using System;
using System.Collections.Generic;

namespace ISW_TP_6.Entities
{
    public class FormasDePago
    {
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public FormasDePago(string nombre, bool activo)
        {
            Nombre = nombre;
            Activo = activo;
        }
    }
}
