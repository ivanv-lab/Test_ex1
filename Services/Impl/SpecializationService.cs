using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Test_ex.DTO;
using Test_ex.Maps;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;
using Test_ex.Services.Interfaces;

namespace Test_ex.Services.Impl
{
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepo _repo;
        private readonly IMapper<Specialization, SpecializationDto> _mapper;
        public SpecializationService(ISpecializationRepo repo, IMapper<Specialization, SpecializationDto> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SpecializationDto> Create(SpecializationDto dto)
        {
            var spec = _mapper.Map(dto);
            await _repo.Add(spec);
            return _mapper.Map(spec);
        }

        public async Task<IEnumerable<SpecializationDto>> GetAll()
        {
            var specs=await _repo.GetAll();
            return _mapper.MapList(specs);
        }

        public async Task<SpecializationDto> GetById(long id)
        {
            var spec=await _repo.GetById(id);
            return _mapper.Map(spec);
        }

        public async Task<IEnumerable<SpecializationDto>> Sort(string? sortOrder)
        {
            var specs = await _repo.GetAll();

            switch (sortOrder)
            {
                case "name":
                    specs=specs.OrderBy(x => x.Name)
                        .ToList();
                    break;
                case "name_desc":
                    specs=specs.OrderByDescending(x => x.Name)
                        .ToList();
                    break;
                default:
                    specs=specs.OrderByDescending(x=>x.Id)
                        .ToList();
                    break;
            }
            return _mapper.MapList(specs);
        }

        public async Task<SpecializationDto> Update(long id, SpecializationDto dto)
        {
            var updateSpec = await _repo.GetById(id);
            updateSpec=_mapper.UpdateMap(updateSpec, dto);
            await _repo.Update(updateSpec);
            return _mapper.Map(updateSpec);
        }

        public async Task<bool> Delete(long id)
        {
            var spec = await _repo.GetById(id);
            if (spec != null)
            {
                await _repo.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<int> Count()
        {
            int count=await _repo.Count();
            return count;
        }
    }
}
