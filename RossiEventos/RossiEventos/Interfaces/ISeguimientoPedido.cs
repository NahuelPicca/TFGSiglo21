namespace RossiEventos.Entidades
{
    public interface ISeguimientoPedido
    {
        ComprobanteEntrega ComprobanteEntre { get; set; }
        int ComproEntreId { get; set; }
        string Descripcion { get; set; }
        DateTime FechaEntrega { get; set; }
        DateTime FechaInicio { get; set; }
        Pedido Pedido { get; set; }
        int PedidoId { get; set; }
        string Ubicacion { get; set; }
    }
}