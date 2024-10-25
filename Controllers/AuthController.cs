using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetMysql.Data;
using DotnetMysql.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace DotnetMysql.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(User user)
{
    if (ModelState.IsValid)
    {
        Console.WriteLine($"Pseudo saisi: {user.Pseudo}");

        // Vérifiez si l'utilisateur existe avec le pseudo et le mot de passe en texte brut
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Pseudo == user.Pseudo && u.Pass == user.Pass);
        
        if (existingUser != null)
        {
            Console.WriteLine("Utilisateur trouvé !");
            
            // Authentifier l'utilisateur ici
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Pseudo)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Permet de se souvenir de l'utilisateur
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        Console.WriteLine("Tentative de connexion invalide.");
        ModelState.AddModelError("", "Tentative de connexion invalide.");
    }
    return View(user);
}

public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Login");
}


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Ajoutez l'utilisateur sans hachage pour l'instant
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login"); // Redirection vers la page de connexion
            }
            return View(user);
        }
    }
}
