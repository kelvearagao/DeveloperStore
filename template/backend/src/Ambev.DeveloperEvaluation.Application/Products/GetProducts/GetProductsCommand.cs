using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Command for retrieving a Products
/// </summary>
public record GetProductsCommand : IRequest<GetProductsResult>
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
