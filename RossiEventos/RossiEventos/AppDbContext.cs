using RossiEventos.Entidades;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;
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
            //Usuario
            modelBuilder.Entity<Persona>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Persona>()
                        .HasIndex(u => u.Cuit)
                        .IsUnique();

            //Transportista
            modelBuilder.Entity<Transportista>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Transportista>()
                        .HasIndex(u => u.Cuit)
                        .IsUnique();

            //Titular
            modelBuilder.Entity<Titular>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Titular>()
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
        }

        public DbSet<Calidad> Calidad { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Producto> Producto { get; set; }
       // public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Transportista> Transportistas { get; set; }
        public DbSet<Titular> Titulares { get; set; }
    }
}
