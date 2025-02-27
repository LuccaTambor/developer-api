namespace Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;

public record UncancelSaleResult {
    /// <summary>
    /// The unique identifier of the sale that was uncanceled
    /// </summary>
    public Guid Id { get; set; }
}
