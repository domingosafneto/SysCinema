using Microsoft.EntityFrameworkCore;
using WebApi_Cinema.Models;

namespace WebApi_Cinema.Data
{
    public class Api_DbContext : DbContext
    {
        public Api_DbContext(DbContextOptions<Api_DbContext> options)
            : base(options) { }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<Filme> Filmes { get; set; }

        public DbSet<SalaFilme> SalaFilmes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<SalaFilme>()
                .HasKey(sf => new { sf.IdSala, sf.IdFilme });
            
            modelBuilder.Entity<SalaFilme>()
                .HasOne(sf => sf.Sala)
                .WithMany(s => s.SalaFilmes)
                .HasForeignKey(sf => sf.IdSala)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalaFilme>()
                .HasOne(sf => sf.Filme)
                .WithMany(f => f.SalaFilmes)
                .HasForeignKey(sf => sf.IdFilme)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
