using FederalBonds.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FederalBonds.Data
{
    // ============================================================
    // ===== Entity Framework Core database context for the app.
    // ===== Integrates ASP.NET Identity and defines all data sets.
    // ============================================================
    public class FederalBondsContext : IdentityDbContext
    {
        // ===== Constructor injecting configuration options for EF Core
        public FederalBondsContext(DbContextOptions<FederalBondsContext> options) 
            : base(options) { }

        // ===== Database tables (DbSets) representing the domain entities
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Investment> Investments => Set<Investment>();
        public DbSet<Profile> Profiles => Set<Profile>();
    }
}
