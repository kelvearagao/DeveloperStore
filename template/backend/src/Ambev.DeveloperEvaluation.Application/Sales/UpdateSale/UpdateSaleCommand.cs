using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing Sale.
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    public bool IsCancelled { get; set; } = false;

    /// <summary>
    /// Validates the command using the UpdateSaleValidator.
    /// </summary>
    /// <returns>A ValidationResultDetail containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}