using Microsoft.AspNetCore.Mvc;
using DotnetMysql.Data;
using DotnetMysql.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class AnimalController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnimalController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Animal
    public IActionResult Index()
    {
        var animals = _context.Animals.ToList(); // Assurez-vous que Animals est bien le DbSet dans votre ApplicationDbContext
        return View(animals); // Renvoie la vue avec la liste des animaux
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Animal animal)
    {
        if (ModelState.IsValid)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        return View(animal); // Renvoie la vue avec les erreurs si le modèle n'est pas valide
    }

    // GET: Animal/Edit/5
    public IActionResult Edit(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal == null)
        {
            return NotFound(); // Gérer le cas où l'animal n'existe pas
        }
        return View(animal);
    }

    // POST: Animal/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return NotFound(); // Gérer le cas où l'ID ne correspond pas
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(animal);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(animal.Id))
                {
                    return NotFound(); // Gérer le cas où l'animal n'existe pas
                }
                else
                {
                    throw; // Relancer l'exception pour un autre type d'erreur
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(animal); // Renvoie la vue avec les erreurs si le modèle n'est pas valide
    }

    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }

    
}
