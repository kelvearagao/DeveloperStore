using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Adds a new Sale to the repository
    /// </summary>
    /// <param name="sale">The Sale to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Sale by its unique identifier
    /// </summary>
    /// <param name="saleId">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid saleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all Sales
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all Sales</returns>
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all Salse in a paginated and ordered manner
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="orderBy">The field to order the results by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A collection of Sales for the specified page</returns>
    Task<(IEnumerable<Sale> Sales, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing Sale in the repository
    /// </summary>
    /// <param name="sale">The Sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Sale by its unique identifier
    /// </summary>
    /// <param name="saleId">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}