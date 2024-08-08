using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class TransportistaDto : Transportista
    {
        public string ApellidoNombreLicencia => Apellido + ", " + Nombre + " - " + Licencia;
    }
}
