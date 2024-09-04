using Test_ex.DTO;

namespace Test_ex.Services.Interfaces
{
    public interface ICabinetService
    {
        public Task<CabinetDto> GetById(long id);
        public Task<CabinetDto> Create(CabinetDto dto);
        public Task<CabinetDto> Update(long id,
            CabinetDto dto);
        public Task<bool> Delete(long id);
        public Task<IEnumerable<CabinetDto>> GetAll();
        public Task<IEnumerable<CabinetDto>> Sort(string? sortOrder);
        public Task<int> Count();
    }
}
