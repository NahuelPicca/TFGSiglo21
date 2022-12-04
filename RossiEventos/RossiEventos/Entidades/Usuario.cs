using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class Usuario : Base, IPersona
    {
        [Required, StringLength(100), Column(TypeName ="varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Apellido { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string TipoDni { get; set; }
        
        [Required]
        public int NroDni { get; set; }

        [Required, StringLength(13), Column(TypeName = "varchar")]
        public string Cuit { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Direccion { get; set; }
        
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Localidad { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Required, StringLength(5), Column(TypeName = "varchar")]
        public string CodigoPostal { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
