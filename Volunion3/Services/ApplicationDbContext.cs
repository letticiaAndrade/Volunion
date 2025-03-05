using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volunion3.Models;

namespace Volunion3.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CampanhaVoluntario> CampanhaVoluntarios { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<CampanhaVoluntario> CampanhasVoluntarios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var organizacao = new IdentityRole
            {
                Id = "1c29e18e-0dcb-4bfa-aebf-48d7f46a5f1e", // ID fixo
                Name = "organizacao",
                NormalizedName = "organizacao"
            };

            var voluntario = new IdentityRole
            {
                Id = "9b94c9d5-40f8-44ff-947c-5763fdf18a75", // ID fixo
                Name = "voluntario",
                NormalizedName = "organizacao"
            };
            base.OnModelCreating(builder);

            builder.Entity<CampanhaVoluntario>()
                .HasKey(cv => new { cv.CampanhaId, cv.VoluntarioId });

            builder.Entity<CampanhaVoluntario>()
                .HasOne(cv => cv.Campanha)
                .WithMany(c => c.CampanhaVoluntarios)
                .HasForeignKey(cv => cv.CampanhaId)
                .OnDelete(DeleteBehavior.NoAction)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CampanhaVoluntario>()
                .HasOne(cv => cv.Voluntario)
                .WithMany(v => v.CampanhaVoluntarios)
                .HasForeignKey(cv => cv.VoluntarioId)
                .OnDelete(DeleteBehavior.NoAction)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CampanhaVoluntario>()
                .Property(cv => cv.VoluntarioId)
             .HasMaxLength(450);

                 builder.Entity<CampanhaVoluntario>()
                .Property(cv => cv.CampanhaId)
                .HasMaxLength(450);

        }

    }
}
