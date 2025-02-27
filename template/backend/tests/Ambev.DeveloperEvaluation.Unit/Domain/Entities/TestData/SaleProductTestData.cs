using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleProductTestData {

    /// <summary>
    /// Configures the Faker to generate valid Sale Product entities.
    /// The generated sales will have valid:
    /// - SaleId  (random Guid)
    /// - ProductId  (random Guid)
    /// - Product  (random Product generate by bogus)
    /// - Quantity (from 1 to 20)
    /// - Status (Canceled or Active)
    /// </summary>
    private static readonly Faker<SaleProduct> SaleProductFaker = new Faker<SaleProduct>()
        .RuleFor(sp => sp.SaleId, f => f.Random.Guid())
        .RuleFor(sp => sp.Product, ProductTestData.GenerateValidProduct())
        .RuleFor(sp => sp.ProductId, ProductTestData.GenerateValidProduct().Id)
        .RuleFor(sp => sp.Quantity, f => f.Random.Number(1, 20))
        .RuleFor(sp => sp.Status, f => f.PickRandom(SaleProductStatus.Active, SaleProductStatus.Canceled));

    /// <summary>
    /// Generates a valid Sale Product entity with randomized data.
    /// The generated Sale Product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale Product entity with randomly generated data.</returns>
    public static SaleProduct GenerateValidSaleProduct() {
        return SaleProductFaker.Generate();
    }

    /// <summary>
    /// Generates a Sale product with more than 20 in it's quantity
    /// </summary>
    /// <returns>A invalid Sale Product entity with randomly generated data and more than 20 in quantity </returns>
    public static SaleProduct GenerateSaleProductWithMoreThanAllowenQuantity() {
        var saleProduct = SaleProductFaker.Generate();
        saleProduct.Quantity = 21;
        return saleProduct;
    }
}
