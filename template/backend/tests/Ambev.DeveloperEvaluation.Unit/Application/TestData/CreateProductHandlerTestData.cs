using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateProductHandlerTestData {
    /// <summary>
    /// Configures the Faker to generate valid CreateProductCommands
    /// The generated commands will have valid:
    /// - Description (using internet product descriptions)
    /// - Value (from 1.0f to 500.0f)
    /// - Status (OutOfStock or OnStock)
    /// </summary>
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(u => u.Description, f => f.Commerce.ProductName())
        .RuleFor(u => u.Value, f => f.Random.Float(1.0f, 500.0f))
        .RuleFor(u => u.Status, f => f.PickRandom(ProductStatus.OutOfStock, ProductStatus.OnStock));

    /// <summary>
    /// Generates a valid CreateProductCommand entity with randomized data.
    /// The generated command will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid command entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand() {
        return createProductHandlerFaker.Generate();
    }
}
