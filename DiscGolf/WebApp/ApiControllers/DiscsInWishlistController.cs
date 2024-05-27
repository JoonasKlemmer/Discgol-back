using System.Net;
using App.Contracts.BLL;
using App.Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace WebApp.ApiControllers
{ /// <summary>
  /// api for discsInWishlist
  /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    
    public class DiscsInWishlistController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _iMapper;
/// <summary>
/// Consturctor for the api
/// </summary>
/// <param name="bll"></param>
/// <param name="userManager"></param>
/// <param name="iMapper"></param>
        public DiscsInWishlistController(IAppBLL bll,UserManager<AppUser> userManager, IMapper iMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _iMapper = iMapper;
        }

        // GET: api/DiscsInWishlist
        /// <summary>
        /// Get all discs from wishlist that belong to the user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscsInWishlist>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
 
        public async Task<ActionResult<IEnumerable<DiscsInWishlist>>> GetDiscInWishlist()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var res = (await _bll.DiscsInWishlists.GetAllWithDetails(Guid.Parse(userId))).ToList();
            var result = res.Select(disc => _iMapper.Map<DiscWithDetails>(disc));

            return Ok(result);

        }
        
        // PUT: api/discsinwishlist/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add disc to the wishlist
        /// </summary>
        /// <param name="wishlistInfo">Contains DiscFromPageId and WishlistID, adds disc to the users wishlist</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<OkObjectResult> PostDiscInWishlist([FromBody]WishlistInfo wishlistInfo)
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
                return new OkObjectResult(discsInWishlist);
            }
            
            return Ok(NoContent());
        }
        /// <summary>
        /// Delete disc from wishlist
        /// </summary>
        /// <param name="discInWishlistId"></param>
        /// <returns></returns>
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

            await _bll.DiscsInWishlists.RemoveAsync(discInWishlist);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Checks if disc is already in wishlist
        /// </summary>
        /// <param name="discFromPageId"></param>
        /// <returns></returns>
        [HttpGet("{discFromPageId}")]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscsInWishlist>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
 
        public async Task<ActionResult<bool>> DiscInWishlist(string discFromPageId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishlists = (await _bll.Wishlists.GetAllSortedAsync(Guid.Parse(userId))).ToList();
            var wishlistId = wishlists[0].Id;

            var res = await _bll.DiscsInWishlists.GetDiscInWishlistById(Guid.Parse(discFromPageId), wishlistId );
           

            return Ok(res);

        }
        
        
        
}
        
}
