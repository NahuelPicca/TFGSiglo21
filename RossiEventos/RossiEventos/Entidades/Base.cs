using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RossiEventos.Interfaces;

namespace RossiEventos.Entidades
{
    public abstract class Base : IBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaInsercion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = new DateTime(1990, 01, 01);
        
        [Column(TypeName ="varchar(100)")]
        public string UsuarioInserto { get; set; } = "SYSTEM";

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
