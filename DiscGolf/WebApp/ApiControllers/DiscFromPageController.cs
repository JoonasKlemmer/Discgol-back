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
using App.Domain;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscFromPageController : ControllerBase
    {
        private readonly IAppBLL _bll;

        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>
            _discWithDetailsMapper;

        public DiscFromPageController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _discWithDetailsMapper =
                new PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>(autoMapper);
        }

        // GET: api/DiscFromPage
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

        // GET: api/DiscFromPage/{discName}
        [HttpGet("{discName}")]
        [ProducesResponseType<Manufacturer>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Manufacturer>> GetDiscsByName(string discName)
        {
            Console.WriteLine("SIIN SEEEES");
            var bllDiscs = (await _bll.DiscFromPages.GetAllWithDetailsByName(discName)).ToList();
            var res = await _bll.DiscFromPages.GetAllDiscData(bllDiscs);
            var result = res.Select(e => _discWithDetailsMapper.Map(e)).ToList();

            return Ok(result);
        }
    }
}
