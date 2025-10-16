using FederalBonds.Data;
using FederalBonds.Models;
using FederalBonds.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FederalBonds.Controllers
{
    // ============================================================
    // ===== Handles user registration, authentication, and logout.
    // ===== Integrates ASP.NET Core Identity for secure user management.
    // ============================================================
    public class AccountController : Controller
    {
        // ===== Identity and database dependencies injected via constructor
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FederalBondsContext _context;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            FederalBondsContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // ============================================================
        // ===== GET: /Account/Register
        // ===== Displays the registration form for new users.
        // ============================================================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // ============================================================
        // ===== POST: /Account/Register
        // ===== Creates a new user account and associated profile record.
        // ============================================================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ===== Create and persist new IdentityUser
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // ===== Automatically create a corresponding Profile entry
                    var profile = new Profile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserId = user.Id,
                        IsActive = true,
                        ImagePath = null
                    };

                    _context.Profiles.Add(profile);
                    await _context.SaveChangesAsync();

                    // ===== Auto-login newly registered user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // ===== Add any identity creation errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // ============================================================
        // ===== GET: /Account/Login
        // ===== Displays the login page with optional return URL.
        // ============================================================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // ============================================================
        // ===== POST: /Account/Login
        // ===== Authenticates a user based on email and password.
        // ============================================================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // ===== Redirect user to the requested page or home
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                // ===== Generic feedback for invalid login attempt
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // ============================================================
        // ===== POST: /Account/Logout
        // ===== Signs the current user out and redirects to home page.
        // ============================================================
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
