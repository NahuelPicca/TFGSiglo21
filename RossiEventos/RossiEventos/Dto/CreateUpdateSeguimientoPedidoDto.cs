using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CreateUpdateSeguimientoPedidoDto
    {
        public int? ComproEntreId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaInicio { get; set; }
        public int PedidoId { get; set; }
        public ICollection<CreateUpdateLocalizacionesDto> Localizaciones { get; set; }
            = new HashSet<CreateUpdateLocalizacionesDto>();
    }
}
