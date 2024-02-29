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
    public class ManufacturerController : Controller
    {
        private readonly AppDbContext _context;

        public ManufacturerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manufacturer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manufacturer.ToListAsync());
        }

        // GET: Manufacturer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: Manufacturer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ManufacturerName,Location")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.Id = Guid.NewGuid();
                _context.Add(manufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        // GET: Manufacturer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ManufacturerName,Location")] Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.Id))
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
            return View(manufacturer);
        }

        // GET: Manufacturer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var manufacturer = await _context.Manufacturer.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturer.Remove(manufacturer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(Guid id)
        {
            return _context.Manufacturer.Any(e => e.Id == id);
        }
    }
}