using FederalBonds.Data;
using FederalBonds.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FederalBonds.Controllers
{
    // ============================================================
    // ===== Handles user profile management.
    // ===== Allows viewing, editing, and deleting user profiles.
    // ===== Each profile is linked to an IdentityUser.
    // ============================================================
    [Authorize]
    public class ProfilesController : Controller
    {
        // ===== Injected dependencies: database, identity, and hosting environment
        private readonly FederalBondsContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;

        // ===== Constructor injection for context, user management, and file handling
        public ProfilesController(
            FederalBondsContext context,
            UserManager<IdentityUser> userManager,
            IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        // ============================================================
        // ===== GET: /Profiles
        // ===== Displays the current user's profile and investments.
        // ============================================================
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // ===== Load the user's profile or redirect if not found
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (profile == null)
                return RedirectToAction("Register", "Account");

            // ===== Load user's active investments
            var myInvestments = await _context.Investments
                .Include(i => i.Product)
                .Where(i => i.UserId == user.Id && i.SaleDate == null)
                .ToListAsync();

            // ===== Load all active investments (for aggregated view)
            var totalInvestments = await _context.Investments
                .Include(i => i.Product)
                .Where(i => i.SaleDate == null)
                .ToListAsync();

            profile.Investments = myInvestments;
            ViewBag.TotalInvestments = totalInvestments;

            return View(profile);
        }

        // ============================================================
        // ===== GET: /Profiles/Edit
        // ===== Displays the profile edit form for the current user.
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.UserId == user.Id);
            if (profile == null) return NotFound();

            return View(profile);
        }

        // ============================================================
        // ===== POST: /Profiles/Edit
        // ===== Updates profile information and optionally the profile image.
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profile input, IFormFile? imageFile)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.UserId == user.Id);
            if (profile == null) return NotFound();

            ModelState.Remove("UserId");
            if (!ModelState.IsValid) return View(input);

            // ===== Update basic profile information
            profile.FirstName = input.FirstName;
            profile.LastName = input.LastName;
            profile.IsActive = input.IsActive;

            // ===== Handle profile image upload if provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images", "profiles");
                Directory.CreateDirectory(uploads);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                profile.ImagePath = $"/images/profiles/{fileName}";
            }

            _context.Update(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // ===== GET: /Profiles/Delete
        // ===== Displays a confirmation view for profile deletion.
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.UserId == user.Id);
            if (profile == null) return NotFound();

            return View(profile);
        }

        // ============================================================
        // ===== POST: /Profiles/Delete
        // ===== Deletes the user profile and associated account.
        // ===== Prevents deletion if active investments exist.
        // ============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles
                .Include(m => m.Investments)
                .FirstOrDefaultAsync(m => m.UserId == user.Id);

            if (profile == null) return NotFound();

            // ===== Prevent profile deletion if active investments exist
            if (profile.Investments.Any())
            {
                TempData["Error"] = "Deletion not possible: Active investments exist.";
                return RedirectToAction(nameof(Index));
            }

            // ===== Remove profile and delete corresponding user account
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(user);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
