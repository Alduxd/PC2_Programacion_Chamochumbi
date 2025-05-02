using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionApp.Data;
using PetAdoptionApp.Models;
using PetAdoptionApp.ViewModels;

namespace PetAdoptionApp.Controllers
{
    public class AdoptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adoptions
        public async Task<IActionResult> Index()
        {
            var adoptions = await _context.Adoptions
                .Include(a => a.Pet)
                .Include(a => a.Adopter)
                .ToListAsync();
                
            return View(adoptions);
        }

        // GET: Adoptions/Create
        public async Task<IActionResult> Create()
        {
            // Obtener solo mascotas disponibles (no adoptadas)
            var availablePets = await _context.Pets
                .Where(p => !p.IsAdopted)
                .ToListAsync();
                
            if (!availablePets.Any())
            {
                TempData["ErrorMessage"] = "No hay mascotas disponibles para adopción.";
                return RedirectToAction(nameof(Index));
            }
            
            var adopters = await _context.Adopters.ToListAsync();
            if (!adopters.Any())
            {
                TempData["ErrorMessage"] = "No hay adoptantes registrados.";
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["PetId"] = new SelectList(availablePets, "Id", "Name");
            ViewData["AdopterId"] = new SelectList(adopters, "Id", "FullName");
            
            return View();
        }

        // POST: Adoptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,AdopterId")] AdoptionViewModel adoptionViewModel)
        {
            if (ModelState.IsValid)
            {
                // Verificar que la mascota no esté ya adoptada
                var pet = await _context.Pets.FindAsync(adoptionViewModel.PetId);
                if (pet == null)
                {
                    ModelState.AddModelError("PetId", "Mascota no encontrada.");
                    return View(adoptionViewModel);
                }
                
                if (pet.IsAdopted)
                {
                    ModelState.AddModelError("PetId", "Esta mascota ya ha sido adoptada.");
                    return View(adoptionViewModel);
                }
                
                // Verificar que el adoptante exista
                var adopter = await _context.Adopters.FindAsync(adoptionViewModel.AdopterId);
                if (adopter == null)
                {
                    ModelState.AddModelError("AdopterId", "Adoptante no encontrado.");
                    return View(adoptionViewModel);
                }
                
                // Crear la adopción
                var adoption = new Adoption
                {
                    PetId = adoptionViewModel.PetId,
                    AdopterId = adoptionViewModel.AdopterId,
                    AdoptionDate = DateTime.Now
                };
                
                // Actualizar el estado de la mascota
                pet.IsAdopted = true;
                
                // Guardar los cambios
                _context.Add(adoption);
                _context.Update(pet);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            // Si llegamos aquí, algo falló, volvemos a cargar los datos
            var availablePets = await _context.Pets
                .Where(p => !p.IsAdopted || p.Id == adoptionViewModel.PetId)
                .ToListAsync();
                
            var adopters = await _context.Adopters.ToListAsync();
            
            ViewData["PetId"] = new SelectList(availablePets, "Id", "Name", adoptionViewModel.PetId);
            ViewData["AdopterId"] = new SelectList(adopters, "Id", "FullName", adoptionViewModel.AdopterId);
            
            return View(adoptionViewModel);
        }

        // GET: Adoptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Pet)
                .Include(a => a.Adopter)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }
    }
}
