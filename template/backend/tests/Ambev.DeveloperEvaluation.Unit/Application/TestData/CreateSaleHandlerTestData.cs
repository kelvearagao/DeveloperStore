using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - SaleNumber (random positive number)
    /// - SaleDate (current or past date)
    /// - UserId (random GUID)
    /// - Branch (random string)
    /// - Items (valid sale items with quantity, price, and discount)
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.SaleNumber, f => f.Random.Int(1, 10000))
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.UserId, f => Guid.NewGuid())
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.Items, (f, s) => GenerateValidSaleItems().Cast<SaleItem>().ToList());

    /// <summary>
    /// Generates a valid CreateSaleCommand with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateSaleCommand with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a list of valid sale items with randomized data.
    /// Each item will have valid:
    /// - ProductId (random GUID)
    /// - Quantity (1 to 20)
    /// - UnitPrice (random decimal value)
    /// - Discount (percentage based on quantity)
    /// </summary>
    /// <returns>A list of valid sale items.</returns>
    private static List<CreateSaleItemCommand> GenerateValidSaleItems()
    {
        var saleItemFaker = new Faker<CreateSaleItemCommand>()
            .RuleFor(i => i.ProductId, f => Guid.NewGuid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(i => i.UnitPrice, f => f.Finance.Amount(1, 100))
            .RuleFor(i => i.Discount, (f, item) =>
            {
                if (item.Quantity < 4) return 0m;
                if (item.Quantity >= 4 && item.Quantity < 10) return 0.1m;
                if (item.Quantity >= 10 && item.Quantity <= 20) return 0.2m;
                return 0m;
            });

        return saleItemFaker.Generate(new Faker().Random.Int(1, 5)); // Generate 1 to 5 items
    }
}

/// <summary>
/// Represents a sale item in the CreateSaleCommand.
/// </summary>
public class CreateSaleItemCommand
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product being sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the item, represented as a percentage (e.g., 0.1 for 10%).
    /// </summary>
    public decimal Discount { get; set; }
}