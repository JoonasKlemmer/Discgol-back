using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using WebApp.Helpers;
namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller for possible category of discs
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category> _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bll">IAppBLL</param>
    /// <param name="autoMapper">AutoMapper</param>
        public CategoryController(IAppBLL bll,  IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category>(autoMapper);

        }

        // GET: api/Category
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of category objects</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Category>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Category>>> GetCategory()
        {
            var res = await _bll.Categories.GetAllAsync();
            var result = res.Select(e => _mapper.Map(e)).ToList();
            return Ok(result);

        }
    }
}
