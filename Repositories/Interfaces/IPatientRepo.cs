using Test_ex.Models;

namespace Test_ex.Repositories.Interfaces
{
    public interface IPatientRepo
    {
        public Task<Patient> GetById(long id);
        public Task<IEnumerable<Patient>> GetAll();
        public Task Add(Patient patient);
        public Task Update(Patient patient);
        public Task Delete(long id);
    }
}
