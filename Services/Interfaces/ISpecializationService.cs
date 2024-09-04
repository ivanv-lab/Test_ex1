using Test_ex.DTO;

namespace Test_ex.Services.Interfaces
{
    public interface ISpecializationService
    {
        public Task<SpecializationDto> GetById(long id);
        public Task<SpecializationDto> Create(SpecializationDto dto);
        public Task<SpecializationDto> Update(long id,
            SpecializationDto dto);
        public Task<bool> Delete(long id);
        public Task<IEnumerable<SpecializationDto>> GetAll();
        public Task<IEnumerable<SpecializationDto>> Sort(string? sortOrder);
        public Task<int> Count();
    }
}
