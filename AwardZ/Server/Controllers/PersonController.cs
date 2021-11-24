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
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pers = await _context.Persons.ToListAsync();
            return Ok(pers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var per = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id);
            return Ok(per);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person per)
        {
            _context.Add(per);
            await _context.SaveChangesAsync();
            return Ok(per.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Person per)
        {
            _context.Entry(per).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var per = new Person { Id = id };
            _context.Remove(per);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

