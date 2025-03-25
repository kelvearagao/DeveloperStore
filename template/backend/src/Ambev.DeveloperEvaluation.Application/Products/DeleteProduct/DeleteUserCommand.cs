using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Command for deleting a user
/// </summary>
public record DeleteProductCommand : IRequest<DeleteProductResponse>
{
    /// <summary>
    /// The unique identifier of the Product to delete
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteProductCommand
    /// </summary>
    /// <param name="id">The ID of the Product to delete</param>
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
