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
    public class DiscFromPageController : Controller
    {
        private readonly AppDbContext _context;

        public DiscFromPageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DiscFromPage
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DiscsFromPage.Include(d => d.Discs).Include(d => d.PriceValue).Include(d => d.Websites);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DiscFromPage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _context.DiscsFromPage
                .Include(d => d.Discs)
                .Include(d => d.PriceValue)
                .Include(d => d.Websites)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discFromPage == null)
            {
                return NotFound();
            }

            return View(discFromPage);
        }

        // GET: DiscFromPage/Create
        public IActionResult Create()
        {
            ViewData["DiscId"] = new SelectList(_context.Disc, "Id", "Name");
            ViewData["PriceId"] = new SelectList(_context.Price, "Id", "Iso");
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "Url");
            return View();
        }

        // POST: DiscFromPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,DiscId,WebsiteId,PriceId")] DiscFromPage discFromPage)
        {
            if (ModelState.IsValid)
            {
                discFromPage.Id = Guid.NewGuid();
                _context.Add(discFromPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscId"] = new SelectList(_context.Disc, "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(_context.Price, "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // GET: DiscFromPage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _context.DiscsFromPage.FindAsync(id);
            if (discFromPage == null)
            {
                return NotFound();
            }
            ViewData["DiscId"] = new SelectList(_context.Disc, "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(_context.Price, "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // POST: DiscFromPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Price,DiscId,WebsiteId,PriceId")] DiscFromPage discFromPage)
        {
            if (id != discFromPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discFromPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscFromPageExists(discFromPage.Id))
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
            ViewData["DiscId"] = new SelectList(_context.Disc, "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(_context.Price, "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(_context.Website, "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // GET: DiscFromPage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _context.DiscsFromPage
                .Include(d => d.Discs)
                .Include(d => d.PriceValue)
                .Include(d => d.Websites)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discFromPage == null)
            {
                return NotFound();
            }

            return View(discFromPage);
        }

        // POST: DiscFromPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var discFromPage = await _context.DiscsFromPage.FindAsync(id);
            if (discFromPage != null)
            {
                _context.DiscsFromPage.Remove(discFromPage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscFromPageExists(Guid id)
        {
            return _context.DiscsFromPage.Any(e => e.Id == id);
        }
    }
}
