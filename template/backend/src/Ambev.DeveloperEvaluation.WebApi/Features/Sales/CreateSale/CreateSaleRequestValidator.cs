using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
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
        
        // price rules
        RuleForEach(sale => sale.Items).ChildRules(saleItem =>
        {
            saleItem.RuleFor(item => item.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");

            saleItem.RuleFor(item => item.Discount)
                .Must((item, discount) =>
                {
                    if (item.Quantity < 4) return discount == 0;
                    if (item.Quantity >= 4 && item.Quantity < 10) return discount == 0.1m;
                    if (item.Quantity >= 10 && item.Quantity <= 20) return discount == 0.2m;
                    return false;
                })
                .WithMessage("Invalid discount for the given quantity of items.");
        });
    }
}