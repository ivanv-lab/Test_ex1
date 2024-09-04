using Test_ex.DTO;

namespace Test_ex.Services.Interfaces
{
    public interface IRegionService
    {
        public Task<RegionDto> GetById(long id);
        public Task<RegionDto> Create(RegionDto dto);
        public Task<RegionDto> Update(long id,
            RegionDto dto);
        public Task<bool> Delete(long id);
        public Task<IEnumerable<RegionDto>> GetAll();
        public Task<IEnumerable<RegionDto>> Sort(string? sortOrder);
        public Task<int> Count();
    }
}
