using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test_ex.DTO;
using Test_ex.Services.Interfaces;

namespace Test_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _service;
        public SpecializationController(ISpecializationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specs = await _service.GetAll();
            return Ok(specs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var spec = await _service.GetById(id);
            
            return Ok(spec);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpec(long id,
            SpecializationDto specializationDto)
        {
            var updateSpec = await _service.Update(id, specializationDto);
            return Ok(updateSpec);
        }

        [HttpPost]
        public async Task<IActionResult> PostSpec(SpecializationDto specializationDto)
        {
            var newSpec = await _service.Create(specializationDto);
            return Ok(newSpec);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpec(long id)
        {
            bool res = await _service.Delete(id);
            if (res)
                return Ok();
            return BadRequest();
        }

        [HttpGet("/sort")]
        public async Task<IActionResult> SortSpec([FromQuery] string? sortOrder,
            [FromQuery] int page=1)
        {
            if(page<=0)
                return BadRequest("Номер страницы должен " +
                    "быть больше нуля");

            const int pageSize = 10;

            var specs = await _service.Sort(sortOrder);
            specs = specs.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var count =await _service.Count();
            var response = new
            {
                items = specs,
                currentPage = page,
                totalPages =
                (int)Math.Ceiling((double)count
                / pageSize),
                totalCount = _service.Count()
            };
         return Ok(response);
        }
    }
}
