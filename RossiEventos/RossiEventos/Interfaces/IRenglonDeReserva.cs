namespace RossiEventos.Entidades
{
    public interface IRenglonDeReserva
    {
        int Cantidad { get; set; }
        decimal CostoTotal { get; set; }
        decimal CostoUnitario { get; set; }
        Producto Producto { get; set; }
        int ProductoId { get; set; }
        Reserva Reserva { get; set; }
        int ReservaId { get; set; }
    }
}