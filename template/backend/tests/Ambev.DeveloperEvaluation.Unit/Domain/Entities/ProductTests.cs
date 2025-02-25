using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class ProductTests {
    /// <summary>
    /// Tests that when a out of stock product is set to avaiable, their status changes to OnStock.
    /// </summary>
    [Fact(DisplayName = "Product status should change to OnStock when set to avaible")]
    public void Given_OutOfStockProduct_When_SetToAvaible_Then_StatusShouldBeOnStock() {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Status = ProductStatus.OutOfStock;

        // Act
        product.SetToAvaiable();

        // Assert
        Assert.Equal(ProductStatus.OnStock, product.Status);
    }

    /// <summary>
    /// Tests that when a on stock product is set to out of stock, their status changes to OutOfStock.
    /// </summary>
    [Fact(DisplayName = "Product status should change to OutOfStock when set to be out of stock")]
    public void Given_OnStockProduct_When_SetToOutOfStock_Then_StatusShouldBeOutOfStock() {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Status = ProductStatus.OnStock;

        // Act
        product.SetToOutOfStock();

        // Assert
        Assert.Equal(ProductStatus.OutOfStock, product.Status);
    }

    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid() {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid() {
        // Arrange
        var product = new Product {
            Description = string.Empty,
            Value = 0.0f,
            Cost = 0.0f,
            Status = ProductStatus.Unknown,
        };

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
