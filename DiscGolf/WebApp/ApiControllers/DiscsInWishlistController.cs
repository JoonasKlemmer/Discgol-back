using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.DTO;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscsInWishlistController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist> _mapper;

        public DiscsInWishlistController(IAppBLL bll,  IMapper autoMapper,UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist>(autoMapper);
        }

        // GET: api/DiscsInWishlist
        [HttpGet("{wishlistId}")]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscsInWishlist>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
 
        public async Task<ActionResult<IEnumerable<DiscsInWishlist>>> GetDiscInWishlist(string wishlistId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var res = (await _bll.DiscsInWishlists.GetAllWithDetails(Guid.Parse(userId), Guid.Parse(wishlistId)));

            return Ok(res);

        }
        
        // PUT: api/discsinwishlist/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> PostDiscInWishlist([FromBody]WishlistInfo wishlistInfo)  
        {
            if (ModelState.IsValid)
            {
                var discsInWishlist = new App.BLL.DTO.DiscsInWishlist
                {
                    Id = Guid.NewGuid(),
                    DiscFromPageId = Guid.Parse(wishlistInfo.DiscFromPageId),
                    WishlistId = Guid.Parse(wishlistInfo.WishlistId)
                };
                _bll.DiscsInWishlists.Add(discsInWishlist);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return Ok(wishlistInfo);
        }
        
        // DELETE: api/discinwishlist/{discInWishlistId}
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{discInWishlistId}")]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> DeleteDiscInWishlist(Guid discInWishlistId)
        {
            Console.WriteLine(discInWishlistId);
            var discInWishlist = await _bll.DiscsInWishlists.FirstOrDefaultAsync(discInWishlistId);
            if (discInWishlist == null)
            {
                return NotFound();
            }
            Console.WriteLine("------------2------------------------------------");

            await _bll.DiscsInWishlists.RemoveAsync(discInWishlist);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

}
        
}
