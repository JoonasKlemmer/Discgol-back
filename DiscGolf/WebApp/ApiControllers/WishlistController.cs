using System.Net;
using App.Contracts.BLL;
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
using WebApp.ApiControllers.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class WishlistController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Wishlist, App.BLL.DTO.Wishlist> _mapper;


        public WishlistController(AppDbContext context, IAppBLL bll, UserManager<AppUser> userManager,  IMapper autoMapper)
        {
            _context = context;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Wishlist, App.BLL.DTO.Wishlist>(autoMapper);

        }

        // GET: api/Wishlist
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Wishlist>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Wishlist>>> GetWishlist()
        {
            var userId = _userManager.GetUserId(User);
            
            if (userId == null)
            {
                return Unauthorized();
            }
            
            var res = (await _bll.Wishlists.GetAllSortedAsync(
                    Guid.Parse(userId)
                ))
                .Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);

        }

        // GET: api/Wishlist/5
        [HttpGet("{id}")]
        [ProducesResponseType<Wishlist>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Wishlist>> GetWishlist(Guid id)
        {
            var wishlist = await _context.Wishlist.FindAsync(id);

            if (wishlist == null)
            {
                return NotFound();
            }

            return Ok(wishlist);
        }

        // PUT: api/Wishlist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> PutWishlist(Guid id, Wishlist wishlist)
        {
            if (id != wishlist.Id)
            {
                return BadRequest();
            }

            _context.Entry(wishlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishlistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Wishlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Wishlist>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist wishlist)
        {
            //_context.Wishlist.Add(wishlist);
            _bll.Wishlists.Add(_mapper.Map(wishlist)!);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWishlist", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = wishlist.Id
            }, wishlist);
        }

        // DELETE: api/Wishlist/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> DeleteWishlist(Guid id)
        {
            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishlistExists(Guid id)
        {
            return _context.Wishlist.Any(e => e.Id == id);
        }
    }
}
