using Microsoft.EntityFrameworkCore;
using TDL.Models;
namespace TDL.Data
{
    public class AppDbContext : DbContext
    {
      
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Prioridades> Prioridades { get; set; }
        public DbSet<HistorialTarea> HistorialTareas { get; set; }
        public DbSet<TokenRecuperacion> TokensRecuperacion { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-I4CNN2P\\SQLEXPRESS;Database=TD;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Tarea>()
        //        .HasOne(t => t.Estado)
        //        .WithMany()
        //        .HasForeignKey(t => t.ID_estado)
        //        .HasPrincipalKey(e => e.ID);
        //}



    }
}
