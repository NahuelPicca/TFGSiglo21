namespace RossiEventos.Entidades
{
    public interface ISaldoUbicacion
    {
        Deposito Deposito { get; set; }
        int DepositoId { get; set; }
        Producto Producto { get; set; }
        int ProductoId { get; set; }
        Ubicacion Ubicacion { get; set; }
        int UbicacionId { get; set; }
    }
}