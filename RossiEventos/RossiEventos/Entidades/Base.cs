using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Base : IBase
    {
        //[Key]
        public int Id { get; set; }
        public DateTime FechaInsercion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = new DateTime(1990, 01, 01);
        public string NombreUsuario { get; set; } = "";
    }
}
