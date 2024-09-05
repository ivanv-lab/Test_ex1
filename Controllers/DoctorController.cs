using Microsoft.AspNetCore.Mvc;
using Test_ex.DTO;
using Test_ex.Services.Interfaces;

namespace Test_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController:ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors=await _service.GetAll();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var doctor=await _service.GetById(id);
            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(long id,
            DoctorUpdateDto doctorUpdateDto)
        {
            var updateDoctor=await _service.Update(id, doctorUpdateDto);
            return Ok(updateDoctor);
        }

        [HttpPost]
        public async Task<IActionResult> PostDoctor(DoctorUpdateDto doctorDto)
        {
            var newDoctor=await _service.Create(doctorDto);
            return Ok(newDoctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(long id)
        {
            bool res=await _service.Delete(id);
            if(res)
                return Ok();
            return BadRequest();
        }

        [HttpGet("/doctors/sort")]
        public async Task<IActionResult> SortDoctors(
            [FromQuery] string? sortOrder,
            [FromQuery] int page = 1)
        {
            if (page <= 0)
                return BadRequest("Номер страницы должен " +
                    "быть больше нуля");

            const int pageSize = 10;

            var doctors = await _service.Sort(sortOrder);
            doctors=doctors.Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList();
            var count = await _service.Count();
            var response = new
            {
                items = doctors,
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
