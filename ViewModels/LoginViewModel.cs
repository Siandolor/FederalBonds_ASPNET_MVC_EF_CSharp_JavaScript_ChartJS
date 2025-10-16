using System.ComponentModel.DataAnnotations;

namespace FederalBonds.ViewModels
{
    // ============================================================
    // ===== ViewModel for handling user login form input.
    // ===== Used by AccountController.Login (GET/POST).
    // ============================================================
    public class LoginViewModel
    {
        // ===== User's email address (used as the login identifier)
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // ===== User's password input (masked in the UI)
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // ===== Optional flag to persist login across sessions
        public bool RememberMe { get; set; }
    }
}
