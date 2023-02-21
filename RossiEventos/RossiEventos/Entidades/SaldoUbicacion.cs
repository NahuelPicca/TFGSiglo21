using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class SaldoUbicacion : Base, ISaldoUbicacion
    {
        [Required]
        public int DepositoId { get; set; }

        [ForeignKey(nameof(DepositoId))]
        public Deposito Deposito { get; set; }

        [Required]
        public int UbicacionId { get; set; }

        [ForeignKey(nameof(UbicacionId))]
        public Ubicacion Ubicacion { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
