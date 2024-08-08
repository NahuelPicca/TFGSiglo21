using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class VehiculoDto : Vehiculo
    {
        public string PatenteModelo => Patente + " " + Modelo;
    }
}
