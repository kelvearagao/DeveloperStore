using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Result of the UpdateSale operation.
/// </summary>
public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; } = false;
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();
}