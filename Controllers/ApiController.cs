using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionApp.Data;
using PetAdoptionApp.Models;

namespace PetAdoptionApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/adoptions
        [HttpGet("adoptions")]
        public async Task<IActionResult> GetAdoptions()
        {
            var adoptions = await _context.Adoptions
                .Include(a => a.Pet)
                .Include(a => a.Adopter)
                .Select(a => new {
                    id = a.Id,
                    petName = a.Pet.Name,
                    adopterName = a.Adopter.FullName,
                    adoptionDate = a.AdoptionDate,
                    status = "Completada"
                })
                .ToListAsync();
                
            return Ok(adoptions);
        }
    }
}
