namespace RossiEventos.Entidades
{
    public interface ITransportista
    {
        ICollection<AsignacionVehicTransp> AsignacionVehicTrans { get; set; }
        DateTime FechaVencLicencia { get; set; }
        bool Habilitado { get; set; }
        string Licencia { get; set; }
    }
}