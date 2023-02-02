﻿namespace RossiEventos.Dto
{
    public class CreateProductoDto
    {
        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Marca { get; set; }

        public DateTime Año { get; set; }

        public bool Habilitado { get; set; }

        public int CalidadId { get; set; }

        public int TipoId { get; set; }
    }
}
