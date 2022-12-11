using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RossiEventos.Interfaces;

namespace RossiEventos.Entidades
{
    public class ComprobanteEntrega : Base, IComprobanteEntrega
    {
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Apellido { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string TipoDni { get; set; }

        [Required]
        public int NroDni { get; set; }
    }
}
