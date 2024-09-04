using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;

namespace Test_ex.Repositories.Impl
{
    public class SpecializationRepo : ISpecializationRepo
    {
        private readonly AppDbContext _context;
        public SpecializationRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Specialization specialization)
        {
            await _context.Specializations.AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var spec=await _context.Specializations
                .Where(s=>!s.IsDeleted
                && s.Id==id)
                .FirstOrDefaultAsync();
            spec.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Specialization>> GetAll()
        {
            return await _context.Specializations
                .Where(s=>!s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Specialization> GetById(long id)
        {
            var spec = await _context.Specializations
                .Where(s => !s.IsDeleted
                && s.Id == id)
                .FirstOrDefaultAsync();
            if (spec == null)
                return null; 
            return spec;
            //return await _context.Specializations
            //    .Where(s => !s.IsDeleted
            //    && s.Id == id)
            //    .FirstOrDefaultAsync();
        }

        public async Task Update(Specialization specialization)
        {
            _context.Entry(specialization).State
                = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task<int> Count()
        {
           return _context.Specializations.CountAsync();
        }
    }
}
