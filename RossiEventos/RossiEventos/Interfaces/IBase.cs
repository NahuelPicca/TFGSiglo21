﻿namespace RossiEventos.Interfaces
{
    public interface IBase
    {
        public int Id { get; set; }
        public DateTime FechaInsercion { get; set; }

        public DateTime FechaModificacion { get; set; }
        public string UsuarioInserto { get; set; }//Nombre del usuario
    }
}
