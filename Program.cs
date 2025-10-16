// ============================================================
// ===== FederalBonds Application Entry Point
// ===== Configures services, middleware, and runtime pipeline
// ============================================================

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// ===== Database Configuration
// ===== Uses SQLite for cross-platform compatibility
// ============================================================
builder.Services.AddDbContext<FederalBondsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ============================================================
// ===== Identity Configuration
// ===== Handles authentication and user management
// ============================================================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<FederalBondsContext>()
    .AddDefaultTokenProviders();

// ============================================================
// ===== MVC Controllers and Razor Views
// ============================================================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ============================================================
// ===== Database Initialization and Seeding
// ===== Ensures schema creation and populates initial products
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FederalBondsContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
}

// ============================================================
// ===== Error Handling and Security Middleware
// ============================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ============================================================
// ===== Request Routing and Authentication Pipeline
// ============================================================
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ============================================================
// ===== Default Route Configuration
// ============================================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ============================================================
// ===== Application Run
// ============================================================
app.Run();
