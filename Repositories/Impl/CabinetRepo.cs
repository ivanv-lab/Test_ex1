using Microsoft.EntityFrameworkCore;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;

namespace Test_ex.Repositories.Impl
{
    public class CabinetRepo : ICabinetRepo
    {
        private readonly AppDbContext _context;
        public CabinetRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Cabinet cabinet)
        {
            await _context.Cabinets.AddAsync(cabinet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var cabinet = await _context.Cabinets
                .Where(c => !c.IsDeleted
                && c.Id == id)
                .FirstOrDefaultAsync();
            cabinet.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cabinet>> GetAll()
        {
            return await _context.Cabinets
                .Where(c=>!c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Cabinet> GetById(long id)
        {
            return await _context.Cabinets
                .Where(c => !c.IsDeleted
                && c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Cabinet cabinet)
        {
            _context.Entry(cabinet).State
                = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
