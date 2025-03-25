using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for UpdateSaleCommand.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateSaleCommand for SaleId: {SaleId}", request.Id);

        // Validate the command
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.Detail);
            throw new ValidationException(errorMessages);
        }

        // Retrieve the existing sale
        var existingSale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingSale == null)
        {
            throw new ValidationException(new[] { "The specified Sale does not exist." });
        }

        // Check if the sale is being cancelled
        if (request.IsCancelled && !existingSale.IsCancelled)
        {
            var saleCancelledEvent = new
            {
                SaleId = existingSale.Id,
                SaleNumber = existingSale.SaleNumber,
                CancelledAt = DateTime.UtcNow
            };

            _logger.LogInformation("SaleCancelledEvent: {@SaleCancelledEvent}", saleCancelledEvent);
        }

        // Fetch products from the database
        var productIds = request.Items.Select(item => item.ProductId).Distinct();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

        if (products.Count() != productIds.Count())
        {
            _logger.LogWarning("One or more products in the request do not exist.");
            throw new ValidationException(new[] { "One or more products in the request do not exist." });
        }

        // Check if items were removed
        var removedItems = existingSale.Items.Where(item => !request.Items.Any(i => i.Id == item.Id)).ToList();
        foreach (var removedItem in removedItems)
        {
            _logger.LogInformation("Removing Item with Id: {ItemId} from SaleId: {SaleId}", removedItem.Id, existingSale.Id);

            var itemRemovedEvent = new
            {
                SaleId = existingSale.Id,
                ItemId = removedItem.Id,
                ProductId = removedItem.ProductId,
                RemovedAt = DateTime.UtcNow
            };

            _logger.LogInformation("ItemRemovedEvent: {@ItemRemovedEvent}", itemRemovedEvent);
        }

        // Update the sale entity
        _logger.LogInformation("Updating Sale with Id: {SaleId}", existingSale.Id);
        _mapper.Map(request, existingSale);

        // Recalculate the total amount using product prices from the database
        existingSale.TotalAmount = request.Items.Sum(item =>
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
            {
                throw new ValidationException(new[] { $"Product with Id {item.ProductId} does not exist." });
            }

            var itemTotal = item.Quantity * product.Price; 
            var discountAmount = itemTotal * (item.Discount / 100); 
            return itemTotal - discountAmount;
        });

        _logger.LogInformation("Recalculated TotalAmount for SaleId: {SaleId} is {TotalAmount}", existingSale.Id, existingSale.TotalAmount);

        // Save changes to the repository
        await _saleRepository.UpdateAsync(existingSale, cancellationToken);
        _logger.LogInformation("Sale with Id: {SaleId} updated successfully", existingSale.Id);

        // Log the SaleModified event
        var saleModifiedEvent = new
        {
            SaleId = existingSale.Id,
            SaleNumber = existingSale.SaleNumber,
            ModifiedAt = DateTime.UtcNow
        };

        _logger.LogInformation("SaleModifiedEvent: {@SaleModifiedEvent}", saleModifiedEvent);

        var result = _mapper.Map<UpdateSaleResult>(existingSale);
        _logger.LogInformation("UpdateSaleCommand handled successfully for SaleId: {SaleId}", result.Id);

        return result;
    }
}