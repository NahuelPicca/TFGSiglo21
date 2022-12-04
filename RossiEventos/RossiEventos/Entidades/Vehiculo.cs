using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RossiEventos.Interfaces;

namespace RossiEventos.Entidades
{
    public class Vehiculo : Base, IVehiculo
    {
        [Required, StringLength(7), Column(TypeName = "varchar")]
        public string Patente { get; set; }

        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string Marca { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Modelo { get; set; }

        [Required]
        public DateTime FechaVencPoliza { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string NroPoliza { get; set; }

        public Titular Titular { get; set; }

        [Required]
        public bool Habilitado { get; set; }
    }
}
