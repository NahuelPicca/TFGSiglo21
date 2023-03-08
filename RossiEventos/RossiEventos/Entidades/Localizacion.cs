using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Localizacion : Base, ILocalizacion
    {
        public Estado Estado { get; set; }
        
        public DateTime Fecha { get; set; }
        
        [Required, StringLength(5000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }
        
        [Required, StringLength(300), Column(TypeName = "varchar")]
        public string Localidad { get; set; }

        [Required]
        public int SeguimientoId { get; set; }

        [ForeignKey(nameof(SeguimientoId))]
        public SeguimientoPedido Seguimiento { get; set; }
        
    }
}
