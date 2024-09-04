using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;

namespace Test_ex.Repositories.Impl
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _context;
        public DoctorRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var doctor=await _context.Doctors
                .Where(d=>!d.IsDeleted
                && d.Id==id)
                .FirstOrDefaultAsync();
            doctor.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _context.Doctors
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<Doctor> GetById(long id)
        {
            return await _context.Doctors
                .Where(d => !d.IsDeleted
                && d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Doctor doctor)
        {
            _context.Entry(doctor).State
                = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public Task<int> Count()
        {
            return _context.Doctors.CountAsync();
        }
    }
}
