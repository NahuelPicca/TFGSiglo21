using RossiEventos.Entidades;

namespace RossiEventos.Interfaces
{
    public interface IVehiculo
    {
        DateTime FechaVencPoliza { get; set; }
        bool Habilitado { get; set; }
        string Marca { get; set; }
        string Modelo { get; set; }
        string NroPoliza { get; set; }
        string Patente { get; set; }
        Titular Titular { get; set; }
    }
}