using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISW_TP_6.Entities
{
    public class Transportista
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public int Calificacion { get; set; }
        public string Vehiculo { get; set; }
        public string Email { get; set; }


        public Transportista() 
        {
            Id = 0;
            NombreCompleto = "";
            TipoDocumento = "";
            NumeroDocumento = 0;
            Calificacion = 1;
            Vehiculo = "";
            Email = "";

        }

        public Transportista(int id, string nombreCompleto, string tipoDocumento, int numeroDocumento, int calificacion, string vehiculo, string email)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Calificacion = calificacion;
            Vehiculo = vehiculo;
            Email = email;

        }
    }
}
