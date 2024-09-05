using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Interfaces;

namespace Test_ex.Services.Impl
{
    public class CabinetService:ICabinetService
    {
        private readonly ICabinetRepo _repo;
        private readonly IMapper<Cabinet, CabinetDto> _mapper;
        public CabinetService(ICabinetRepo repo,
            IMapper<Cabinet,CabinetDto> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Count()
        {
            int count = await _repo.Count();
            return count;
        }

        public async Task<CabinetDto> Create(CabinetDto dto)
        {
            var cabinet = _mapper.Map(dto);
            await _repo.Add(cabinet);
            return _mapper.Map(cabinet);
        }

        public async Task<bool> Delete(long id)
        {
            var cabinet = await _repo.GetById(id);
            if (cabinet != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CabinetDto>> GetAll()
        {
            var cabinets = await _repo.GetAll();
            return _mapper.MapList(cabinets);
        }

        public async Task<CabinetDto> GetById(long id)
        {
            var cabinet = await _repo.GetById(id);
            if (cabinet == null)
                throw new Exception($"Объект с Id {id} не найден");
            return _mapper.Map(cabinet);
        }

        public async Task<IEnumerable<CabinetDto>> Sort(string? sortOrder)
        {
            var cabinets = await _repo.GetAll();

            switch (sortOrder)
            {
                case "number":
                    cabinets=cabinets.OrderBy(c=>c.Number)
                        .ToList();
                    break;
                case "number_desc":
                    cabinets = cabinets.OrderByDescending(c =>c.Number)
                        .ToList();
                    break;
                default:
                    cabinets = cabinets.OrderByDescending(c => c.Id)
                        .ToList();
                    break;
            }
            return _mapper.MapList(cabinets);
        }

        public async Task<CabinetDto> Update(long id, CabinetDto dto)
        {
            var updateCabinet = await _repo.GetById(id);
            if (updateCabinet == null)
                throw new Exception($"Объект с Id {id} не найден");
            updateCabinet = _mapper.UpdateMap(updateCabinet, dto);
            await _repo.Update(updateCabinet);
            return _mapper.Map(updateCabinet);
        }
    }
}
