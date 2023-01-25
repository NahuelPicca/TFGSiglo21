using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RossiEventos.Interfaces;

namespace RossiEventos.Entidades
{
    public class TipoProducto : Base, ITipoProducto
    {
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

       // [Required]
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria Categoria { get; set; }
    }
}
