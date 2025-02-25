using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData {

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Description
    /// - Value
    /// - Status
    /// - CreatedAt
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
        .RuleFor(u => u.Value, f => f.Random.Float(1.0f, 500.0f))
        .RuleFor(u => u.Cost, f => f.Random.Float(1.0f, 500.0f))
        .RuleFor(u => u.Status, f => f.PickRandom(ProductStatus.OutOfStock, ProductStatus.OnStock))
        .RuleFor(u => u.CreatedAt, f => f.Date.Future(1, DateTime.UtcNow));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct() {
        return ProductFaker.Generate();
    }
}
