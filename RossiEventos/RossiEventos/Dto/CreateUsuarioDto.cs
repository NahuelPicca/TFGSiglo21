﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RossiEventos.Dto
{
    public class CreateUsuarioDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDni { get; set; }
        public string Cuit { get; set; }
        public int NroDni { get; set; }
        public string CodigoPostal { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public TipoUsuario Tipo { get; set; }
        public string Contraseña { get; set; }
    }
}
