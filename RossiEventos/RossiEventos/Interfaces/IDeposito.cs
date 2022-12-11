namespace RossiEventos.Entidades
{
    public interface IDeposito
    {
        string Codigo { get; set; }
        string Descripcion { get; set; }
        string Direccion { get; set; }
        bool Habilitado { get; set; }
        string Localidad { get; set; }
        string Provincia { get; set; }
    }
}