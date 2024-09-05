using Test_ex.DTO;

namespace Test_ex.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<DoctorDto> GetById(long id);
        public Task<DoctorDto> Create(DoctorUpdateDto dto);
        public Task<DoctorUpdateDto> Update(long id,
            DoctorUpdateDto dto);
        public Task<bool> Delete(long id);
        public Task<IEnumerable<DoctorDto>> GetAll();
        public Task<IEnumerable<DoctorDto>> Sort(string? sortOrder);
        public Task<int> Count();
    }
}
