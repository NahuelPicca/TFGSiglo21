using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class ULocalizacionDto
    {
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Localidad { get; set; }
    }
}
