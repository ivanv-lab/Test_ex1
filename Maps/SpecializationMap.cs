using Microsoft.VisualBasic.FileIO;
using Test_ex.DTO;
using Test_ex.Models;

namespace Test_ex.Maps
{
    public class SpecializationMap : IMapper<Specialization, SpecializationDto>
    {
        public Specialization Map(SpecializationDto dto)
        {
            return new Specialization
            {
                Name = dto.Name
            };
        }

        public SpecializationDto Map(Specialization model)
        {
            return new SpecializationDto
            {
                Name = model.Name
            };
        }

        public IEnumerable<SpecializationDto> MapList(IEnumerable<Specialization> models)
        {
            List<SpecializationDto> result = new List<SpecializationDto>();
            foreach (var model in models)
            {
                result.Add(new SpecializationDto
                {
                    Name = model.Name
                });
            }
            return result;
        }

        public Specialization UpdateMap(Specialization model, SpecializationDto dto)
        {
            model.Name = dto.Name;
            return model;
        }
    }
}
