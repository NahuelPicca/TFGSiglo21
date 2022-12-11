namespace RossiEventos.Interfaces
{
    public interface IComprobanteEntrega
    {
        string Apellido { get; set; }
        string Nombre { get; set; }
        int NroDni { get; set; }
        string TipoDni { get; set; }
    }
}