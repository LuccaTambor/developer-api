using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData {

    private static readonly Faker<SaleProductDTO> saleProductDTOFaker = new Faker<SaleProductDTO>()
        .RuleFor(s => s.ProductId, f => f.Random.Guid())
        .RuleFor(s => s.Status, f => f.PickRandom(SaleProductStatus.Canceled, SaleProductStatus.Active))
        .RuleFor(s => s.Quantity, f => f.Random.Number(1, 20));

    public static SaleProductDTO GenerateValidSaleProductDTO() {
        return saleProductDTOFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid CreateProductCommands
    /// The generated commands will have valid:
    /// - Description (using internet product descriptions)
    /// - Value (from 1.0f to 500.0f)
    /// - Status (OutOfStock or OnStock)
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Number, f => f.Random.Number(100, 9999).ToString())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.Status, f => f.PickRandom(SaleStatus.Canceled, SaleStatus.NotCanceled))
        .RuleFor(s => s.Branch, f => f.Commerce.Department())
        .RuleFor(s => s.Products, Enumerable.Range(1,5).Select(_ => GenerateValidSaleProductDTO()).ToList());

    public static CreateSaleCommand GenerateValidCommand() {
        return createSaleHandlerFaker.Generate();
    }
}
