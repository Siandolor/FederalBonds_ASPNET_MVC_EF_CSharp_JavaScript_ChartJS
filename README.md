## FederalBonds – Secure Investment Web Application

> “FederalBonds” is an educational and technical demo project illustrating secure, transparent, and user-friendly investment simulations using modern .NET architecture.

---

### Overview
**FederalBonds** is a modern ASP.NET Core MVC web application inspired by the Austrian “Bundesschatz” investment system.  
It allows users to register, manage their profiles, view government bond–style products, and simulate investments in a secure and user-friendly way.

The project demonstrates:
- Full **MVC architecture** using Razor Pages
- **Identity-based authentication**
- A **seeded SQLite database**
- Clean UI built with **Bootstrap 5**
- Multi-view design including product listings, FAQ, profile management, and investments

---

### Features
| Category | Description                                                                      |
|-----------|----------------------------------------------------------------------------------|
| **Authentication** | ASP.NET Core Identity with login, register, and logout functionality             |
| **Database** | SQLite database using Entity Framework Core (auto-created and seeded on startup) |
| **Products** | Displays both “Classic” and “Green” Federal Bonds investments                    |
| **Investments** | Allows users to invest, track, and sell                                          |
| **Profile Management** | Editable user profile with image upload and status toggle                        |
| **Charts** | Real-time investment distribution visualization using Chart.js                   |
| **Responsive Design** | Built with Bootstrap 5 for desktop and mobile                                    |

---

### 🧩 Architecture

```
FederalBonds/
│
├── Controllers/               # MVC controllers (Home, Account, Profiles, etc.)
├── Models/                    # Data models (Product, Profile, Investment)
├── ViewModels/                # View-specific models (Login, Register, Error)
├── Data/                      # EF Core DbContext + SeedData
├── Attributes/                # Custom validation attributes
├── Views/                     # Razor views for all pages
├── wwwroot/                   # Static files (CSS, JS, images)
└── Program.cs                 # Application entry point
```

---

### Database

- Default database: **SQLite**
- Database file automatically created in the working directory
- Seed data includes example FederalBonds investment products

---

### Getting Started

#### 1. Clone the repository
```bash
  git clone https://github.com/<your-username>/FederalBonds.git
  cd FederalBonds
```

#### 2. Run the application
```bash
  dotnet run
```

#### 3. Open in browser
```
https://localhost:5001
```

---

### Technology Stack

| Category | Technology |
|-----------|-------------|
| Framework | .NET 9.0 |
| Language | C# 12 |
| Web Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQLite |
| UI Framework | Bootstrap 5.3 |
| Charts | Chart.js |
| Identity | ASP.NET Core Identity |
| JSON | Newtonsoft.Json |

---

### Development Notes

- Development uses SQLite for simplicity, but easily switchable to SQL Server or PostgreSQL.
- Identity roles and tokens are configured but confirmation is disabled for local testing.
- The project is fully cross-platform and runs on macOS, Linux, and Windows.

---

### Example Default Products (Seeded)

| Product                       | Duration | Rate | Type |
|-------------------------------|-----------|------|------|
| Classic Federal Bond 1 Month  | 1 Month | 2.5 % p.a. | Classic |
| Classic Federal Bond 12 Months | 12 Months | 3.0 % p.a. | Classic |
| Classic Federal Bond 10 Years | 10 Years | 3.5 % p.a. | Classic |
| Green Federal Bond 6 Months   | 6 Months | 2.8 % p.a. | Green |
| Green Federal Bond 4 Years    | 4 Years | 3.2 % p.a. | Green |

---

### Author & License

**Author:** Daniel Fitz  
**License:** MIT License  

---

### Future Enhancements

- Two-Factor authentication  
- Investment export to CSV/JSON  
- Role-based admin dashboard  
- REST API layer for external integration  
- Localization (English + German switch)  

---

> “FederalBonds is more than code —  
> it’s a reflection of how secure systems should feel: simple, transparent, and human.”  
> — Daniel Fitz, 2025
