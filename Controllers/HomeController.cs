using FederalBonds.Data;
using Microsoft.AspNetCore.Mvc;

namespace FederalBonds.Controllers
{
    // ============================================================
    // ===== Main entry controller for the application.
    // ===== Handles homepage, FAQ, and contact views.
    // ============================================================
    public class HomeController : Controller
    {
        // ===== Application database context for data access
        private readonly FederalBondsContext _context;

        // ===== Injects the EF Core database context via dependency injection
        public HomeController(FederalBondsContext context)
        {
            _context = context;
        }

        // ============================================================
        // ===== GET: /Home/Index
        // ===== Displays the homepage and lists all available products.
        // ============================================================
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // ============================================================
        // ===== GET: /Home/FAQ
        // ===== Displays the frequently asked questions page.
        // ============================================================
        public IActionResult FAQ()
        {
            return View();
        }

        // ============================================================
        // ===== GET: /Home/Contact
        // ===== Displays the contact page for user inquiries.
        // ============================================================
        public IActionResult Contact()
        {
            return View();
        }
    }
}
