using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class AsignacionVehicTransp : Base
    {
        public int TransportitaId { get; set; }

        //[ForeignKey(nameof(TransportitaId))]
        //public virtual Transportista Transportista { get; set; }

        public int VehiculoId { get; set; }

        [ForeignKey(nameof(VehiculoId))]
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
