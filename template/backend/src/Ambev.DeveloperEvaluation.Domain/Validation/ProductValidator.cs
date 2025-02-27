using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product> {
    public ProductValidator() { 
        RuleFor(product => product.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product description must be filled.");

        RuleFor(product => product.Value)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Product must have a valid selling value.");

        RuleFor(product => product.Status)
            .NotEqual(ProductStatus.Unknown)
            .WithMessage("Product status cannot be Unknown.");
    }
}
