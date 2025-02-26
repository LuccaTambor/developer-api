using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
public interface ISaleRepository {
    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a sale from the database
    /// </summary>
    /// <param name="sale"> The sale to be updated </param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get's a <see cref="IQueryable{Sale}"/> with filtering and sorting
    /// </summary>
    /// <param name="sortBy">The property to sort the query by</param>
    /// <param name="isDescending">If the query is to be sorted descending</param>
    /// <param name="filter">The filter to be applied to the query</param>
    /// <returns>The <see cref="IQueryable"/></returns>
    IQueryable<Sale> GetQuery(string sortBy, bool isDescending = false, SaleFilter? filter = null);

}
