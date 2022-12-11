namespace RossiEventos.Entidades
{
    public interface IReserva
    {
        int CantidadPersonas { get; set; }
        string CodigoPostal { get; set; }
        string DireccionEvento { get; set; }
        DateTime FechaEvento { get; set; }
        DateTime FechaReserva { get; set; }
        string LocalidadEvento { get; set; }
        string NroReserva { get; set; }
        string ProvinciaEvento { get; set; }
        Usuario Usuario { get; set; }
        int UsuarioId { get; set; }
    }
}