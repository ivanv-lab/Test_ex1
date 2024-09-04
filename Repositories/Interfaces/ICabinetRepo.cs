using Test_ex.Models;

namespace Test_ex.Repositories.Interfaces
{
    public interface ICabinetRepo
    {
        public Task<Cabinet> GetById(long id);
        public Task<IEnumerable<Cabinet>> GetAll();
        public Task Add(Cabinet cabinet);
        public Task Update(Cabinet cabinet);
        public Task Delete(long id);
    }
}
