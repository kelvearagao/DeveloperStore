using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

/// <summary>
/// Command for retrieving a Sales
/// </summary>
public record GetAllSaleCommand : IRequest<GetAllSaleResult>
{
    /// <summary>
    /// The page number for pagination
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of items per page for pagination
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The field to order the results by
    /// </summary>
    public string OrderBy { get; set; }
}
