using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;

/// <summary>
/// Command for uncancelling a sale
/// </summary>
public class UncancelSaleCommand : IRequest<UncancelSaleResult> {
    /// <summary>
    /// The unique identifier of the sale to uncancel
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of UncancelSaleCommand
    /// </summary>
    /// <param name="id">The ID of the sale to uncancel</param>
    public UncancelSaleCommand(Guid id) {
        Id = id;
    }
}
