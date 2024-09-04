using Test_ex.Models;

namespace Test_ex.Repositories.Interfaces
{
    public interface IDoctorRepo
    {
        public Task<Doctor> GetById(long id);
        public Task<IEnumerable<Doctor>> GetAll();
        public Task Add(Doctor doctor);
        public Task Update(Doctor doctor);
        public Task Delete(long id);
    }
}
