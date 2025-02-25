using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleTest {
    /// <summary>
    /// Tests that when a canceled sale is uncanceled, their status changes to NotCanceled.
    /// </summary>
    [Fact(DisplayName = "Sale status should change to NotCanceled when uncanceled")]
    public void Given_CanceledSale_When_Uncanceled_Then_StatusShouldBeNotCanceled() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Status = SaleStatus.Canceled;

        // Act
        sale.Uncancel();

        // Assert
        Assert.Equal(SaleStatus.NotCanceled, sale.Status);
    }

    /// <summary>
    /// Tests that when a NotCanceled sale is canceled, their status changes to Canceled.
    /// </summary>
    [Fact(DisplayName = "Sale status should change to NotCanceled when uncanceled")]
    public void Given_NotCanceledSale_When_Canceled_Then_StatusShouldBeCanceled() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Status = SaleStatus.NotCanceled;

        // Act
        sale.Cancel();

        // Assert
        Assert.Equal(SaleStatus.Canceled, sale.Status);
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid sale data")]
    public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid() {
        // Arrange
        var sale = new Sale {
            Number = string.Empty,
            CustomerId = Guid.Empty,
            Status = SaleStatus.Unknown,
            Products = [ SaleProductTestData.GenerateSaleProductWithMoreThanAllowenQuantity()]
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Test that the calculate total method calculates the correct values given the sale products
    /// </summary>
    [Fact(DisplayName = "Calculate total should calculate discounts and values correctly")]
    public void Given_ValidSale_When_CalculateTotal_Then_ShoulHaveCorrectValues() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.CalculateTotal();

        // Assert
        Assert.Equal(sale.Products.Sum(sp => sp.TotalWithDiscount), sale.Total);
        Assert.Equal(sale.Products.Sum(sp => sp.Discount), sale.TotalDiscount);
        Assert.Equal(sale.Products.Sum(sp => sp.TotalValue), sale.TotalDiscount + sale.Total);
    }
}
