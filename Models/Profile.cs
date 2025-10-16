using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FederalBonds.Models
{
    // ============================================================
    // ===== Represents an application user profile, linked to an IdentityUser.
    // ===== Contains personal data, account status, and investments.
    // ============================================================
    public class Profile
    {
        // ===== Unique identifier for each profile
        public int Id { get; set; }

        // ===== Basic personal information
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        // ===== Optional profile image path
        public string? ImagePath { get; set; }

        // ===== Indicates whether the profile is active
        public bool IsActive { get; set; } = true;

        // ===== All investments associated with this profile
        public ICollection<Investment> Investments { get; set; } = new List<Investment>();

        // ===== Combines first and last name for display purposes
        public string FullName => $"{FirstName} {LastName}";

        // ===== Foreign key link to IdentityUser
        [Required]
        public string UserId { get; set; } = string.Empty;

        // ===== Navigation property for IdentityUser reference
        public IdentityUser? User { get; set; }
    }
}
