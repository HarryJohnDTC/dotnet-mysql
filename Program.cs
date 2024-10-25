using Microsoft.EntityFrameworkCore;
using DotnetMysql.Data;
using DotnetMysql.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuration du DbContext pour utiliser MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 21))));

// Ajouter les services aux contrôleurs avec vues
builder.Services.AddControllersWithViews();

// Configurer l'authentification par cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

var app = builder.Build();



// Configurer le pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Valeur HSTS par défaut de 30 jours
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Authentification
app.UseAuthorization();

// Configurer les routes
app.MapControllerRoute(
    name: "animal",
    pattern: "Animal/{action=Index}/{id?}",
    defaults: new { controller = "Animal" }
);

// Ajoutez une route pour le contrôleur d'authentification
app.MapControllerRoute(
    name: "auth",
    pattern: "Auth/{action=Login}/{id?}",
    defaults: new { controller = "Auth", action = "Login" }
);

// Route par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
