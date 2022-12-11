namespace RossiEventos.Entidades
{
    public interface IEncabezadoMovStk
    {
        string ComprobanteRelacionado { get; set; }
        string Descripcion { get; set; }
        DateTime FechaMovimiento { get; set; }
        string NroComprobante { get; set; }
        TipoComprobante TipoMovimiento { get; set; }
    }
}