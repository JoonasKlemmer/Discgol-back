using System.Net;
using App.Contracts.BLL;
using App.DTO.v1_0;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for actions with discFromPages service
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscFromPageController : ControllerBase
    {
        private readonly IAppBLL _bll;

        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>
            _discWithDetailsMapper;
/// <summary>
/// Constructor for controller
/// </summary>
/// <param name="bll">access to service</param>
/// <param name="autoMapper">Maps bll to public dto</param>
        public DiscFromPageController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _discWithDetailsMapper =
                new PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>(autoMapper);
        }

        // GET: api/DiscFromPage
        /// <summary>
        /// Get all discFromPages
        /// </summary>
        /// <returns>list of DiscFromPages</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscFromPage>>((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<DiscFromPage>>> GetDiscsFromPage()
        {
            var bllDiscs = (await _bll.DiscFromPages.GetAllWithDetails()).ToList();
            var res = await _bll.DiscFromPages.GetAllDiscData(bllDiscs);
            var result = res.Select(e => _discWithDetailsMapper.Map(e)).ToList();
            return Ok(result);
        }
    /// <summary>
    /// Get all discs from the pages
    /// </summary>
    /// <param name="discId"></param>
    /// <returns>List of found discs </returns>
        // GET: api/DiscFromPage/{discId}
        [HttpGet("{discId}")]
        [ProducesResponseType<Manufacturer>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Manufacturer>> GetDiscsFromPageByDiscId(string discId)
        { 
            var bllDiscs = (await _bll.DiscFromPages.GetWithDetailsByDiscId(Guid.Parse(discId))).ToList();
            var res = await _bll.DiscFromPages.GetAllDiscData(bllDiscs);
            var result = res.Select(e => _discWithDetailsMapper.Map(e)).ToList();
            return Ok(result);

        }
    }
}
