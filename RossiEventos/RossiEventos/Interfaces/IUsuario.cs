using RossiEventos.Entidades;

public interface IUsuario
{
    string Contraseña { get; set; }
    DateTime? FechaBaja { get; set; }
    TipoUsuario Tipo { get; set; }
}