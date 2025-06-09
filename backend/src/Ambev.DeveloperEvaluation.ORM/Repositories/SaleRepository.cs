using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task<Sale?> FindByIdAsync(Guid id)
        {
            // Use Include to ensure that related items are loaded along with the sale (Eager Loading).
            return await _context.Sales
                                 .Include(s => s.Items)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            // Returns the list of sales without tracking changes for performance.
            // Orders by most recent date.
            return await _context.Sales
                                 .AsNoTracking()
                                 .OrderByDescending(s => s.Date)
                                 .ToListAsync();
        }
    }
}
