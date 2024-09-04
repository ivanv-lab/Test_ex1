using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test_ex.DTO;
using Test_ex.Services.Interfaces;

namespace Test_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _service;
        public RegionController(IRegionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _service.GetAll();
            return Ok(regions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var region = await _service.GetById(id);
            return Ok(region);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(long id,
            RegionDto regionDto)
        {
            var updateRegion = await _service
                .Update(id, regionDto);
            return Ok(updateRegion);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegion(RegionDto regionDto)
        {
            var newRegion = await _service.Create(regionDto);
            return Ok(newRegion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(long id)
        {
            bool res = await _service.Delete(id);
            if (res)
                return Ok();
            return BadRequest();
        }

        [HttpGet("/regions/sort")]
        public async Task<IActionResult> SortRegion([FromQuery] string? sortOrder,
            [FromQuery] int page = 1)
        {
            if (page <= 0)
                return BadRequest("Номер страницы должен " +
                   "быть больше нуля");

            const int pageSize = 10;

            var regions = await _service.Sort(sortOrder);
            regions = regions.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var count = await _service.Count();
            var response = new
            {
                items = regions,
                currentPage = page,
                totalPages =
                (int)Math.Ceiling((double)count
                / pageSize),
                totalCount = count
            };
            return Ok(response);
        }
    }
}
