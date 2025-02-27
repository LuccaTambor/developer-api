using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Description: Must not be empty and have a max of 50 characters
    /// - Value: Must not be empty and be greater than 0
    /// - Status: Cannot be set to Unknown
    /// </remarks>
    public CreateProductCommandValidator() {
        RuleFor(product => product.Description)
            .NotEmpty().WithMessage("Product must have a description.")
            .MaximumLength(50).WithMessage("Product description must have under 50 characters.");

        RuleFor(product => product.Value)
            .NotEmpty().GreaterThan(0.0f).WithMessage("Product must have a value.");

        RuleFor(product => product.Status)
            .NotEqual(ProductStatus.Unknown).WithMessage("Product must have a valid status.");

    }
}
