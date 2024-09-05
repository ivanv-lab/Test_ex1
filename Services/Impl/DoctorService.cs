using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Interfaces;

namespace Test_ex.Services.Impl
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorRepo _repo;
        private readonly IMapper<Doctor, DoctorDto> _mapper;
        public DoctorService(IDoctorRepo repo, IMapper<Doctor, DoctorDto> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Count()
        {
            int count=await _repo.Count();
            return count;
        }

        public async Task<DoctorDto> Create(DoctorUpdateDto dto)
        {
            var doctor = new Doctor
            {
                Fullname = dto.Fullname,
                CabinetId = dto.CabinetId,
                SpecializationId = dto.SpecializationId,
                RegionId = dto.RegionId
            };
            await _repo.Add(doctor);
            return _mapper.Map(doctor);
        }

        public async Task<bool> Delete(long id)
        {
            var doctor = await _repo.GetById(id);
            if (doctor != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<DoctorDto>> GetAll()
        {
            var doctors=await _repo.GetAll();
            return _mapper.MapList(doctors);
        }

        public async Task<DoctorDto> GetById(long id)
        {
            var doctor=await _repo.GetById(id);
            if(doctor==null)
                throw new Exception($"Объект с Id {id} не найден");
            return _mapper.Map(doctor);
        }

        public async Task<IEnumerable<DoctorDto>> Sort(string? sortOrder)
        {
            var doctors = await _repo.GetAll();

            switch (sortOrder)
            {
                case "name":
                    doctors=doctors.OrderBy(d=>d.Fullname)
                        .ToList(); break;
                case "name_desc":
                    doctors=doctors.OrderByDescending(d=>d.Fullname)
                        .ToList(); break;
                case "region":
                    doctors=doctors.OrderBy(d=>Convert.ToInt32(d.Region.Number))
                        .ToList(); break;
                case "region_desc":
                    doctors=doctors.OrderByDescending(d=> Convert.ToInt32(d.Region.Number))
                        .ToList(); break;
                case "cabinet":
                    doctors=doctors.OrderBy(d=>d.Cabinet.Number)
                        .ToList(); break;
                case "cabinet_desc":
                    doctors=doctors.OrderByDescending(d=>d.Cabinet.Number)
                        .ToList(); break;
                case "spec":
                    doctors=doctors.OrderBy(d=>d.Specialization.Name)
                        .ToList(); break;
                case "spec_desc":
                    doctors = doctors.OrderByDescending(d => d.Specialization.Name)
                        .ToList(); break;
                default:
                    doctors=doctors.OrderByDescending(d=>d.Id)
                        .ToList(); break;
            }
            return _mapper.MapList(doctors);
        }

        public async Task<DoctorUpdateDto> Update(long id, DoctorUpdateDto dto)
        {
            var updateDoctor=await _repo.GetById(id);
            if(updateDoctor==null)
                throw new Exception($"Объект с Id {id} не найден");
            
            updateDoctor.Fullname = dto.Fullname;
            updateDoctor.SpecializationId = dto.SpecializationId;
            updateDoctor.RegionId = dto.RegionId;
            updateDoctor.CabinetId = dto.CabinetId;

            await _repo.Update(updateDoctor);
            return new DoctorUpdateDto
            {
                Fullname = dto.Fullname,
                SpecializationId = dto.SpecializationId,
                RegionId = dto.RegionId,
                CabinetId = dto.CabinetId
            };
        }
    }
}
