using System.Diagnostics;
using Test_ex.DTO;
using Test_ex.Models;

namespace Test_ex.Maps
{
    public class RegionMap : IMapper<Region, RegionDto>
    {
        public Region Map(RegionDto dto)
        {
            return new Region
            {
                Number = dto.Number
            };
        }

        public RegionDto Map(Region model)
        {
            return new RegionDto
            {
                Number = model.Number
            };
        }

        public IEnumerable<RegionDto> MapList(IEnumerable<Region> models)
        {
            List<RegionDto> result = new List<RegionDto>();
            foreach (var model in models)
            {
                result.Add(new RegionDto
                {
                    Number = model.Number
                });
            }
            return result;
        }

        public Region UpdateMap(Region model, RegionDto dto)
        {
            model.Number = dto.Number;
            return model;
        }
    }
}
