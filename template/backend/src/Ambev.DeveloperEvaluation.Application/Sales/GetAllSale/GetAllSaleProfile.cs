using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

namespace Ambev.DeveloperEvaluation.Application.Saless.GetAllSale;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleResponse
/// </summary>
public class GetAllSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale operation
    /// </summary>
    public GetAllSaleProfile()
    {
        CreateMap<Sale, SaleResult>();
    }
}
