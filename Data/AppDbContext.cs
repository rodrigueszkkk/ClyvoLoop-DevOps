using Microsoft.EntityFrameworkCore;
using PetHealthEcosystem.Api.Models;

namespace PetHealthEcosystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento da entidade (5 pts)
            modelBuilder.Entity<Pet>().ToTable("PETS");
            modelBuilder.Entity<Pet>().HasKey(p => p.Id);
            modelBuilder.Entity<Pet>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Pet>().Property(p => p.Breed).HasMaxLength(50);

            // Converte o bool para int (0 ou 1) para compatibilidade com o Oracle 21c
            modelBuilder.Entity<Pet>().Property(p => p.NeedsPostOpCare).HasConversion<int>();
        }
    }
}