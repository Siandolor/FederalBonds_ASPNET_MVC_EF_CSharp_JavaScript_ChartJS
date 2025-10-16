using FederalBonds.Data;
using Microsoft.AspNetCore.Mvc;

namespace FederalBonds.Controllers
{
    // ============================================================
    // ===== Handles all product-related views and interactions.
    // ===== Provides product listings and detailed information.
    // ============================================================
    public class ProductsController : Controller
    {
        // ===== Database context for product data access
        private readonly FederalBondsContext _context;

        // ===== Constructor injects the EF Core database context
        public ProductsController(FederalBondsContext context)
        {
            _context = context;
        }

        // ============================================================
        // ===== GET: /Products
        // ===== Displays a list of all available financial products.
        // ============================================================
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // ============================================================
        // ===== GET: /Products/Details/{id}
        // ===== Displays detailed information for a single product.
        // ============================================================
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
