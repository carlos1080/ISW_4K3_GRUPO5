using System;
using System.Collections.Generic;

namespace TP6_TANGOAPP_RES_G5_2024.Entities
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
