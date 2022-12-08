using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    [Table("Titular")]
    public class Titular : Persona
    {
        public ICollection<Vehiculo> Vehiculos { get; set; } = new HashSet<Vehiculo>();
    }
}
