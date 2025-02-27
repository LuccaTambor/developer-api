using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;

/// <summary>
/// Validator for UncancelSaleCommand
/// </summary>
public class UncancelSaleCommandValidator : AbstractValidator<UncancelSaleCommand> {
    /// <summary>
    /// Initializes validation rules for UncancelSaleCommand
    /// </summary>
    public UncancelSaleCommandValidator() {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
