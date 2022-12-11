namespace RossiEventos.Entidades
{
    public interface IRenglonMovStk
    {
        int Cantidad { get; set; }
        EncabezadoMovStk Encabezado { get; set; }
        int MovimientoId { get; set; }
        Producto Producto { get; set; }
        int ProductoId { get; set; }
        SaldoUbicacion SaldoUbi { get; set; }
        int SaldoUbiId { get; set; }
    }
}