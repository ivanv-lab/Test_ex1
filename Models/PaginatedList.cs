using Microsoft.EntityFrameworkCore;

namespace Test_ex.Models
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get;private set; }

        public PaginatedList(List<T> items,
            int count, int pageIndex)
        {
            PageIndex = pageIndex;
        }

        public bool HasPreviousPage => PageIndex > 1;
        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize=5)
        {
            var count = await source.CountAsync();
            var items=await source.Skip((pageIndex-1)*
                pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex);
        }
    }
}
