using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class Deposito : Base, IDeposito
    {
        [Required, StringLength(4), Column(TypeName = "varchar")]
        public string Codigo { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Direccion { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Localidad { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Provincia { get; set; }

        [Required]
        public bool Habilitado { get; set; }
    }
}
