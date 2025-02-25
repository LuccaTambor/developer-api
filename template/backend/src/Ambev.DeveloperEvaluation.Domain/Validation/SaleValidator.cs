using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale> {
    public SaleValidator() {
        RuleFor(sale => sale.Number)
            .NotEmpty()
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        RuleFor(sale => sale.CustomerId)
            .NotNull().NotEmpty()
            .WithMessage("Sale must have a customer.");

        RuleFor(sale => sale.Branch)
            .NotEmpty().NotNull().WithMessage("Sale must have a branch.");

        RuleFor(sale => sale.Status)
            .NotEqual(Enums.SaleStatus.Unknown)
            .WithMessage("Sale must have valid status.");

        RuleFor(sale => sale.Products)
            .ForEach(sp => sp.SetValidator(new SaleProductValidator()));
    }
}
