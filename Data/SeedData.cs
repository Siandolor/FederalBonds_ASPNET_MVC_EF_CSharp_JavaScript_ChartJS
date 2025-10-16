using FederalBonds.Models;
using Microsoft.EntityFrameworkCore;

namespace FederalBonds.Data
{
    // ============================================================
    // ===== Populates the database with initial product data.
    // ===== Called automatically during application startup
    // ===== if the product table is empty.
    // ============================================================
    public static class SeedData
    {
        // ===== Initializes the database with default records
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // ===== Create a scoped context instance using DI
            using var context = new FederalBondsContext(
                serviceProvider.GetRequiredService<DbContextOptions<FederalBondsContext>>());

            // ===== Exit if product data already exists
            if (context.Products.Any()) return;

            // ===== Add predefined government bond products
            context.Products.AddRange(
                new Product
                {
                    Name = "Classic Federal Bond 1 Month",
                    Duration = "1 Month",
                    Rate = "2.5% p.a.",
                    IsGreen = false,
                    Description = "Short-term investment with fixed interest."
                },
                new Product
                {
                    Name = "Classic Federal Bond 12 Months",
                    Duration = "12 Months",
                    Rate = "3.0% p.a.",
                    IsGreen = false,
                    Description = "One-year term with fixed interest rate."
                },
                new Product
                {
                    Name = "Classic Federal Bond 10 Years",
                    Duration = "10 Years",
                    Rate = "3.5% p.a.",
                    IsGreen = false,
                    Description = "Long-term investment with fixed interest."
                },
                new Product
                {
                    Name = "Green Federal Bond 6 Months",
                    Duration = "6 Months",
                    Rate = "2.8% p.a.",
                    IsGreen = true,
                    Description = "Short-term investment with sustainable focus."
                },
                new Product
                {
                    Name = "Green Federal Bond 4 Years",
                    Duration = "4 Years",
                    Rate = "3.2% p.a.",
                    IsGreen = true,
                    Description = "Sustainable mid-term investment option."
                }
            );

            // ===== Commit all seed data to the database
            context.SaveChanges();
        }
    }
}
