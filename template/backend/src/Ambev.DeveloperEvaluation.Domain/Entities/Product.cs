using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Product in the system.
/// </summary>
public class Product : BaseEntity {
    /// <summary>
    /// Gets the product's description.
    /// Must not be null or empty.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Get's the product's unit sell value.
    /// Must not be null.
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Get's the product's cost
    /// </summary>
    public float Cost { get; set; }

    /// <summary>
    /// Get's the product's status
    /// </summary>
    public ProductStatus Status { get; set; }

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product() {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Description of the product</list>
    /// <list type="bullet">Value of the product</list>
    /// <list type="bullet">Product's status</list>
    /// </remarks>
    public ValidationResultDetail Validate() {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };      
    }

    /// <summary>
    /// Set the product as avaible to be purchased.
    /// Changes the product's status to OnStock.
    /// </summary>
    public void SetToAvaiable() {
        Status = ProductStatus.OnStock;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Set the product as not avaible to be purchased.
    /// Changes the product's status to OutOfStock.
    /// </summary>
    public void SetToOutOfStock() {
        Status = ProductStatus.OutOfStock;
        UpdatedAt = DateTime.UtcNow;
    }
}
