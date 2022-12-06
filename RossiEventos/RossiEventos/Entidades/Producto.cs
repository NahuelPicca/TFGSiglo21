using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RossiEventos.Entidades
{
    public class Producto : Base
    {
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string Codigo { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Marca { get; set; }

        [Required]
        public DateTime Año { get; set; }

        [Required]
        public bool Habilitado { get; set; }

        [Required]
        public int CalidadId { get; set; }

        [ForeignKey(nameof(CalidadId))]
        public virtual Calidad Calidad { get; set; }
    }
}
