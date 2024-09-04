using System.Diagnostics;
using Test_ex.DTO;
using Test_ex.Models;

namespace Test_ex.Maps
{
    public class CabinetMap : IMapper<Cabinet, CabinetDto>
    {
        public Cabinet Map(CabinetDto dto)
        {
            return new Cabinet
            {
                Number = dto.Number
            };
        }

        public CabinetDto Map(Cabinet model)
        {
            return new CabinetDto
            {
                Number = model.Number
            };
        }

        public IEnumerable<CabinetDto> MapList(IEnumerable<Cabinet> models)
        {
            List<CabinetDto> result=new List<CabinetDto>();
            foreach (var model in models)
            {
                result.Add(new CabinetDto
                {
                    Number = model.Number
                });
            }
            return result;
        }

        public Cabinet UpdateMap(Cabinet model, CabinetDto dto)
        {
            model.Number = dto.Number;
            return model;
        }
    }
}
