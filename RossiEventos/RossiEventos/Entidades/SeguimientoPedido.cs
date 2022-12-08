﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Entidades
{
    public class SeguimientoPedido : Base
    {
        [Required]
        public int PedidoId { get; set; }

        [ForeignKey(nameof(PedidoId))]
        public Pedido Pedido { get; set; }

        [Required]
        public int ComproEntreId { get; set; }

        [ForeignKey(nameof(ComproEntreId))]
        public ComprobanteEntrega ComprobanteEntre { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaEntrega { get; set; }

        [Required, StringLength(5000), Column(TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Required, StringLength(10), Column(TypeName = "varchar")]
        public string Ubicacion { get; set; }//Nombre de la localidad o coordenadas-->Capaz que hay que agregar nuevos campos
    }
}
