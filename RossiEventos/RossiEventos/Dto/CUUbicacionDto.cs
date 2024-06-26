﻿using RossiEventos.Entidades;

namespace RossiEventos.Dto
{
    public class CUUbicacionDto : IUbicacion
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Columna { get; set; }
        public string Estante { get; set; }
        public string Fila { get; set; }
        public bool Habilitado { get; set; }
    }
}
