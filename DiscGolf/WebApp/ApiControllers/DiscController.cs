using System.ComponentModel;
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for actions with discs
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Disc, App.BLL.DTO.Disc> _discMapper;

        /// <summary>
        /// Constructor for DiscController
        /// </summary>
        /// <param name="bll">Interface for all the service interfaces</param>
        /// <param name="autoMapper">Mapper to map object from BLL to public DTO</param>
        public DiscController(IAppBLL bll, IMapper autoMapper)
        {

            _bll = bll;
            _discMapper = new PublicDTOBllMapper<App.DTO.v1_0.Disc, App.BLL.DTO.Disc>(autoMapper);

        }

        // GET: api/Disc
        /// <summary>
        /// Get all discs that have been found
        /// </summary>
        /// <returns>List of discs</returns>
        [HttpGet]
        [ProducesResponseType<CategoryAttribute>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.DiscWithDetails>>> GetDisc()
        {


            var res = await _bll.Discs.GetAllDiscs();
            var result = res.Select(e => _discMapper.Map(e)).ToList();
            return Ok(result);
        }
    }
}
