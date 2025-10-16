using System.ComponentModel.DataAnnotations;

namespace FederalBonds.ViewModels
{
    // ============================================================
    // ===== ViewModel for handling user registration input.
    // ===== Used by AccountController.Register (GET/POST).
    // ============================================================
    public class RegisterViewModel
    {
        // ===== User's email address (used as login identifier)
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // ===== Primary password for account creation
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // ===== Confirmation field — must match Password
        [Required, DataType(DataType.Password), 
         Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // ===== User's first name
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        // ===== User's last name
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
    }
}
