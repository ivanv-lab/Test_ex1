
using Microsoft.AspNetCore.Mvc;
using Test_ex.DTO;
using Test_ex.Services.Interfaces;

namespace Test_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController:ControllerBase
    {
        private readonly ICabinetService _service;
        public CabinetController(ICabinetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cabs = await _service.GetAll();
            return Ok(cabs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cab = await _service.GetById(id);
            return Ok(cab);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabinet(long id,
            CabinetDto cabinetDto)
        {
            var updateCab = await _service.Update(id, cabinetDto);
            return Ok(updateCab);
        }

        [HttpPost]
        public async Task<IActionResult> PostCabinet(CabinetDto cabinetDto)
        {
            var newCab = await _service.Create(cabinetDto);
            return Ok(newCab);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabinet(long id)
        {
            bool res = await _service.Delete(id);
            if (res)
                return Ok();
            return BadRequest();
        }

        [HttpGet("/cabinets/sort")]
        public async Task<IActionResult> SortCabinets(
            [FromQuery] string? sortOrder,
            [FromQuery] int page = 1)
        {
            if (page <= 0)
                return BadRequest("Номер страницы должен " +
                    "быть больше нуля");

            const int pageSize = 10;

            var cabs = await _service.Sort(sortOrder);
            cabs = cabs.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var count = await _service.Count();
            var response = new
            {
                items = cabs,
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
