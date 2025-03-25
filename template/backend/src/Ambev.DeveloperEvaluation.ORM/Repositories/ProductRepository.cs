using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Product in the database
    /// </summary>
    /// <param name="Product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product</returns>
    public async Task<Product> CreateAsync(Product Product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(Product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Product;
    }

    /// <summary>
    /// Retrieves a Product by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves multiple Products by their unique identifiers
    /// </summary>
    /// <param name="ids">The list of unique identifiers of the Products</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of Products matching the provided identifiers</returns>
    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all Products in a paginated and ordered format
    /// </summary>
    /// <param name="pageNumber">The page number (1-based)</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="orderBy">The property name to order the Products by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of Products for the specified page</returns>
    public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(
        int pageNumber, 
        int pageSize, 
        string orderBy, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderParams = orderBy.Split(',');
            foreach (var param in orderParams)
            {
                var trimmedParam = param.Trim();
                var isDescending = trimmedParam.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
                var propertyName = isDescending 
                    ? trimmedParam[..^5].Trim() 
                    : trimmedParam;

                query = isDescending 
                    ? query.OrderByDescending(p => EF.Property<object>(p, propertyName)) 
                    : query.OrderBy(p => EF.Property<object>(p, propertyName));
            }
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (products, totalCount);
    }

    /// <summary>
    /// Deletes a Product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var Product = await GetByIdAsync(id, cancellationToken);
        if (Product == null)
            return false;

        _context.Products.Remove(Product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
