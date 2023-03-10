using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CUEncabezadoMovStkDto : IEncabezadoMovStk
    {
        public string ComprobanteRelacionado { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string NroComprobante { get; set; }
        public TipoComprobante TipoMovimiento { get; set; }
        public ICollection<CURenglonMovStkDto> Renglones { get; set; }
                     = new HashSet<CURenglonMovStkDto>();
    }
}
