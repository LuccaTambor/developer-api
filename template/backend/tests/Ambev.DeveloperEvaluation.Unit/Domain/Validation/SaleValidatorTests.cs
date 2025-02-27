using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all Sale properties including Number, customerId and value
/// </summary>
public class SaleValidatorTests {
    private readonly SaleValidator _validator;

    public SaleValidatorTests() {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// This test verifies that a sale with valid:
    /// - Number (not null and up to 50 characters)
    /// - CustomerId (not null)
    /// - Status (Canceled/NotCanceled)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }


    /// <summary>
    /// Tests that validation fails when number exceeds maximum length.
    /// This test verifies that sale number longer than 50 characters fail validation.
    /// The test uses TestDataGenerator to create a sale number that exceeds the maximum
    /// length limit, ensuring the validation rule is properly enforced.
    /// </summary>
    [Fact(DisplayName = "Number longer than maximum length should fail validation")]
    public void Given_NumberLongerThanMaximum_When_Validated_Then_ShouldHaveError() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Number = SaleTestData.GenerateLongNumber();

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    /// <summary>
    /// Tests that validation fails when customerId is empty.
    /// </summary>
    [Fact(DisplayName = "When Customer id is empty should fail validation")]
    public void Given_CustomerIdIsNull_When_Validated_Then_ShouldHaveError() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CustomerId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    /// <summary>
    /// Tests that validation fails when status is Unknown.
    /// </summary>
    [Fact(DisplayName = "When status is unknown should fail validation")]
    public void Given_StatusIsUnknown_When_Validated_Then_ShouldHaveError() {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.Status = SaleStatus.Unknown;

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    /// <summary>
    /// Test that validation fails when a product of the sale has more than 20 in quantity
    /// </summary>
    [Fact(DisplayName = "When product has more than 20 of quantity should fail validation")]
    public void Given_ProductWithMoreThan20Units_When_Validated_Then_ShouldHaveError() {
        // Arrange
        var sale = new Sale {
            Number = "123",
            Total = 500f,
            TotalDiscount = 100f,
            CustomerId = new Guid(),
            Status = SaleStatus.NotCanceled,
            Products = [SaleProductTestData.GenerateSaleProductWithMoreThanAllowenQuantity()]
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveAnyValidationError();
    }

}
