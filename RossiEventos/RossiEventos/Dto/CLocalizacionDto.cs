using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CLocalizacionDto
    {
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Localidad { get; set; }
        public int SeguimientoId { get; set; }
    }
}
