namespace RossiEventos.Entidades
{
    public interface IUbicacion
    {
        string Codigo { get; set; }
        string Columna { get; set; }
        string Estante { get; set; }
        string Fila { get; set; }
        string Habilitado { get; set; }
        string Nombre { get; set; }
    }
}