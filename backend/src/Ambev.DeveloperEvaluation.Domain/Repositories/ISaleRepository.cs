using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for the repository that will handle the persistence of the Sale aggregate.
    /// This interface is part of the Domain layer and its implementation will be in the Infrastructure layer.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale to the repository.
        /// </summary>
        /// <param name="sale">The sale to be added.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Sale sale);

        /// <summary>
        /// Finds a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The sale's unique identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sale if found; otherwise, null.</returns>
        Task<Sale?> FindByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to be updated.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Sale sale);
        Task<IEnumerable<Sale>> GetAllAsync();
    }
}
