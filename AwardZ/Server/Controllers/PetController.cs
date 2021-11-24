using AwardZ.Server.Data;
using AwardZ.Shared.Models;
using AwardZ.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AwardZ.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pets = await _context.Pets.ToListAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pet = await _context.Pets.SingleOrDefaultAsync(x => x.Id == id);
            return Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pet pet)
        {
            _context.Add(pet);
            await _context.SaveChangesAsync();
            return Ok(pet.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Pet pet)
        {
            _context.Entry(pet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = new Pet { Id = id };
            _context.Remove(pet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
