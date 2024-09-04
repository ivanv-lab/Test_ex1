using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Interfaces;

namespace Test_ex.Services.Impl
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepo _repo;
        private readonly IMapper<Region, RegionDto> _mapper;
        public RegionService(IRegionRepo repo,
            IMapper<Region,RegionDto> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Count()
        {
            int count = await _repo.Count();
            return count;
        }

        public async Task<RegionDto> Create(RegionDto dto)
        {
            var region = _mapper.Map(dto);
            await _repo.Add(region);
            return _mapper.Map(region);
        }

        public async Task<bool> Delete(long id)
        {
            var region=await _repo.GetById(id);
            if(region != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<RegionDto>> GetAll()
        {
            var regions = await _repo.GetAll();
            return _mapper.MapList(regions);
        }

        public async Task<RegionDto> GetById(long id)
        {
            var region = await _repo.GetById(id);
            if (region == null)
                throw new Exception($"Объект с Id {id} не найден");
            return _mapper.Map(region);
        }

        public async Task<IEnumerable<RegionDto>> Sort(string? sortOrder)
        {
            var regions=await _repo.GetAll();

            switch (sortOrder)
            {
                case "number":
                    regions=regions.OrderBy(r=>r.Number)
                        .ToList(); 
                    break;
                case "number_desc":
                    regions = regions.OrderByDescending(r => r.Number)
                        .ToList();
                    break;
                default:
                    regions=regions.OrderByDescending(r=>r.Id)
                        .ToList();
                    break;
            }
            return _mapper.MapList(regions);
        }

        public async Task<RegionDto> Update(long id, RegionDto dto)
        {
            var updateRegion = await _repo.GetById(id);
            if(updateRegion == null)
                throw new Exception($"Объект с Id {id} не найден");
            updateRegion = _mapper.UpdateMap(updateRegion, dto);
            await _repo.Update(updateRegion);
            return _mapper.Map(updateRegion);
        }
    }
}
