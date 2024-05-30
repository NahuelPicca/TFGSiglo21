using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CUDepositoDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public bool Habilitado { get; set; }
    }
}
