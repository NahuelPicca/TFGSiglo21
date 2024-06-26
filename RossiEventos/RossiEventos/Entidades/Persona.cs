﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RossiEventos.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    [Table("Persona")]
    public class Persona : Base, IPersona
    {
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Nombre { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required, StringLength(9), Column(TypeName = "varchar")]
        public string NroDni { get; set; }

        [Required, StringLength(13), Column(TypeName = "varchar")]
        public string Cuit { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Direccion { get; set; }

        [Phone]
        [Required, StringLength(25), Column(TypeName = "varchar")]
        public string Telefono { get; set; }

        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Localidad { get; set; }

        [EmailAddress]
        [Required, StringLength(100), Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Required, StringLength(5), Column(TypeName = "varchar")]
        public string CodigoPostal { get; set; }
    }
}