using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Tests cover validation of all product properties including description, valu and status.
/// </summary>
public class ProductValidatorTests {
    private readonly ProductValidator _validator;

    public ProductValidatorTests() {
       _validator = new ProductValidator();
    }

    /// <summary>
    /// Tests that validation passes when all product's properties are valid.
    /// This test verifies that a user with valid:
    /// - Description (it's not empty or null)
    /// - Value (It's not empty and it's greater than zero)
    /// - Status (OnStock/OutOfStock)
    /// passes all validation rules without any errors.
    /// </summary>
    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors() {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails for invalid description format
    /// This test verifies that description that are:
    /// - Empty strings or null values
    /// fail validation with appropriate error messages.
    /// The description is a required field and should not be null or empty
    /// </summary>
    /// <param name="description">The invalid description to test.</param>
    [Theory(DisplayName = "Invalid description should fail validation")]
    [InlineData("")] // Empty
    [InlineData(null)] // null
    public void Given_EmptyOrNullDescription_When_Validated_Then_ShouldHaveError(string description) {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        product.Description = description;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    /// <summary>
    /// Tests that validation fails for invalid value
    /// This test verifies that values that are:
    /// - Null
    /// - Less or equal zero
    /// fail validation with appropriate error messages.
    /// The value is a required field and should not be null or less or equal zero
    /// </summary>
    /// <param name="value">The invalid value to test.</param>
    [Theory(DisplayName = "Invalid value should fail validation")]
    [InlineData(0)] // zero
    [InlineData(null)] // null
    public void Given_EmptyOrZeroValue_When_Validated_Then_ShouldHaveError(float value) {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        product.Value = value;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Value);
    }

    /// <summary>
    /// Tests that validation fails for invalid status
    /// This test verifies that status that are:
    /// - Unknown
    /// fail validation with appropriate error messages.
    /// The status is a required field and should not be Unknown
    /// </summary>
    [Fact(DisplayName = "Invalid status should fail validation")]
    public void Given_UnknownStatus_When_Validated_Then_ShouldHaveError() {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        product.Status = DeveloperEvaluation.Domain.Enums.ProductStatus.Unknown;

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

}
