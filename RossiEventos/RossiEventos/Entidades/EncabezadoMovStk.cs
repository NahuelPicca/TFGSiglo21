using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossiEventos.Entidades
{
    public class EncabezadoMovStk : Base
    {
        [Required]
        public DateTime FechaMovimiento { get; set; }

        [Required]
        public TipoComprobante TipoMovimiento { get; set; }

        [Required, StringLength(15), Column(TypeName = "varchar")]
        public string NroComprobante { get; set; }

        [Required, StringLength(5000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string ComprobanteRelacionado { get; set; }
    }
}
