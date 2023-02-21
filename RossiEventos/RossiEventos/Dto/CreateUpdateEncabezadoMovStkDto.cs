using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CreateUpdateEncabezadoMovStkDto : IEncabezadoMovStk
    {
        public string ComprobanteRelacionado { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string NroComprobante { get; set; }
        public TipoComprobante TipoMovimiento { get; set; }
        public ICollection<CreateUpdateRenglonMovStkDto> Renglones { get; set; }
                     = new HashSet<CreateUpdateRenglonMovStkDto>();
    }
}
