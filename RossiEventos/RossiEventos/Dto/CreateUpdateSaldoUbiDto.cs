using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CreateUpdateSaldoUbiDto
    {
        public int DepositoId { get; set; }
        public int UbicacionId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
