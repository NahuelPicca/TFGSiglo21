namespace RossiEventos.Dto
{
    public class ProductoEditarDto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Poster1 { get; set; }
        public string Poster2 { get; set; }
        public string Poster3 { get; set;}
        public DateTime Anio { get; set;}
        public bool Habilitado { get; set; }
        public int CalidadId { get; set; }
        public string CodigoCalidad { get; set; }
        public int TipoProductoId { get; set; }
        public decimal Precio { get; set; }
    }
}
