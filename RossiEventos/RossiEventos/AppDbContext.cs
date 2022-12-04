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
            modelBuilder.Entity<Usuario>()
                        .HasIndex(u => u.NroDni)
                        .IsUnique();

            modelBuilder.Entity<Usuario>()
                        .HasIndex(u => u.Cuit)
                        .IsUnique();
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
