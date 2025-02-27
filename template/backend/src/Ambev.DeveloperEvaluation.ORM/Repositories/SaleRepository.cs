using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository {
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context) {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default) {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Retrieves a sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return await _context.Sales.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var sale = await GetByIdAsync(id, cancellationToken);
        if(sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Updates a sale from the database
    /// </summary>
    /// <param name="sale"> The sale to be updated </param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default) {
        sale.UpdatedAt = DateTime.UtcNow;
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Get's a <see cref="IQueryable{Sale}"/> with filtering and sorting
    /// </summary>
    /// <param name="sortBy">The property to sort the query by</param>
    /// <param name="isDescending">If the query is to be sorted descending</param>
    /// <param name="filter">The filter to be applied to the query</param>
    /// <returns>The <see cref="IQueryable"/></returns>
    public IQueryable<Sale> GetQuery(string sortBy, bool isDescending = false, SaleFilter? filter = null) {
        var query = _context.Sales.AsQueryable();

        if(filter != null) {
            if(!string.IsNullOrEmpty(filter.Number)) query = query.Where(query => query.Number.ToLower().Contains(filter.Number.ToLower()));
        }

        switch(sortBy) {
            case "createdAt":
                query = isDescending ? query.OrderByDescending(s => s.CreatedAt) : query.OrderBy(s => s.CreatedAt);
                break;
            case "number":
            default:
                query = isDescending ? query.OrderByDescending(s => s.Number) : query.OrderBy(s => s.Number);
                break;
        }

        return query;
    }
}
