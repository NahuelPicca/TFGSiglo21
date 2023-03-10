using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CUSeguimientoPedidoDto
    {
        public int? ComproEntreId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaInicio { get; set; }
        public int PedidoId { get; set; }
        public ICollection<CULocalizacionesDto> Localizaciones { get; set; }
            = new HashSet<CULocalizacionesDto>();
    }
}
