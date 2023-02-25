using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class Reserva : Base, IReserva
    {
        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario Usuario { get; set; }

        public DateTime FechaReserva { get; set; }

        [Required, StringLength(15), Column(TypeName = "varchar")]
        public string NroReserva { get; set; }// Ejemplo A20/A21/A22

        [Required]
        public DateTime FechaEvento { get; set; }

        [Required]
        public int CantidadPersonas { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string DireccionEvento { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string LocalidadEvento { get; set; }

        [Required, StringLength(30), Column(TypeName = "varchar")]
        public string ProvinciaEvento { get; set; }

        [Required, StringLength(5), Column(TypeName = "varchar")]
        public string CodigoPostal { get; set; }

        public ICollection<RenglonReserva> Renglones { get; set; } = new HashSet<RenglonReserva>();
    }
}
