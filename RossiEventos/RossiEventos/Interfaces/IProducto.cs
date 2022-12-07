using RossiEventos.Entidades;

namespace RossiEventos.Interfaces
{
    public interface IProducto
    {
        DateTime Año { get; set; }
        Calidad Calidad { get; set; }
        int CalidadId { get; set; }
        string Codigo { get; set; }
        string Descripcion { get; set; }
        bool Habilitado { get; set; }
        string Marca { get; set; }
        TipoProducto Tipo { get; set; }
        int TipoId { get; set; }
    }
}