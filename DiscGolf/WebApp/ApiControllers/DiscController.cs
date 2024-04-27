using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiscController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Disc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disc>>> GetDisc()
        {
            return await _context.Disc.ToListAsync();
        }

        // GET: api/Disc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disc>> GetDisc(Guid id)
        {
            var disc = await _context.Disc.FindAsync(id);

            if (disc == null)
            {
                return NotFound();
            }

            return disc;
        }

        // PUT: api/Disc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisc(Guid id, Disc disc)
        {
            if (id != disc.Id)
            {
                return BadRequest();
            }

            _context.Entry(disc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Disc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Disc>> PostDisc(Disc disc)
        {
            _context.Disc.Add(disc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisc", new { id = disc.Id }, disc);
        }

        // DELETE: api/Disc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisc(Guid id)
        {
            var disc = await _context.Disc.FindAsync(id);
            if (disc == null)
            {
                return NotFound();
            }

            _context.Disc.Remove(disc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscExists(Guid id)
        {
            return _context.Disc.Any(e => e.Id == id);
        }
    }
}
