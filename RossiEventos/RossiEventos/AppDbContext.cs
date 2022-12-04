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


            //Creación de índices vía fluent api
            //Usuario
            modelBuilder.Entity<Usuario>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Usuario>()
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
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transportista> Transportistas { get; set; }
        public DbSet<Titular> Titulares { get; set; }
        
    }
}
