
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;

/// <summary>
/// Validator for GetAllSaleRequest
/// </summary>
public class GetAllSaleRequestValidator : AbstractValidator<GetAllSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for GetAllSaleRequest
    /// </summary>
    public GetAllSaleRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("_page must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("_size must be greater than or equal to 1.");

        RuleFor(x => x.OrderBy)
            .Matches(@"^(\w+ (asc|desc))(, \w+ (asc|desc))*$")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("_order must follow the format 'field direction[, field direction]'.");
    }
}
