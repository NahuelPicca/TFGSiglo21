using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CUUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDni { get; set; }
        public string Cuit { get; set; }
        public int NroDni { get; set; }
        public string CodigoPostal { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public TipoUsuario Tipo { get; set; }
        public string Contraseña { get; set; }
    }
}
