using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class WishlistController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Wishlist, App.BLL.DTO.Wishlist> _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="userManager"></param>
        /// <param name="autoMapper"></param>

        public WishlistController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Wishlist, App.BLL.DTO.Wishlist>(autoMapper);

        }

        /// <summary>
        /// Get all wishlists
        /// </summary>
        /// <returns></returns>

        // GET: api/Wishlist
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Wishlist>>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
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
    }
}
