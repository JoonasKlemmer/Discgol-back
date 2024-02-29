using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class DiscController : Controller
    {
        private readonly AppDbContext _context;

        public DiscController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Disc
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Disc.Include(d => d.Categories).Include(d => d.Manufacturer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Disc/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _context.Disc
                .Include(d => d.Categories)
                .Include(d => d.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // GET: Disc/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Location");
            return View();
        }

        // POST: Disc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Speed,Glide,Turn,Fade,ManufacturerId,CategoryId")] Disc disc)
        {
            if (ModelState.IsValid)
            {
                disc.Id = Guid.NewGuid();
                _context.Add(disc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Location", disc.ManufacturerId);
            return View(disc);
        }

        // GET: Disc/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _context.Disc.FindAsync(id);
            if (disc == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Location", disc.ManufacturerId);
            return View(disc);
        }

        // POST: Disc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Speed,Glide,Turn,Fade,ManufacturerId,CategoryId")] Disc disc)
        {
            if (id != disc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscExists(disc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturer, "Id", "Location", disc.ManufacturerId);
            return View(disc);
        }

        // GET: Disc/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _context.Disc
                .Include(d => d.Categories)
                .Include(d => d.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // POST: Disc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var disc = await _context.Disc.FindAsync(id);
            if (disc != null)
            {
                _context.Disc.Remove(disc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscExists(Guid id)
        {
            return _context.Disc.Any(e => e.Id == id);
        }
    }
}
