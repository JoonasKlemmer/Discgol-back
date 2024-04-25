using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {

        private readonly IAppUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public WishlistController(IAppUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));

            return View(await _uow.Wishlists.GetAllAsync(userId));
        }

        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _uow.Wishlists.FirstOrDefaultAsync(id.Value);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }
    }
    /*
       // GET: Wishlist/Create
       public async Task<IActionResult> Create()
       {

           ViewData["AppUserId"] = new SelectList(await _uow.Users.GetAllAsync(), "Id", "Id");
           return View();
       }

       // POST: Wishlist/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create( Wishlist wishlist) //Bind("WishlistName,UsersId,AppUserId,Id")]
       {
           if (ModelState.IsValid)
           {
               wishlist.Id = Guid.NewGuid();
               _uow.Wishlists.Add(wishlist);
               await _uow.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
           }
           ViewData["AppUserId"] = new SelectList(await _uow.Users.GetAllAsync(), "Id", "Id", wishlist.AppUserId);
           return View(wishlist);
       }

       // GET: Wishlist/Edit/5
       public async Task<IActionResult> Edit(Guid? id)
       {
           if (id == null)
           {
               return NotFound();
           }

           var wishlist = await _uow.Wishlists.FirstOrDefaultAsync(id.Value);
           if (wishlist == null)
           {
               return NotFound();
           }
           ViewData["AppUserId"] = new SelectList(await _uow.Users.GetAllAsync(), "Id", "Id", wishlist.AppUserId);
           return View(wishlist);
       }

       // POST: Wishlist/Edit/5
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(Guid id,Wishlist wishlist)// [Bind("WishlistName,UsersId,AppUserId,Id")]
       {
           if (id != wishlist.Id)
           {
               return NotFound();
           }

           if (ModelState.IsValid)
           {
               try
               {
                   _uow.Wishlists.Update(wishlist);
                   await _uow.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!await _uow.Wishlists.ExistsAsync(wishlist.Id))
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
           ViewData["AppUserId"] = new SelectList(await _uow.Users.GetAllAsync(), "Id", "Id", wishlist.AppUserId);
           return View(wishlist);
       }

       // GET: Wishlist/Delete/5
       public async Task<IActionResult> Delete(Guid? id)
       {
           if (id == null)
           {
               return NotFound();
           }

           var wishlist = await _uow.Wishlists
               .FirstOrDefaultAsync(id.Value);
           if (wishlist == null)
           {
               return NotFound();
           }

           return View(wishlist);
       }

       // POST: Wishlist/Delete/5
       [HttpPost, ActionName("Delete")]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> DeleteConfirmed(Guid id)
       {
           await _uow.Wishlists.RemoveAsync(id);
           await _uow.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
       }

   }
   */
}
