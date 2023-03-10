using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CURenglonDeReservaDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int Id { get; set; }
    }
}
