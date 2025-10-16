namespace FederalBonds.Models
{
    // ============================================================
    // ===== Represents a financial product offered for investment.
    // ===== Contains basic metadata such as name, duration, rate,
    // ===== and a sustainability flag (IsGreen).
    // ============================================================
    public class Product
    {
        // ===== Unique identifier for the product
        public int Id { get; set; }

        // ===== Product name (e.g., “Federal Bond 2030”)
        public string Name { get; set; } = string.Empty;

        // ===== Duration or term of the product (e.g., “10 years”)
        public string Duration { get; set; } = string.Empty;

        // ===== Interest rate or return information
        public string Rate { get; set; } = string.Empty;

        // ===== Indicates if the product is part of a green/sustainable portfolio
        public bool IsGreen { get; set; }

        // ===== Descriptive text about the product
        public string Description { get; set; } = string.Empty;
    }
}
