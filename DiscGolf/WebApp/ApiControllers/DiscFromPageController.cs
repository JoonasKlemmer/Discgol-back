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
        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscFromPage, App.BLL.DTO.DiscFromPage> _mapper;

        public DiscFromPageController(IAppBLL bll,  IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.DiscFromPage, App.BLL.DTO.DiscFromPage>(autoMapper);
        }

        // GET: api/DiscFromPage
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.DiscFromPage>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<DiscFromPage>>> GetDiscsFromPage()
        {
            var res = await _bll.DiscFromPages.GetAllAsync();
            return Ok(res);
        }
    }
}
