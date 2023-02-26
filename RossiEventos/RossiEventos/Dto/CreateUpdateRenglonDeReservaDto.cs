using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CreateUpdateRenglonDeReservaDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        //public decimal CostoUnitario { get; set; }
        //public decimal CostoTotal { get; set; }
    }
}
