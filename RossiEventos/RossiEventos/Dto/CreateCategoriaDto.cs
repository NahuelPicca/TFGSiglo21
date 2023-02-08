using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class CreateCategoriaDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioInserto { get; set; }
    }
}
