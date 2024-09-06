using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISW_TP_6.Entities
{
    public class PedidoEnvio
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaLimite { get; set; }
        public string UbicacionRetiro { get; set; }
        public string UbicacionLlegada { get; set; }
        public string Estado { get; set; }
        public List<Cotizacion> Cotizaciones { get; set; }
        public int? NumeroPago { get; set; }
        public PedidoEnvio() 
        {
            Id = 0;
            UbicacionRetiro = "";
            UbicacionLlegada = "";
            FechaInicio = DateTime.Now;
            FechaLimite = DateTime.Now;
            Estado = "";
            Cotizaciones = new();
            NumeroPago = null;
        }

        public PedidoEnvio(int id, string ubicacionRetiro, string ubicacionLlegada, DateTime fechaInicio, DateTime fechaLimite, string estado, List<Cotizacion> cotizaciones, int? numeroPago)
        {
            Id = id;
            UbicacionRetiro = ubicacionRetiro;
            UbicacionLlegada = ubicacionLlegada;
            FechaInicio = fechaInicio;
            FechaLimite = fechaLimite;
            Estado = estado;
            Cotizaciones = cotizaciones;
            NumeroPago = numeroPago;
        }

    }
}
