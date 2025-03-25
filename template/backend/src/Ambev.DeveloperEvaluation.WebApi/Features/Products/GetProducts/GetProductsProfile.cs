using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Profile for mapping between Application and API GetProducts responses
/// </summary>
public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for Getroducts feature
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<GetProductsRequest, GetProductsCommand>();
        CreateMap<ProductResult, ProductResponse>();
    }
}
