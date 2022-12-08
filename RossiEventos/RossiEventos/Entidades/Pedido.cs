using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class Pedido : Base
    {
        [Required]
        public int AsignacionId { get; set; }

        [ForeignKey(nameof(AsignacionId))]
        public AsignacionVehicTransp Asignacion { get; set; }

        [Required]
        public int ReservaId { get; set; }

        [ForeignKey(nameof(ReservaId))]
        public Reserva Reserva { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; }

        [Required, StringLength(13), Column(TypeName = "varchar")]
        public string NroPedido { get; set; }

        [Required, StringLength(13), Column(TypeName = "varchar")]
        public string Factura { get; set; }

        [Required]
        public bool Pagado { get; set; }
    }
}
