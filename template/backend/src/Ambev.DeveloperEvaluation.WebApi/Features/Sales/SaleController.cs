using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

/// <summary>
/// Controller for managing sales.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SaleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The request containing sale details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale response.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var command = _mapper.Map<CreateSaleCommand>(request);

        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<CreateSaleResponse>(result);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = response
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale if found, or NotFound if not.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetSaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetSaleRequest { Id = id };
            var validator = new GetSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetSaleCommand>(request.Id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound("Sale not found");
            }

            var response = _mapper.Map<GetSaleResponse>(result);

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = response
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    /// <summary>
    /// Retrieves a GetAllSale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the GetAllSale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The GetAllSale details if found</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<GetAllSaleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGetAllSales(
        [FromQuery(Name = "_page")] int pageNumber = 1, 
        [FromQuery(Name = "_size")] int pageSize = 10,
        [FromQuery(Name = "_order")] string? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetAllSaleRequest { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy };
            var validator = new GetAllSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetAllSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetAllSaleResponse>
            {
                Success = true,
                Message = "GetAllSale retrieved successfully",
                Data = _mapper.Map<GetAllSaleResponse>(response)
            });
        } catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }

    /// <summary>
    /// Deletes a Sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Sale was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to update.</param>
    /// <param name="request">The request containing updated sale details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated sale response.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateSaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;

        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound("Sale not found");
            }

            var response = _mapper.Map<UpdateSaleResponse>(result);

            return Ok(new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = true,
                Message = "Sale updated successfully",
                Data = response
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
}