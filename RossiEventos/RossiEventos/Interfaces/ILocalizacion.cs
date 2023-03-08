using RossiEventos.Entidades;

namespace RossiEventos.Interfaces
{
    public interface ILocalizacion
    {
        DateTime Fecha { get; set; }
        Estado Estado { get; set; }
        string Descripcion { get; set; }
        string Localidad { get; set; }
        SeguimientoPedido Seguimiento { get; set; }
        int SeguimientoId { get; set; }
    }
}
