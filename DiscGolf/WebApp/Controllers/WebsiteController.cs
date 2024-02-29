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
    public class WebsiteController : Controller
    {
        private readonly AppDbContext _context;

        public WebsiteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Website
        public async Task<IActionResult> Index()
        {
            return View(await _context.Website.ToListAsync());
        }

        // GET: Website/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // GET: Website/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Website/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,WebsiteName")] Website website)
        {
            if (ModelState.IsValid)
            {
                website.Id = Guid.NewGuid();
                _context.Add(website);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(website);
        }

        // GET: Website/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }
            return View(website);
        }

        // POST: Website/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Url,WebsiteName")] Website website)
        {
            if (id != website.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(website);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteExists(website.Id))
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
            return View(website);
        }

        // GET: Website/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // POST: Website/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var website = await _context.Website.FindAsync(id);
            if (website != null)
            {
                _context.Website.Remove(website);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteExists(Guid id)
        {
            return _context.Website.Any(e => e.Id == id);
        }
    }
}
