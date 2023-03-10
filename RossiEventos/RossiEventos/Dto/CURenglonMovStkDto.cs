using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CURenglonMovStkDto
    {
        public int MovimientoId { get; set; }
    //   public EncabezadoMovStk Encabezado { get; set; }
        public int SaldoUbiId { get; set; }
        SaldoUbicacion SaldoUbi { get; set; }
        public int ProductoId { get; set; }
        Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
