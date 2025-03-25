using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;

/// <summary>
/// Handler for processing GetSaleCommand requests
/// </summary>
public class GetAllSaleHandler : IRequestHandler<GetAllSaleCommand, GetAllSaleResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetAllSaleHandler(
        ISaleRepository SaleRepository,
        IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleCommand request
    /// </summary>
    /// <param name="request">The GetSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale details if found</returns>
    public async Task<GetAllSaleResult> Handle(GetAllSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetAllSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _SaleRepository.GetAllAsync(request.PageNumber, request.PageSize, request.OrderBy, cancellationToken);
        var Sales = result.Sales;
        var totalCount = result.TotalCount;

        if (Sales == null || !Sales.Any())
            return new GetAllSaleResult { Data = Enumerable.Empty<SaleResult>(), TotalItems = 0, TotalPages = 0, CurrentPage = 0 };

        var mappedSales = _mapper.Map<IEnumerable<SaleResult>>(Sales);

        return new GetAllSaleResult
        {
            Data = mappedSales,
            TotalItems = result.TotalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            CurrentPage = request.PageNumber
        };
    }
}
