using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    public bool IsCancelled { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}