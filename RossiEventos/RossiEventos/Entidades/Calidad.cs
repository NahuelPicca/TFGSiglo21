using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Calidad : Base, ICalidad
    {
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string Codigo { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }
    }
}
