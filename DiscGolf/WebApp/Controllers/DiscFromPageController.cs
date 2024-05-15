using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Controllers
{
    public class DiscFromPageController : Controller
    {

        private readonly IAppBLL _bll;
        

        public DiscFromPageController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: DiscFromPage
        public async Task<IActionResult> Index()
        {
            var res = await _bll.DiscFromPages.GetAllWithDetails();
            return View(res);
        }

        // GET: DiscFromPage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _bll.DiscFromPages.FirstOrDefaultAsync(id.Value);
            if (discFromPage == null)
            {
                return NotFound();
            }

            return View(discFromPage);
        }

        // GET: DiscFromPage/Create
        public IActionResult Create()
        {
            ViewData["DiscId"] = new SelectList(_bll.Discs.GetAll(), "Id", "Name");
            ViewData["PriceId"] = new SelectList(_bll.Prices.GetAll(), "Id", "Iso");
            ViewData["WebsiteId"] = new SelectList(_bll.Websites.GetAll(), "Id", "Url");
            return View();
        }

        // POST: DiscFromPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscFromPage discFromPage)
        {
            if (ModelState.IsValid)
            {
                discFromPage.Id = Guid.NewGuid();
                _bll.DiscFromPages.Add(discFromPage);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscId"] = new SelectList(await _bll.Discs.GetAllAsync(), "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(await _bll.Prices.GetAllAsync(), "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(await _bll.Websites.GetAllAsync(), "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // GET: DiscFromPage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _bll.DiscFromPages.FirstOrDefaultAsync(id.Value);
            if (discFromPage == null)
            {
                return NotFound();
            }
            ViewData["DiscId"] = new SelectList(await _bll.Discs.GetAllAsync(), "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(await _bll.Prices.GetAllAsync(), "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(await _bll.Websites.GetAllAsync(), "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // POST: DiscFromPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DiscFromPage discFromPage)
        {
            if (id != discFromPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.DiscFromPages.Update(discFromPage);
                    await _bll.SaveChangesAsync();
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
            ViewData["DiscId"] = new SelectList(await _bll.Discs.GetAllAsync(), "Id", "Name", discFromPage.DiscId);
            ViewData["PriceId"] = new SelectList(await _bll.Prices.GetAllAsync(), "Id", "Iso", discFromPage.PriceId);
            ViewData["WebsiteId"] = new SelectList(await _bll.Websites.GetAllAsync(), "Id", "Url", discFromPage.WebsiteId);
            return View(discFromPage);
        }

        // GET: DiscFromPage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discFromPage = await _bll.DiscFromPages.FirstOrDefaultAsync(id.Value);
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
            var discFromPage = await _bll.DiscFromPages.FirstOrDefaultAsync(id);
            if (discFromPage != null)
            {
                _bll.DiscFromPages.Remove(discFromPage);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscFromPageExists(Guid id)
        {
            return _bll.DiscFromPages.Exists(id);
        }
    }
}
