using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionApp.Data;
using PetAdoptionApp.Models;

namespace PetAdoptionApp.Controllers
{
    public class AdoptersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adopters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adopters.ToListAsync());
        }

        // GET: Adopters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adopters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PhoneNumber")] Adopter adopter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adopter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adopter);
        }

        // GET: Adopters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopter = await _context.Adopters
                .Include(a => a.Adoptions)
                .ThenInclude(a => a.Pet)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (adopter == null)
            {
                return NotFound();
            }

            return View(adopter);
        }
    }
}
