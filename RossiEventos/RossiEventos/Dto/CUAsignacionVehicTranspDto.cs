namespace RossiEventos.Dto
{
    public class CUAsignacionVehicTranspDto
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public string Modelo { get; set; }
        public string NombreTransportista { get; set; }
        public string ApellidoTransportista { get; set; }
        public string Licencia { get; set; }
        public int TransportitaId { get; set; }
        public int VehiculoId { get; set; }
    }
}
