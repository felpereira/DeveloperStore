using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for the repository that will handle the persistence of the Product aggregate.
    /// </summary>
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product?> FindByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    }
}
