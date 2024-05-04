using System.ComponentModel;
using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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


        public CategoryController(IAppBLL bll,  IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category>(autoMapper);

        }

        // GET: api/Category
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.DTO.v1_0.Category>>((int) HttpStatusCode.OK)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Category>>> GetCategory()
        {
            var res = await _bll.Categories.GetAllAsync();
            return Ok(res);

        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [ProducesResponseType<CategoryAttribute>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            
             var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
             
            
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _bll.Categories.Update(_mapper.Map(category)!);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Category>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _bll.Categories.Add(_mapper.Map(category)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = category.Id
            }, category);
        }

        // DELETE: api/Category/5
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _bll.Categories.RemoveAsync(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _bll.Categories.Exists(id);
        }
    }
}
