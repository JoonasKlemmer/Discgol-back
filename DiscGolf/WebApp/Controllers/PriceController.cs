
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace WebApp.Controllers
{
    public class PriceController : Controller
    {
        private readonly IAppBLL _bll;

        public PriceController( IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Price
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Prices.GetAllAsync());
        }

        // GET: Price/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Price/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Price price)
        {
            if (ModelState.IsValid)
            {
                price.Id = Guid.NewGuid();
                _bll.Prices.Add(price);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Price/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Currency,Iso,Symbol")] Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Prices.Update(price);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await PriceExists(price.Id))
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
            return View(price);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price != null)
            {
              await _bll.Prices.RemoveAsync(price);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> PriceExists(Guid id)
        {
            return _bll.Prices.ExistsAsync(id);
        }
    }
}
