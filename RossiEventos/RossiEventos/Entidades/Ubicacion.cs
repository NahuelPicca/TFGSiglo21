using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Ubicacion : Base, IUbicacion
    {
        [Required, StringLength(4), Column(TypeName = "varchar")]
        public string Codigo { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Estante { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Columna { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Fila { get; set; }

        [Required]
        public bool Habilitado { get; set; }
    }
}
