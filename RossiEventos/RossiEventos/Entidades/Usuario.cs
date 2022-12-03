using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Usuario : Base, IPersona
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(100)]
        public string Apellido { get; set; }

        [Required, StringLength(10)]
        public string TipoDni { get; set; }
        
        [Required]
        public int NroDni { get; set; }

        [Required, StringLength(13)]
        public string Cuit { get; set; }

        [Required, StringLength(100)]
        public string Direccion { get; set; }
        
        [Required, StringLength(100)]
        public string Localidad { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(5)]
        public string CodigoPostal { get; set; }
    }
}
