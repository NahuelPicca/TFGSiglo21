using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Cuit { get; set; }

        [Required]
        public string NroDni { get; set; }

        [Required]
        public string CodigoPostal { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Localidad { get; set; }
        [Required]
        public string Telefono { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
