
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly IAppBLL _bll;

        public WebsiteController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Website
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Websites.GetAllAsync());
        }

        // GET: Website/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _bll.Websites
                .FirstOrDefaultAsync(id.Value);
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
        public async Task<IActionResult> Create(Website website)
        {
            if (ModelState.IsValid)
            {
                website.Id = Guid.NewGuid();
                _bll.Websites.Add(website);
                await _bll.SaveChangesAsync();
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

            var website = await _bll.Websites.FirstOrDefaultAsync(id.Value);
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
        public async Task<IActionResult> Edit(Guid id,Website website)
        {
            if (id != website.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Websites.Update(website);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await WebsiteExists(website.Id))
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

            var website = await _bll.Websites
                .FirstOrDefaultAsync(id.Value);
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
            var website = await _bll.Websites.FirstOrDefaultAsync(id);
            if (website != null)
            {
                await _bll.Websites.RemoveAsync(website);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> WebsiteExists(Guid id)
        {
            return _bll.Websites.ExistsAsync(id);
        }
    }
}
