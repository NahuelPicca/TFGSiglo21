using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    [Table("Transportista")]
    public class Transportista2 : Persona
    {
        [Required]
        public DateTime FechaVencLicencia { get; set; }

        [Required, StringLength(50), Column(TypeName = "varchar")]
        public string Licencia { get; set; }

        public bool Habilitado { get; set; }

        //public ICollection<AsignacionVehicTransp> AsignacionVehicTrans { get; set; }
        //    = new HashSet<AsignacionVehicTransp>();
    }
}
