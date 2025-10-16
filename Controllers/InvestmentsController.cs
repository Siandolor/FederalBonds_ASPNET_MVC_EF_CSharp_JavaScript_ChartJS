using FederalBonds.Data;
using FederalBonds.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FederalBonds.Controllers
{
    // ============================================================
    // ===== Handles investment management for authenticated users.
    // ===== Allows viewing, creating, and selling of investments.
    // ============================================================
    [Authorize]
    public class InvestmentsController : Controller
    {
        // ===== Dependencies: database context and identity manager
        private readonly FederalBondsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // ===== Constructor injects required services for data and user handling
        public InvestmentsController(FederalBondsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ============================================================
        // ===== GET: /Investments
        // ===== Lists all investments belonging to the currently logged-in user.
        // ============================================================
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var investments = await _context.Investments
                .Include(i => i.Product)
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return View(investments);
        }

        // ============================================================
        // ===== GET: /Investments/Create
        // ===== Displays a list of available products to invest in.
        // ============================================================
        public async Task<IActionResult> Create()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // ============================================================
        // ===== POST: /Investments/Create
        // ===== Creates a new investment for the logged-in user.
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, decimal amount)
        {
            var userId = _userManager.GetUserId(User);

            var investment = new Investment
            {
                ProductId = productId,
                UserId = userId ?? string.Empty,
                Amount = amount,
                PurchaseDate = DateTime.Today
            };

            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // ===== GET: /Investments/Sell/{id}
        // ===== Marks an existing investment as sold by setting a SaleDate.
        // ============================================================
        public async Task<IActionResult> Sell(int id)
        {
            var investment = await _context.Investments
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (investment == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            // ===== Prevent unauthorized access to other users' investments
            if (investment.UserId != userId)
            {
                return Forbid();
            }

            // ===== Only mark as sold if not already sold
            if (investment.SaleDate == null)
            {
                investment.SaleDate = DateTime.Today;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
