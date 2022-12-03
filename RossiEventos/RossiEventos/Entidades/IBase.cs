namespace RossiEventos.Entidades
{
    public interface IBase
    {
        public int Id { get; set; }
        public DateTime FechaInsercion { get; set; }

        public DateTime FechaModificacion { get; set; }
        public string NombreUsuario { get; set; }//Nombre del usuario
    }
}
