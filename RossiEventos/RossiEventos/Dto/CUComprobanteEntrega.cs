using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CUComprobanteEntrega
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDni { get; set; }
        public int NroDni { get; set; }
    }
}
