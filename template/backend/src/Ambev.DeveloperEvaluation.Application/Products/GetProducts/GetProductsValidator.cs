using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Validator for GetProductsCommand
/// </summary>
public class GetProductsValidator : AbstractValidator<GetProductsCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductsCommand
    /// </summary>
    public GetProductsValidator()
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
