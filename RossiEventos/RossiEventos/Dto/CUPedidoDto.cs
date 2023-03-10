using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CUPedidoDto
    {
        public int? AsignacionId { get; set; }
    //    string Factura { get; set; }
        public DateTime FechaPedido { get; set; }
        //string NroPedido { get; set; }
        public bool Pagado { get; set; }
        public int ReservaId { get; set; }
    }
}
