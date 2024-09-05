using Microsoft.AspNetCore.Mvc;
using Test_ex.DTO;
using Test_ex.Services.Interfaces;

namespace Test_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAll();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var patient = await _service.GetById(id);
            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(long id,
            PatientUpdateDto patientUpdateDto)
        {
            var updatePatient=await _service.Update(id, patientUpdateDto);
            return Ok(updatePatient);
        }

        [HttpPost]
        public async Task<IActionResult> PostPatient(PatientUpdateDto patientDto)
        {
            var newPatient=await _service.Create(patientDto);
            return Ok(newPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(long id)
        {
            bool res=await _service.Delete(id);
            if(res)
                return Ok();
            return BadRequest();
        }

        [HttpGet("/patients/sort")]
        public async Task<IActionResult> SortPatients(
            [FromQuery] string? sortOrder,
            [FromQuery] int page = 1)
        {
            if (page <= 0)
                return BadRequest("Номер страницы должен " +
                    "быть больше нуля");

            const int pageSize = 10;

            var patients = await _service.Sort(sortOrder);
            patients=patients.Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList();
            var count = await _service.Count();
            var response = new
            {
                items = patients,
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
