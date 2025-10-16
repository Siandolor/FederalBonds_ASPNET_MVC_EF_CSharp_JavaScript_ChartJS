using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FederalBonds.Models
{
    // ============================================================
    // ===== Represents a user's investment in a government bond.
    // ===== Links a Product to an IdentityUser and tracks investment details.
    // ============================================================
    public class Investment
    {
        // ===== Unique identifier for each investment record
        public int Id { get; set; }

        // ===== Foreign key reference to the associated product
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // ===== Foreign key reference to the investor (IdentityUser)
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        // ===== Investment amount in EUR (minimum 100 €)
        [Column(TypeName = "decimal(18,2)")]
        [Range(100, double.MaxValue, ErrorMessage = "The minimum investment amount is €100.")]
        public decimal Amount { get; set; }

        // ===== Date when the investment was made
        public DateTime PurchaseDate { get; set; }

        // ===== Optional date when the investment was sold or matured
        public DateTime? SaleDate { get; set; }
    }
}
