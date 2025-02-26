namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public record CancelSaleResult {
    /// <summary>
    /// The unique identifier of the sale that was canceled
    /// </summary>
    public Guid Id { get; set; }
}
