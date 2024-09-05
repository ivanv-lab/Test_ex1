using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Interfaces;

namespace Test_ex.Services.Impl
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _repo;
        private readonly IMapper<Patient,PatientDto> _mapper;
        public PatientService(IPatientRepo repo, IMapper<Patient, PatientDto> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Count()
        {
            int count=await _repo.Count();
            return count;
        }

        public async Task<PatientDto> Create(PatientUpdateDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                Lastname = dto.Lastname,
                Address = dto.Address,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                RegionId = dto.RegionId,
                Surname = dto.Surname
            };
            await _repo.Add(patient);
            return _mapper.Map(patient);
        }

        public async Task<bool> Delete(long id)
        {
            var patient=await _repo.GetById(id);
            if (patient != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PatientDto>> GetAll()
        {
            var patients=await _repo.GetAll();
            return _mapper.MapList(patients);
        }

        public async Task<PatientDto> GetById(long id)
        {
            var patient = await _repo.GetById(id);
            if (patient == null)
                throw new Exception($"Объект с Id {id} не найден");
            return _mapper.Map(patient);
        }

        public async Task<IEnumerable<PatientDto>> Sort(string? sortOrder)
        {
            var patients=await _repo.GetAll();

            switch (sortOrder)
            {
                case "surname":
                    patients = patients.OrderBy(c=>c.Surname)
                        .ToList(); break;
                case "surname_desc":
                    patients = patients.OrderByDescending(c=>c.Surname)
                        .ToList(); break;
                case "name":
                    patients = patients.OrderBy(c=>c.Name)
                        .ToList(); break;
                case "name_desc":
                    patients = patients.OrderByDescending(c=>c.Name)
                        .ToList(); break;
                case "lastname":
                    patients = patients.OrderBy(c=>c.Lastname)
                        .ToList(); break;
                case "lastname_desc":
                    patients = patients.OrderByDescending(c=>c.Lastname)
                        .ToList(); break;
                case "address":
                    patients = patients.OrderBy(c=>c.Address)
                        .ToList(); break;
                case "address_desc":
                    patients = patients.OrderByDescending(c => c.Address)
                        .ToList(); break;
                case "date":
                    patients = patients.OrderBy(c=>c.BirthDate)
                        .ToList(); break;
                case "date_desc":
                    patients = patients.OrderByDescending(c=>c.BirthDate)
                        .ToList(); break;
                case "gender":
                    patients = patients.OrderBy(c=>c.Gender)
                        .ToList(); break;
                case "gender_desc":
                    patients = patients.OrderByDescending(c=>c.Gender)
                        .ToList(); break;
                case "region":
                    patients = patients.OrderBy(c=>c.Region.Number)
                        .ToList(); break;
                case "region_desc":
                    patients = patients.OrderByDescending(c => c.Region.Number)
                        .ToList(); break;
                default:
                    patients=patients.OrderByDescending(c=>c.Id)
                        .ToList(); break;
            }
            return _mapper.MapList(patients);
        }

        public async Task<PatientUpdateDto> Update(long id, PatientUpdateDto dto)
        {
            var updatePatient = await _repo.GetById(id);
            if(updatePatient == null)
                throw new Exception($"Объект с Id {id} не найден");

            updatePatient.Name = dto.Name;
            updatePatient.BirthDate = dto.BirthDate;
            updatePatient.Gender = dto.Gender;
            updatePatient.Surname = dto.Surname;
            updatePatient.Lastname = dto.Lastname;
            updatePatient.Address = dto.Address;
            updatePatient.RegionId = dto.RegionId;

            await _repo.Update(updatePatient);
            return new PatientUpdateDto
            {
                Surname = dto.Surname,
                Name = dto.Name,
                Lastname = dto.Lastname,
                Address = dto.Address,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                RegionId = dto.RegionId
            };
        }
    }
}
