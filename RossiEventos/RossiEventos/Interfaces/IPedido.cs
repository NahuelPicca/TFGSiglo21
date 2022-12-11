namespace RossiEventos.Entidades
{
    public interface IPedido
    {
        AsignacionVehicTransp Asignacion { get; set; }
        int AsignacionId { get; set; }
        string Factura { get; set; }
        DateTime FechaPedido { get; set; }
        string NroPedido { get; set; }
        bool Pagado { get; set; }
        Reserva Reserva { get; set; }
        int ReservaId { get; set; }
    }
}