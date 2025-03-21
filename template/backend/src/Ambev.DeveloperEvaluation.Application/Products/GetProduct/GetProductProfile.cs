using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Application.Productss.GetProduct;

/// <summary>
/// Profile for mapping between Product entity and GetProductResponse
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
}
