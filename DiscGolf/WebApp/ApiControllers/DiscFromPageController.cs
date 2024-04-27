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
    public class DiscFromPageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiscFromPageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DiscFromPage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscFromPage>>> GetDiscsFromPage()
        {
            return await _context.DiscsFromPage.ToListAsync();
        }

        // GET: api/DiscFromPage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscFromPage>> GetDiscFromPage(Guid id)
        {
            var discFromPage = await _context.DiscsFromPage.FindAsync(id);

            if (discFromPage == null)
            {
                return NotFound();
            }

            return discFromPage;
        }

        // PUT: api/DiscFromPage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscFromPage(Guid id, DiscFromPage discFromPage)
        {
            if (id != discFromPage.Id)
            {
                return BadRequest();
            }

            _context.Entry(discFromPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscFromPageExists(id))
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

        // POST: api/DiscFromPage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiscFromPage>> PostDiscFromPage(DiscFromPage discFromPage)
        {
            _context.DiscsFromPage.Add(discFromPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscFromPage", new { id = discFromPage.Id }, discFromPage);
        }

        // DELETE: api/DiscFromPage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscFromPage(Guid id)
        {
            var discFromPage = await _context.DiscsFromPage.FindAsync(id);
            if (discFromPage == null)
            {
                return NotFound();
            }

            _context.DiscsFromPage.Remove(discFromPage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscFromPageExists(Guid id)
        {
            return _context.DiscsFromPage.Any(e => e.Id == id);
        }
    }
}
