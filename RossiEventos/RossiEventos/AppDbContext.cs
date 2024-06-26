﻿using RossiEventos.Entidades;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RossiEventos
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //INICIO Creación de Índices
            //Creación de índices vía fluent api
            //Persona
            modelBuilder.Entity<Persona>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Persona>()
                        .HasIndex(u => u.Cuit)
                        .IsUnique();

            //Vehiculos
            modelBuilder.Entity<Vehiculo>()
                        .HasIndex(u => u.Patente)
                        .IsUnique();

            modelBuilder.Entity<Vehiculo>()
                        .HasIndex(u => u.NroPoliza)
                        .IsUnique();
            //Calidad
            modelBuilder.Entity<Calidad>()
                       .HasIndex(u => u.Codigo)
                       .IsUnique();
            //Producto
            modelBuilder.Entity<Producto>()
                       .HasIndex(u => u.Codigo)
                       .IsUnique();

            //Reserva
            modelBuilder.Entity<Reserva>()
                       .HasIndex(u => new { u.NroReserva, u.UsuarioId })
                       .IsUnique();

            modelBuilder.Entity<Pedido>()
                      .HasIndex(p => p.NroPedido)
                      .IsUnique();

            modelBuilder.Entity<Pedido>()
                      .HasIndex(p => p.Factura)
                      .IsUnique();

            //Encabezado Mov Stk.
            modelBuilder.Entity<EncabezadoMovStk>()
                        .HasIndex(u => new { u.TipoMovimiento, u.NroComprobante })
                        .IsUnique();

            //Deposito
            modelBuilder.Entity<Deposito>()
                       .HasIndex(d => d.Codigo)
                       .IsUnique();

            modelBuilder.Entity<Ubicacion>()
                       .HasIndex(d => d.Codigo)
                       .IsUnique();

            //FIN Creación de Índices

            //INICIO ForeingKeys
            //Estos son los casos de muchos a muchos.
            modelBuilder.Entity<AsignacionVehicTransp>()
                        .HasOne(pt => pt.Vehiculo)
                        .WithMany(p => p.AsignacionVehicTrans)
                        .HasForeignKey(pt => pt.VehiculoId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AsignacionVehicTransp>()
                        .HasOne(pt => pt.Transportista)
                        .WithMany(t => t.AsignacionVehicTrans)
                        .HasForeignKey(pt => pt.TransportitaId)
                        .OnDelete(DeleteBehavior.Restrict);
            //FIN ForeingKeys

            // INI - Detemina la cantidad de decimales de los campos
            // de dicho objeto. 
            modelBuilder.Entity<Producto>()
                       .Property(o => o.Precio)
                       .HasPrecision(15, 2);

            modelBuilder.Entity<RenglonReserva>()
                        .Property(o => o.PrecioTotal)
                        .HasPrecision(15, 2);
            modelBuilder.Entity<RenglonReserva>()
                        .Property(o => o.PrecioUnit)
                        .HasPrecision(15, 2);
            //FIN

            //INI campos calculados
            modelBuilder.Entity<RenglonReserva>()
                        .Property(u => u.PrecioTotal)
                        .HasComputedColumnSql("Cantidad*PrecioUnit");
            //FIN campos calculados


            //INI campos que aceptan NULL
            modelBuilder.Entity<Pedido>()
                        .Property(b => b.AsignacionId)
                        .IsRequired(false);

            modelBuilder.Entity<SeguimientoPedido>()
                        .Property(b => b.ComproEntreId)
                        .IsRequired(false);

            //modelBuilder.Entity<SeguimientoPedido>()
            //        .Property(b => b.Localizaciones)
            //        .IsRequired(false);
            //INI campos que aceptan NULL
        }
        public DbSet<AsignacionVehicTransp> AsignacionVehicTransp { get; set; }
        public DbSet<Calidad> Calidad { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Transportista> Transportista { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Titular> Titular { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ComprobanteEntrega> ComprobanteEntrega { get; set; }
        public DbSet<SeguimientoPedido> SeguimientoPedido { get; set; }
        public DbSet<EncabezadoMovStk> EncabezadoMovStk { get; set; }
        public DbSet<Deposito> Deposito { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<SaldoUbicacion> SaldoUbicacion { get; set; }
        public DbSet<RenglonMovStk> RenglonMov { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<Localizacion> Localizacion { get; set; }
    }
}
