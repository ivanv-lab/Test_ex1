using Microsoft.EntityFrameworkCore;
using Test_ex.DTO;
using Test_ex.Models;

namespace Test_ex.Maps
{
    public class PatientMap : IMapper<Patient, PatientDto>
    {
        private readonly AppDbContext _context;
        public PatientMap(AppDbContext context)
        {
            _context = context;
        }
        public Patient Map(PatientDto dto)
        {
            return new Patient
            {
                Name = dto.Name,
                Lastname = dto.Lastname,
                Surname= dto.Surname,
                Address = dto.Address,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                RegionId = _context.Patients
                .Where(p => p.Region.Number == dto.RegionNumber)
                .FirstOrDefault()
                .RegionId
            };
        }

        public PatientDto Map(Patient model)
        {
            return new PatientDto
            {
                Name = model.Name,
                Lastname = model.Lastname,
                Surname= model.Surname,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                RegionNumber = _context.Regions.Where(r => r.Id == model.RegionId).FirstOrDefaultAsync().Result.Number
            };
        }

        public IEnumerable<PatientDto> MapList(IEnumerable<Patient> models)
        {
            List<PatientDto> result = new List<PatientDto>();
            foreach (var model in models)
            {
                result.Add(new PatientDto
                {
                    Name = model.Name,
                    Lastname = model.Lastname,
                    Address = model.Address,
                    Surname = model.Surname,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    RegionNumber = _context.Regions.Where(r => r.Id == model.RegionId).FirstOrDefaultAsync().Result.Number
                });
            }
            return result;
        }

        public Patient UpdateMap(Patient model, PatientDto dto)
        {
            model.Name = dto.Name;
            model.Lastname = dto.Lastname;
            model.Surname = dto.Surname;
            model.Address = dto.Address;
            model.BirthDate = dto.BirthDate;
            model.Gender = dto.Gender;
            model.RegionId = _context.Patients
                .Where(p => p.Region.Number == dto.RegionNumber)
                .FirstOrDefault()
                .RegionId;
            return model;
        }
    }
}
