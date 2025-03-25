using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Represents the response returned after successfully get a .
/// </summary>
/// <remarks>
/// This response contains a ,
/// which can be  for subsequent operations or reference.
/// </remarks>
public class GetSaleResult
{
    // <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Gets or sets the date of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    // <summary>
    /// Gets or sets the items included in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();
}
