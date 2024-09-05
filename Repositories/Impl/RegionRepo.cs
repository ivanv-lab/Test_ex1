using Microsoft.EntityFrameworkCore;
using Test_ex.Models;
using Test_ex.Repositories.Interfaces;

namespace Test_ex.Repositories.Impl
{
    public class RegionRepo : IRegionRepo
    {
        private readonly AppDbContext _context;
        public RegionRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var region = await _context.Regions
                .Where(r => !r.IsDeleted
                && r.Id == id)
                .FirstOrDefaultAsync();
            region.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions
                .Where(r=>!r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Region> GetById(long id)
        {
            return await _context.Regions
                .Where(r => !r.IsDeleted
                && r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Region region)
        {
            _context.Entry(region).State
                = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public Task<int> Count()
        {
            return _context.Regions.Where(c => !c.IsDeleted).CountAsync();
        }
    }
}
