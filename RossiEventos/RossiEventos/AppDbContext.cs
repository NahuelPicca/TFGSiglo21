using RossiEventos.Entidades;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RossiEventos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                       .HasIndex(u => new { u.NroReserva, u.UsuarioId})
                       .IsUnique();

            modelBuilder.Entity<Pedido>()
                      .HasIndex(p => p.NroPedido)
                      .IsUnique();

            modelBuilder.Entity<Pedido>()
                      .HasIndex(p=>p.Factura)
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

            // Detemina la cantidad de decimales de los campos
            // de dicho objeto. 
            modelBuilder.Entity<RenglonDeReserva>()
                       .Property(o => o.CostoUnitario)
                       .HasPrecision(15, 2);

            modelBuilder.Entity<RenglonDeReserva>()
                        .Property(o => o.CostoTotal)
                        .HasPrecision(15, 2);

        }

        public DbSet<Calidad> Calidad { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Transportista> Transportista { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Titular> Titular { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ComprobanteEntrega> ComprobanteEntrega { get; set; }
    }
}
