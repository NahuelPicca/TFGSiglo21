using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class RenglonReserva : Base, IRenglonDeReserva
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

        public decimal PrecioUnit
        {
            get
            {
                return this.PrecioUnit;
            }
            set
            {
                this.PrecioUnit = Producto.Precio;
            }
        }

        public decimal PrecioTotal
        {
            get
            {
                return this.PrecioTotal;
            }
            set
            {
                this.PrecioTotal = Cantidad * PrecioUnit;
            }
        }
    }
}
