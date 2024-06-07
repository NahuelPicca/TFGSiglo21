using RossiEventos.Entidades;

namespace RossiEventos.Interfaces
{
    public interface IProducto
    {
        DateTime Anio { get; set; }
        Calidad Calidad { get; set; }
        int CalidadId { get; set; }
        string Codigo { get; set; }
        string Descripcion { get; set; }
        bool Habilitado { get; set; }
        string Marca { get; set; }
        TipoProducto Tipo { get; set; }
        int TipoProductoId { get; set; }
        decimal Precio { get; set; }
    }
}