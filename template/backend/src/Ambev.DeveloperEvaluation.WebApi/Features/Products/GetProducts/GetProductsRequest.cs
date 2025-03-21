namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Request model for getting a Product by ID
/// </summary>
public class GetProductsRequest
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
