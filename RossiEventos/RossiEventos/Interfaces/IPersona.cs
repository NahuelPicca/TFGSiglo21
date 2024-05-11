using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Interfaces
{
    public interface IPersona
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        DateTime FechaNacimiento { get; set; }
        int NroDni { get; set; }
        string Cuit { get; set; }
        string Direccion { get; set; }
        string Telefono { get; set; }
        string Localidad { get; set; }
        string Email { get; set; }
        string CodigoPostal { get; set; }
    }
}
