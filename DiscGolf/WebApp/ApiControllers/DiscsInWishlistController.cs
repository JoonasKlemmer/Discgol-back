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
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscsInWishlist>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<DiscsInWishlist>>> GetDiscInWishlist()
        {
            var res = await _bll.DiscsInWishlists.GetAllAsync();
           
            return Ok(res);
        }
}
        
}
