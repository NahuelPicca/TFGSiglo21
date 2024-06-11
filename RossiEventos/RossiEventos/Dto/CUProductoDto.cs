namespace RossiEventos.Dto
{
    public class CUProductoDto 
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public DateTime Anio { get; set; }
        public bool Habilitado { get; set; }
        public decimal Precio { get; set; }
        public int CalidadId { get; set; }
        public int TipoProductoId { get; set; }
        public IFormFile? Poster1 { get; set; }
        public IFormFile? Poster2 { get; set; }
        public IFormFile? Poster3 { get; set; }
    }
}
