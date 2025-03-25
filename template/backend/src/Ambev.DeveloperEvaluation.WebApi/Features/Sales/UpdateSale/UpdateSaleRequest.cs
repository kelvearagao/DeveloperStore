using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to Update a new Sale in the system.
/// </summary>
public class UpdateSaleRequest
{
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
    /// Gets or sets the user ID associated with the sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of items in the sale.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;
}