using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand> {
    public CreateSaleCommandValidator() { 
        RuleFor(command => command.Number).NotEmpty().NotNull().WithMessage("Sale must have a number.");
        RuleFor(command => command.CustomerId).NotEmpty().NotNull().WithMessage("Sale must have a customer related to it.");
        RuleFor(command => command.Branch).NotEmpty().NotNull().WithMessage("Sale must have a branch name where it's was created.");
        RuleFor(command => command.Status).NotEqual(Domain.Enums.SaleStatus.Unknown).WithMessage("Sale must have a valid status.");
        RuleFor(command => command.Products).ForEach(sp => sp.SetValidator(new SaleProductDTOValidator()));
    }
}

public class SaleProductDTOValidator : AbstractValidator<SaleProductDTO> {
    public SaleProductDTOValidator() {
        RuleFor(dto => dto.ProductId).NotEmpty().NotNull().WithMessage("Sale product must have a product related to it.");
        RuleFor(dto => dto.Quantity)
            .NotEmpty().GreaterThan(0).WithMessage("Sale product must have a valid quantity greater than zero.")
            .LessThanOrEqualTo(20).WithErrorCode("A Same product cannot be selled with more than 20 units.");

        RuleFor(dto => dto.Status).NotEqual(Domain.Enums.SaleProductStatus.Unknown).WithMessage("Sale Product must have a valid status.");
    }
}
