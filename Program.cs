using Microsoft.EntityFrameworkCore;
using Project.Data;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication.Cookies; // Added for Step 2

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- STEP 2: AUTHENTICATION SERVICE CONFIGURATION ---
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "TenderCare.Auth"; // Name of the cookie
        options.LoginPath = "/Account/Login";    // Redirect here if unauthorized
        options.ExpireTimeSpan = TimeSpan.FromHours(2); // Auto logout after 2 hours
    });

// Database Connection String Setup
var csb = new MySqlConnectionStringBuilder
{
    Server = "localhost",
    Port = 3306,
    Database = "TenderCareDb",
    UserID = "root",
    Password = "PRESHEUN03"
};

// Connecting the TenderCareDbContext to MySQL
builder.Services.AddDbContext<TenderCareDbContext>(options =>
    options.UseMySql(csb.ConnectionString, ServerVersion.AutoDetect(csb.ConnectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// --- STEP 2: AUTHENTICATION MIDDLEWARE ---
// Important: UseAuthentication must come BEFORE UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

// Route Configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();