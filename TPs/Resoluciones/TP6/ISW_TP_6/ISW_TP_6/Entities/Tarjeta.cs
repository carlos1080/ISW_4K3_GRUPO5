using System;
using System.Collections.Generic;
namespace ISW_TP_6.Entities
{
    public class Tarjeta
    {
        public int Numero { get; set; }
        public int Pin { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public Tarjeta() 
        {
            Numero = 0;
            Pin = 0;
            NombreCompleto = "";
            TipoDocumento = "";
            NumeroDocumento = 0;
        }

        public Tarjeta(int numero, int pin, string nombreCompleto, string tipoDocumento, int numeroDocumento)
        {
            Numero = numero;
            Pin = pin;
            NombreCompleto = nombreCompleto;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
        }
    }
}
