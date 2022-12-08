using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

[Table("Usuario")]
public class Usuario : Persona
{
    [Required]
    public TipoUsuario Tipo { get; set; }

    [Required, StringLength(20), Column(TypeName = "varchar")]
    [PasswordPropertyText]
    public string Contraseña { get; set; }

    public DateTime? FechaBaja { get; set; }
}