using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult> {
    public string Number { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public SaleStatus Status { get; set; }
    public List<SaleProductDTO> Products { get; set; } = [];
}

public class SaleProductDTO {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public SaleProductStatus Status { get; set; }
}
