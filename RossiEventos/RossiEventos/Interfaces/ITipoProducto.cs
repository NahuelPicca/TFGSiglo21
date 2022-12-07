using RossiEventos.Entidades;

namespace RossiEventos.Interfaces
{
    public interface ITipoProducto
    {
        Categoria Categoria { get; set; }
        int CategoriaId { get; set; }
        string Descripcion { get; set; }
        string Nombre { get; set; }
    }
}