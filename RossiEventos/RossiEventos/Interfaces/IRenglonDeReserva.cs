namespace RossiEventos.Entidades
{
    public interface IRenglonDeReserva
    {
        int Cantidad { get; set; }
        decimal PrecioUnit { get; set; }
    //    decimal PrecioTotal { get; private set; }
        Producto Producto { get; set; }
        int ProductoId { get; set; }
        Reserva Reserva { get; set; }
        int ReservaId { get; set; }
    }
}