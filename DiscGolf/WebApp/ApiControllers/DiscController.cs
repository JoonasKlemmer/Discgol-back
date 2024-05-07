
using System.ComponentModel;

using System.Net;

using App.Contracts.BLL;

using Microsoft.AspNetCore.Mvc;

using App.DTO.v1_0;
using Asp.Versioning;
using AutoMapper;
using Newtonsoft.Json;
using WebApp.Helpers;
using WebApp.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Manufacturer = App.DAL.DTO.Manufacturer;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Disc, App.BLL.DTO.Disc> _mapper;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category> _categoryMapper;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Manufacturer, App.BLL.DTO.Manufacturer> _manufacturerMapper;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscFromPage, App.BLL.DTO.DiscFromPage> _pageMapper;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Website, App.BLL.DTO.Website> _websiteMapper;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>_discWithDetailsMapper ;

        public DiscController(IAppBLL bll,  IMapper autoMapper)
        {
            
            _bll = bll;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Disc, App.BLL.DTO.Disc>(autoMapper);
            _categoryMapper = new PublicDTOBllMapper<App.DTO.v1_0.Category, App.BLL.DTO.Category>(autoMapper);
            _manufacturerMapper = new PublicDTOBllMapper<App.DTO.v1_0.Manufacturer, App.BLL.DTO.Manufacturer>(autoMapper);
            _pageMapper = new PublicDTOBllMapper<App.DTO.v1_0.DiscFromPage, App.BLL.DTO.DiscFromPage>(autoMapper);
            _websiteMapper = new PublicDTOBllMapper<App.DTO.v1_0.Website, App.BLL.DTO.Website>(autoMapper);
            _discWithDetailsMapper = new PublicDTOBllMapper<App.DTO.v1_0.DiscWithDetails, App.BLL.DTO.DiscWithDetails>(autoMapper);
            
        }

        // GET: api/Disc
        [HttpGet]
        [ProducesResponseType<CategoryAttribute>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.DiscWithDetails>>> GetDisc()
        {
            
            var bllDiscs = (await _bll.DiscFromPages.GetAllWithDetails()).ToList();
            var res = await _bll.DiscFromPages.GetAllDiscData(bllDiscs);
            var result = res.Select(e => _discWithDetailsMapper.Map(e)).ToList();
            return Ok(result);
        }
        // GET: api/Disc/5
        [HttpGet("{name}")]
        [ProducesResponseType<CategoryAttribute>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Disc>> GetDisc(string name)
        {
            var disc = await _bll.Discs.GetByName(name);

            if (disc == null)
            {
                return NotFound();
            }

            return Ok(disc);
        }

        // PUT: api/Disc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutDisc(Guid id, Disc disc)
        {
            if (id != disc.Id)
            {
                return BadRequest();
            }

            _context.Entry(disc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Disc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Category>((int) HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<Disc>> PostDisc(Disc disc)
        {
            _bll.Discs.Add(_mapper.Map(disc)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetDisc", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = disc.Id
            }, disc);
        }

        // DELETE: api/Disc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisc(Guid id)
        {
            var disc = await _bll.Discs.FirstOrDefaultAsync(id);
            if (disc == null)
            {
                return NotFound();
            }

            await _bll.Discs.RemoveAsync(disc);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscExists(Guid id)
        {
            return _bll.Discs.Exists(id);
        }
    }
}
