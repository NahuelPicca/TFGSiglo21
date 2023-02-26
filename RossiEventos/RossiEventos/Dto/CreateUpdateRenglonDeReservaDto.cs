using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CreateUpdateRenglonDeReservaDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public int RenglonReservaId { get; set; }
    }
}
