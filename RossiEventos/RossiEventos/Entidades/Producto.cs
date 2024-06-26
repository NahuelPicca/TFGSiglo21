﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RossiEventos.Interfaces;

namespace RossiEventos.Entidades
{
    public class Producto : Base, IProducto
    {
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string Codigo { get; set; }

        [Required, StringLength(1000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required, StringLength(200), Column(TypeName = "varchar")]
        public string Marca { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Anio { get; set; }

        [Required]
        public bool Habilitado { get; set; }

        [Required]
        public int CalidadId { get; set; }

        [ForeignKey(nameof(CalidadId))]
        public virtual Calidad? Calidad { get; set; }

        [Required]
        public int TipoProductoId { get; set; }

        [ForeignKey(nameof(TipoProductoId))]
        public virtual TipoProducto? Tipo { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required, Column(TypeName = "varchar(MAX)")]
        public string Poster1 { get; set; } = "";

        [Required, Column(TypeName = "varchar(MAX)")]
        public string Poster2 { get; set; } = "";

        [Required, Column(TypeName = "varchar(MAX)")]
        public string Poster3 { get; set; } = "";
    }
}
