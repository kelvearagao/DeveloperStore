using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(
        IMapper mapper,
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        ILogger<CreateSaleHandler> logger)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateSaleCommand for UserId: {UserId}", request.UserId);

        // Validate the command
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.Detail);
            _logger.LogWarning("Validation failed for CreateSaleCommand: {Errors}", errorMessages);
            throw new ValidationException(errorMessages);
        }

        // Validate if the user exists
        var userExists = await _userRepository.ExistsAsync(request.UserId, cancellationToken);
        if (!userExists)
        {
            _logger.LogWarning("User with Id {UserId} does not exist", request.UserId);
            throw new ValidationException(new[] { "The specified User does not exist." });
        }

        // Fetch products from the database
        var productIds = request.Items.Select(item => item.ProductId).Distinct();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

        if (products.Count() != productIds.Count())
        {
            _logger.LogWarning("One or more products in the request do not exist.");
            throw new ValidationException(new[] { "One or more products in the request do not exist." });
        }

        var sale = _mapper.Map<Sale>(request);

        // Calculate the total price based on the items and apply percentage discounts
        sale.TotalAmount = request.Items.Sum(item =>
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
            {
                throw new ValidationException(new[] { $"Product with Id {item.ProductId} does not exist." });
            }

            var itemTotal = item.Quantity * product.Price; // Use the product price from the database
            var discountAmount = itemTotal * (item.Discount / 100); // Calculate discount as a percentage
            return itemTotal - discountAmount;
        });

        _logger.LogInformation("Calculated TotalAmount for Sale: {TotalAmount}", sale.TotalAmount);

        // Add the sale to the repository
        await _saleRepository.AddAsync(sale, cancellationToken);
        _logger.LogInformation("Sale created successfully with Id: {SaleId}", sale.Id);

        // Log SaleCreated event
        var saleCreatedEvent = new
        {
            SaleId = sale.Id,
            SaleNumber = sale.SaleNumber,
            TotalAmount = sale.TotalAmount,
            CreatedAt = DateTime.UtcNow
        };
        _logger.LogInformation("SaleCreatedEvent: {@SaleCreatedEvent}", saleCreatedEvent);

        return _mapper.Map<CreateSaleResult>(sale);
    }
}