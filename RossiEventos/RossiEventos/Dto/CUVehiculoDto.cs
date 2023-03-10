using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CUVehiculoDto
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }//Boniato argentino
        public DateTime FechaVencPoliza { get; set; }
        public string NroPoliza { get; set; }
        public int TitularId { get; set; }
        public bool Habilitado { get; set; }
        public string UsuarioInserto { get; set; }
    }
}
