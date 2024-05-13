using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using RossiEventos.Dto;

[Table("Usuario")]
public class Usuario : Persona, IUsuario
{
    [Required]
    public TipoUsuario Tipo { get; set; }

    [Required, Column(TypeName = "varchar(MAX)")]
    [PasswordPropertyText]
    public string Contraseña { get; set; } 

    public DateTime? FechaBaja { get; set; }
}