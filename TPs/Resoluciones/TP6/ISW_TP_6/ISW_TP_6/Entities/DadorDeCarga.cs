using System;
using System.Collections.Generic;

namespace ISW_TP_6.Entities
{
    public class DadorDeCarga
    {
        public string NombreCompleto { get; set; }
        public List<Tarjeta> TarjetasDisponibles { get; set; }
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public string Email { get; set; }
        public List<PedidoEnvio> PedidosPresentados { get; set; }

        public DadorDeCarga()
        {
            NombreCompleto = "";
            TarjetasDisponibles = new();
            TipoDocumento = "";
            NumeroDocumento = 0;
            Email = "";
            PedidosPresentados = new();
        }

        public DadorDeCarga(string nombreCompleto, List<Tarjeta> tarjetasDisponibles, string tipoDocumento, int numeroDocumento, string email, List<PedidoEnvio> pedidosPresentados)
        {
            NombreCompleto = nombreCompleto;
            TarjetasDisponibles = tarjetasDisponibles;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Email = email;
            PedidosPresentados = pedidosPresentados;
        }
    }
}
