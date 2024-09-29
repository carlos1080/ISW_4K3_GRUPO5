using System;
using System.Collections.Generic;
namespace TP6_TANGOAPP_RES_G5_2024.Entities
{
    public class Tarjeta
    {
        public string Numero { get; set; }
        public int Pin { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public double Saldo { get; set; }
        public string Tipo { get; set; }

        public Tarjeta() 
        {
            Numero = "0";
            Pin = 0;
            NombreCompleto = "";
            TipoDocumento = "";
            NumeroDocumento = 0;
            Saldo = 0;
            Tipo = "";
        }

        public Tarjeta(string numero, int pin, string nombreCompleto, string tipoDocumento, int numeroDocumento,double saldo,string tipo)
        {
            Numero = numero;
            Pin = pin;
            NombreCompleto = nombreCompleto;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Saldo = saldo;
            Tipo = tipo;
        }
    }
}
