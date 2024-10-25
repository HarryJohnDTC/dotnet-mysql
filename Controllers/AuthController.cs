using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetMysql.Data;
using DotnetMysql.Models;
using System.Threading.Tasks;

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
                // Hacher le mot de passe saisi
                var hashedPassword = user.HashPassword(user.Pass);

                // Vérifiez si l'utilisateur existe
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Pseudo == user.Pseudo && u.Pass == hashedPassword);
                if (existingUser != null)
                {
                    // Authentifiez l'utilisateur
                    // Utilisez des cookies ou des sessions pour gérer la connexion
                    // Exemple : TempData["UserId"] = existingUser.Id;
                    return RedirectToAction("Index", "Home"); // Redirection vers la page d'accueil
                }

                ModelState.AddModelError("", "Tentative de connexion invalide.");
            }
            return View(user);
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
                // Hacher le mot de passe avant de le stocker
                user.Pass = user.HashPassword(user.Pass);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login"); // Redirection vers la page de connexion
            }
            return View(user);
        }
    }
}
