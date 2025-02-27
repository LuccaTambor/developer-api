using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleProductValidator : AbstractValidator<SaleProduct> {
    public SaleProductValidator() {
        RuleFor(sp => sp.Quantity)
           .GreaterThan(0).WithMessage("Quantity of a product in a sale must be more than zero.")
           .LessThanOrEqualTo(20).WithMessage("The purchase of more than 20 of the same product is not allowed.");
    }
}
