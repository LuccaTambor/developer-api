using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including Description, Value, cost, and status. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult> {
    /// <summary>
    /// Gets or sets the description of the product to be created.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the product to be created.
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Gets or sets the cost of the product to be created.
    /// </summary>
    public float? Cost { get; set; }

    /// <summary>
    /// Gets or sets the status of the product to be created.
    /// </summary>
    public ProductStatus Status { get; set; }

    public ValidationResultDetail Validate() {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
