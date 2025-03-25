using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Repositories;

public class SaleRepository: ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Set<Sale>().AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Sale>()
            .Include(s => s.Items) 
            .FirstOrDefaultAsync(s => s.Id == saleId, cancellationToken);
    }

    public async Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Sale>()
            .Include(s => s.Items)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Sale> Sales, int TotalCount)> GetAllAsync(
        int pageNumber, 
        int pageSize, 
        string orderBy, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.AsQueryable();

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

        var sales = await query
            .Include(s => s.Items) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (sales, totalCount);
    }

    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Set<Sale>().Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(saleId, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}