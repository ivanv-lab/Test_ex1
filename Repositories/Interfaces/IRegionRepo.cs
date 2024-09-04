using Test_ex.Models;

namespace Test_ex.Repositories.Interfaces
{
    public interface IRegionRepo
    {
        public Task<Region> GetById(long id);
        public Task<IEnumerable<Region>> GetAll();
        public Task Add(Region region);
        public Task Update(Region region);
        public Task Delete(long id);
        public Task<int> Count();
    }
}
