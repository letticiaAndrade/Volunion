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

            builder.Entity<IdentityRole>().HasData(organizacao, voluntario);
        }
    }
}
