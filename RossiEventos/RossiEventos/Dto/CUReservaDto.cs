using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CUReservaDto
    {
        public DateTime FechaReserva { get; set; }     
        public string NroReserva { get; set; }// Ejemplo A20/A21/A22
        public DateTime FechaEvento { get; set; }
        public int CantidadPersonas { get; set; }
        public string DireccionEvento { get; set; }
        public string LocalidadEvento { get; set; }
        public string ProvinciaEvento { get; set; }
        public string CodigoPostal { get; set; }
        public int UsuarioId { get; set; }
        public ICollection<CURenglonDeReservaDto> Renglones { get; set; }
             = new HashSet<CURenglonDeReservaDto>();
    }
}
