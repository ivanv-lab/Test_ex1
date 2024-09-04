using Test_ex.Models;

namespace Test_ex.Repositories.Interfaces
{
    public interface ISpecializationRepo
    {
        public Task<Specialization> GetById(long id);
        public Task<IEnumerable<Specialization>> GetAll();
        public Task Add(Specialization specialization);
        public Task Update(Specialization specialization);
        public Task Delete(long id);
        public Task<int> Count();
    }
}
