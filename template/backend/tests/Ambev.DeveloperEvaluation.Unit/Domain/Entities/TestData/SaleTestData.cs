using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData {

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - Number (from 1 to 1000)
    /// - Total (from 1.0f to 500.0f)
    /// - Total Discount (from 1.0f to 500.0f)
    /// - CustomerId (random Guid)
    /// - Branch (random company name from bogus)
    /// - Products
    /// - Status (Canceled or NotCanceled)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.Number, f => f.Random.Number(1, 1000).ToString())
        .RuleFor(u => u.Total, f => f.Random.Float(1.0f, 500.0f))
        .RuleFor(u => u.TotalDiscount, f => f.Random.Float(1.0f, 500.0f))
        .RuleFor(u => u.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.Products, Enumerable.Range(1, 5).Select(_ => SaleProductTestData.GenerateValidSaleProduct()).ToList())
        .RuleFor(u => u.Status, f => f.PickRandom(SaleStatus.NotCanceled, SaleStatus.Canceled));

    /// <summary>
    /// Generates a valid sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale() {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a sale number that exceeds the maximum length limit.
    /// The generated username will:
    /// - Be longer than 50 characters
    /// - Contain random alphanumeric characters
    /// This is useful for testing sale number length validation error cases.
    /// </summary>
    /// <returns>A sale number that exceeds the maximum length limit.</returns>
    public static string GenerateLongNumber() {
        return new Faker().Random.String2(51);
    }
}
