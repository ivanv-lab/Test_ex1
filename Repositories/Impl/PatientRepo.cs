using Microsoft.EntityFrameworkCore;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;

namespace Test_ex.Repositories.Impl
{
    public class PatientRepo : IPatientRepo
    {
        private readonly AppDbContext _context;
        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var patient = await _context.Patients
                .Where(p => !p.IsDeleted
                && p.Id == id)
                .FirstOrDefaultAsync();
            patient.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _context.Patients
                .Where(p=>!p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Patient> GetById(long id)
        {
            return await _context.Patients
                .Where(p => !p.IsDeleted
                && p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Patient patient)
        {
            _context.Entry(patient).State
                = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public Task<int> Count()
        {
            return _context.Patients.CountAsync();
        }
    }
}
