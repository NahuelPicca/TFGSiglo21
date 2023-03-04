using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CreateUpdatePedidoDto
    {
        public int? AsignacionId { get; set; }
    //    string Factura { get; set; }
        public DateTime FechaPedido { get; set; }
        //string NroPedido { get; set; }
        public bool Pagado { get; set; }
        public int ReservaId { get; set; }
    }
}
