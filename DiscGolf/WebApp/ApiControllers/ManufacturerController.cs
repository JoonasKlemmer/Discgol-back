using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
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
    public class ManufacturerController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Manufacturer, App.BLL.DTO.Manufacturer> _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="userManager"></param>
        /// <param name="autoMapper"></param>
        public ManufacturerController(IAppBLL bll, UserManager<AppUser> userManager,
            IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Manufacturer, App.BLL.DTO.Manufacturer>(autoMapper);
        }

        /// <summary>
        /// Get all manufacturers
        /// </summary>
        /// <returns></returns>
        // GET: api/Manufacturer
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Manufacturer>>((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Manufacturer>>> GetManufacturer()
        {
            var res = (await _bll.Manufacturers.GetAllAsync()).ToList();
            var result = res.Select(m => _mapper.Map(m));
            return Ok(result);
        }

    }
}
