using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class RenglonMovStk : Base, IRenglonMovStk
    {
        [Required]
        public int MovimientoId { get; set; }

        [ForeignKey(nameof(MovimientoId))]
        public EncabezadoMovStk Encabezado { get; set; }

        [Required]
        public int SaldoUbiId { get; set; }

        [ForeignKey(nameof(SaldoUbiId))]
        public SaldoUbicacion Saldo { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
