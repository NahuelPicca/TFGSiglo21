namespace RossiEventos.Dto
{
    public class CreateTipoDeProductoDto
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public string UsuarioInserto { get; set; }
    }
}
