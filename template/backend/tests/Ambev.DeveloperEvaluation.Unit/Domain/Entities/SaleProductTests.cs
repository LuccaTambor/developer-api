using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleProduct entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleProductTests {
    /// <summary>
    /// Tests that when a canceled sale product is uncanceled, their status changes to Active.
    /// </summary>
    [Fact(DisplayName = "Sale product status should change to Active when uncanceled")]
    public void Given_CanceledSaleProduct_When_Uncanceled_Then_StatusShouldBeActive() {
        // Arrange
        var product = SaleProductTestData.GenerateValidSaleProduct();
        product.Status = SaleProductStatus.Canceled;

        // Act
        product.Uncancel();

        // Assert
        Assert.Equal(SaleProductStatus.Active, product.Status);
    }

    /// <summary>
    /// Tests that when a active sale product is canceled, their status changes to Canceled.
    /// </summary>
    [Fact(DisplayName = "Sale product status should change to Canceled when active")]
    public void Given_ActiveSaleProduct_When_Canceled_Then_StatusShouldBeCanceled() {
        // Arrange
        var product = SaleProductTestData.GenerateValidSaleProduct();
        product.Status = SaleProductStatus.Active;

        // Act
        product.Cancel();

        // Assert
        Assert.Equal(SaleProductStatus.Canceled, product.Status);
    }

    /// <summary>
    /// Tests that validation passes when all sale product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale product data")]
    public void Given_ValidSaleProductData_When_Validated_Then_ShouldReturnValid() {
        // Arrange
        var product = SaleProductTestData.GenerateValidSaleProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid sale product data")]
    public void Given_InvalidSaleProductData_When_Validated_Then_ShouldReturnInvalid() {
        // Arrange
        var product = SaleProductTestData.GenerateSaleProductWithMoreThanAllowenQuantity();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Test that the calculate total method calculates the correct values given the sale product values and quantity
    /// </summary>
    /// <param name="quantity">The quantity of the sale product</param>
    [Theory(DisplayName = "Calculate total should calculate discounts and values correctly")]
    [InlineData(3)]
    [InlineData(8)]
    [InlineData(12)]
    public void Given_ValidSaleProduct_When_CalculateTotal_Then_ShoulHaveCorrectValues(int quantity) {
        // Arrange
        var product = SaleProductTestData.GenerateValidSaleProduct();
        product.Quantity = quantity;

        // Act
        product.CalculateTotal();

        // Assert
        Assert.Equal(product.Product.Value * product.Quantity, product.TotalValue);
        Assert.Equal(product.TotalValue * product.GetDiscountRate(), product.Discount);
        Assert.Equal(product.TotalValue - product.Discount, product.TotalWithDiscount);
    }
}
