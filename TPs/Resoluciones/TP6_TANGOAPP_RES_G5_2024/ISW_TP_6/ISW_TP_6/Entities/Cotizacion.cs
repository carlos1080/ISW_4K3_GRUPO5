using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISW_TP_6.Entities
{
    public class Cotizacion
    {
        public int Id { get; set; }
        public Transportista Transp { get; set; }
        public DateTime FechaRetiro { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double ImporteEnvio { get; set; }
        public List<FormasDePago> FormasHabilitadas { get; set; }

        public Cotizacion()
        {
            Id = 0;
            Transp = new();
            FechaRetiro = DateTime.Now;
            FechaEntrega = DateTime.Now;
            ImporteEnvio = 0;
            FormasHabilitadas = new();
        }

        public Cotizacion(int id,Transportista trans, DateTime fechaRetiro, DateTime fechaEntrega, double importeEnvio, List<FormasDePago> formasHabilitadas)
        {
            Id = id;
            Transp = trans;
            FechaRetiro = fechaRetiro;
            FechaEntrega = fechaEntrega;
            ImporteEnvio = importeEnvio;
            FormasHabilitadas = formasHabilitadas;
        }
    }
}
