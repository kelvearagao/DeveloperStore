using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class GetAllSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetAllSale feature
    /// </summary>
    public GetAllSaleProfile()
    {
        CreateMap<GetAllSaleRequest, GetAllSaleCommand>();
        CreateMap<GetAllSaleResult, GetAllSaleResponse>();
        CreateMap<SaleResult, SaleResponse>();
    }
}
