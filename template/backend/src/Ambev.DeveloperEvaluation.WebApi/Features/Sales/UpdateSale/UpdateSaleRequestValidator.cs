using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleRequest.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty().WithMessage("Sale ID is required.");

        RuleFor(s => s.SaleNumber)
            .GreaterThan(0).WithMessage("Sale number must be greater than 0.");

        RuleFor(s => s.SaleDate)
            .NotEmpty().WithMessage("Sale date is required.");

        RuleFor(s => s.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(s => s.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to 0.");

        RuleFor(s => s.Branch)
            .NotEmpty().WithMessage("Branch is required.")
            .MaximumLength(100).WithMessage("Branch must not exceed 100 characters.");

        RuleFor(s => s.Items)
            .NotEmpty().WithMessage("At least one sale item is required.");
    }
}