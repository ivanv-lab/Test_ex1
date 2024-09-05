using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Test_ex.DTO;
using Test_ex.Models;

namespace Test_ex.Maps
{
    public class DoctorMap:IMapper<Doctor,DoctorDto>
    {
        private readonly AppDbContext _context;
        public DoctorMap(AppDbContext context)
        {
            _context = context;
        }

        public Doctor Map(DoctorDto dto)
        {
            return new Doctor
            {
                Fullname = dto.Fullname,
                CabinetId = _context.Doctors
                .Where(d => d.Cabinet.Number == dto.CabinetNumber)
                .FirstOrDefaultAsync()
                .Result.CabinetId,
                RegionId = _context.Doctors
                .Where(d => d.Region.Number == dto.RegionNumber)
                .FirstOrDefaultAsync()
                .Result.RegionId,
                SpecializationId = _context.Doctors
                .Where(d => d.Specialization.Name == dto.SpecializationName)
                .FirstOrDefaultAsync()
                .Result.SpecializationId
            };
        }

        public DoctorDto Map(Doctor model)
        {
            return new DoctorDto
            {
                Fullname = model.Fullname,
                RegionNumber = _context.Regions
                .Where(r => r.Id == model.RegionId)
                .FirstOrDefaultAsync().Result.Number,
                CabinetNumber = _context.Cabinets
                .Where(c => c.Id == model.CabinetId)
                .FirstOrDefaultAsync().Result.Number,
                SpecializationName = _context.Specializations
                .Where(s => s.Id == model.SpecializationId)
                .FirstOrDefaultAsync().Result.Name
            };
        }

        public IEnumerable<DoctorDto> MapList(IEnumerable<Doctor> models)
        {
            List<DoctorDto> result=new List<DoctorDto>();
            foreach(var model in models)
            {
                result.Add(Map(model));
            }
            return result;
        }

        public Doctor UpdateMap(Doctor model, DoctorDto dto)
        {
            model.Fullname = dto.Fullname;
            model.SpecializationId = _context.Doctors
                .Where(d => d.Specialization.Name == dto.SpecializationName)
                .FirstOrDefaultAsync()
                .Result.SpecializationId;
            model.CabinetId = _context.Doctors
                .Where(d => d.Cabinet.Number == dto.CabinetNumber)
                .FirstOrDefaultAsync()
                .Result.CabinetId;
            model.RegionId = _context.Doctors
                .Where(d => d.Region.Number == dto.RegionNumber)
                .FirstOrDefaultAsync()
                .Result.RegionId;
            return model;

        }
    }
}
