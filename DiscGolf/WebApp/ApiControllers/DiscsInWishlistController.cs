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
    public class DiscsInWishlistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiscsInWishlistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DiscsInWishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscsInWishlist>>> GetDiscInWishlist()
        {
            return await _context.DiscInWishlist.ToListAsync();
        }

        // GET: api/DiscsInWishlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscsInWishlist>> GetDiscsInWishlist(Guid id)
        {
            var discsInWishlist = await _context.DiscInWishlist.FindAsync(id);

            if (discsInWishlist == null)
            {
                return NotFound();
            }

            return discsInWishlist;
        }

        // PUT: api/DiscsInWishlist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscsInWishlist(Guid id, DiscsInWishlist discsInWishlist)
        {
            if (id != discsInWishlist.Id)
            {
                return BadRequest();
            }

            _context.Entry(discsInWishlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscsInWishlistExists(id))
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

        // POST: api/DiscsInWishlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiscsInWishlist>> PostDiscsInWishlist(DiscsInWishlist discsInWishlist)
        {
            _context.DiscInWishlist.Add(discsInWishlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscsInWishlist", new { id = discsInWishlist.Id }, discsInWishlist);
        }

        // DELETE: api/DiscsInWishlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscsInWishlist(Guid id)
        {
            var discsInWishlist = await _context.DiscInWishlist.FindAsync(id);
            if (discsInWishlist == null)
            {
                return NotFound();
            }

            _context.DiscInWishlist.Remove(discsInWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscsInWishlistExists(Guid id)
        {
            return _context.DiscInWishlist.Any(e => e.Id == id);
        }
    }
}
