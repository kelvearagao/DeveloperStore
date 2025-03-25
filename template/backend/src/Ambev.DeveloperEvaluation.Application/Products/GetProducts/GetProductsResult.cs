using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// API response model for GetProducts operation
/// </summary>
public class GetProductsResult
{
    public IEnumerable<ProductResult> Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class ProductResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public Rating Rating { get; set; }
}

