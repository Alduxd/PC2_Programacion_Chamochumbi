using Microsoft.EntityFrameworkCore;
using PetAdoptionApp.Models;

namespace PetAdoptionApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adopter> Adopters { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configurar la relación uno a uno entre Pet y Adoption
            // Una mascota solo puede ser adoptada una vez
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Adoption)
                .WithOne(a => a.Pet)
                .HasForeignKey<Adoption>(a => a.PetId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configurar la relación uno a muchos entre Adopter y Adoption
            modelBuilder.Entity<Adopter>()
                .HasMany(a => a.Adoptions)
                .WithOne(a => a.Adopter)
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
