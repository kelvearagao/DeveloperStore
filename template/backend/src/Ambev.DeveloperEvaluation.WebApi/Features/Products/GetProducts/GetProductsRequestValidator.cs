
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Validator for GetUserRequest
/// </summary>
public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    /// <summary>
    /// Initializes validation rules for GetProductsRequest
    /// </summary>
    public GetProductsRequestValidator()
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
