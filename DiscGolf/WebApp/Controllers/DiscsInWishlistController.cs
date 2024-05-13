using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    
    public class DiscsInWishlistController : Controller
    {
        private readonly AppDbContext _context;

        public DiscsInWishlistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DiscsInWishlist
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DiscInWishlist.Include(d => d.DiscFromPage).Include(d => d.Wishlists);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DiscsInWishlist/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _context.DiscInWishlist
                .Include(d => d.DiscFromPage)
                .Include(d => d.Wishlists)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discsInWishlist == null)
            {
                return NotFound();
            }

            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Create
        public IActionResult Create()
        {
            ViewData["DiscFromPageId"] = new SelectList(_context.DiscsFromPage, "Id", "Id");
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "Id", "WishlistName");
            return View();
        }

        // POST: DiscsInWishlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiscFromPageId,WishlistId")] DiscsInWishlist discsInWishlist)
        {
            if (ModelState.IsValid)
            {
                discsInWishlist.Id = Guid.NewGuid();
                _context.Add(discsInWishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscFromPageId"] = new SelectList(_context.DiscsFromPage, "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _context.DiscInWishlist.FindAsync(id);
            if (discsInWishlist == null)
            {
                return NotFound();
            }
            ViewData["DiscFromPageId"] = new SelectList(_context.DiscsFromPage, "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // POST: DiscsInWishlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DiscFromPageId,WishlistId")] DiscsInWishlist discsInWishlist)
        {
            if (id != discsInWishlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discsInWishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscsInWishlistExists(discsInWishlist.Id))
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
            ViewData["DiscFromPageId"] = new SelectList(_context.DiscsFromPage, "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlist, "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _context.DiscInWishlist
                .Include(d => d.DiscFromPage)
                .Include(d => d.Wishlists)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discsInWishlist == null)
            {
                return NotFound();
            }

            return View(discsInWishlist);
        }

        // POST: DiscsInWishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var discsInWishlist = await _context.DiscInWishlist.FindAsync(id);
            if (discsInWishlist != null)
            {
                _context.DiscInWishlist.Remove(discsInWishlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscsInWishlistExists(Guid id)
        {
            return _context.DiscInWishlist.Any(e => e.Id == id);
        }
    }
}
