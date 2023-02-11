using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CreateUpdateTransportistaDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDni { get; set; }
        public int NroDni { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Localidad { get; set; }
        public string Email { get; set; }
        public string CodigoPostal { get; set; }
        public string UsuarioInserto { get; set; }
        public DateTime FechaVencLicencia { get; set; }
        public string Licencia { get; set; }
        public bool Habilitado { get; set; }
    }
}
