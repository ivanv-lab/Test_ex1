using Test_ex.DTO;

namespace Test_ex.Services.Interfaces
{
    public interface IPatientService
    {
        public Task<PatientDto> GetById(long id);
        public Task<PatientDto> Create(PatientUpdateDto dto);
        public Task<PatientUpdateDto> Update(long id,
            PatientUpdateDto dto);
        public Task<bool> Delete(long id);
        public Task<IEnumerable<PatientDto>> GetAll();
        public Task<IEnumerable<PatientDto>> Sort(string? sortOrder);
        public Task<int> Count();
    }
}
