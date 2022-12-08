using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class RenglonDeReserva : Base
    {
        [Required]
        public int ProductoId { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [Required]
        public int ReservaId { get; set; }

        [ForeignKey(nameof(ReservaId))]
        public Reserva Reserva { get; set; }
        public int Cantidad { get; set; }

        [Required]
        public decimal CostoUnitario { get; set; }

        [Required]
        public decimal CostoTotal { get; set; }
    }
}
