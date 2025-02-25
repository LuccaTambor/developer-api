using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Product of a sale in the system
/// </summary>
public class SaleProduct : BaseEntity {
    /// <summary>
    /// Get's the product of the sale product
    /// </summary>
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Get's the sale of the sale product
    /// </summary>
    [ForeignKey(nameof(Sale))]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = null!;

    /// <summary>
    /// Get's the quantity of the sale product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Get's the discount of the sale product
    /// </summary>
    public float Discount { get; set; }

    /// <summary>
    /// Get's the total value of the sale product without discount's
    /// </summary>
    public float TotalValue { get; set; }

    /// <summary>
    /// Get's the total value of the sale product with discount's
    /// </summary>
    public float TotalWithDiscount { get; set; }

    /// <summary>
    /// Ge'ts the status of the sale product
    /// </summary>
    public SaleProductStatus Status { get; set; }

    /// <summary>
    /// Gets the date and time when the SaleProduct was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the SaleProduct's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public SaleProduct() {
        CreatedAt = DateTime.UtcNow;
        Status = SaleProductStatus.Active;
    }

    /// <summary>
    /// Performs validation of the sale product entity using the SaleProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Quantity of the sale product</list>
    /// </remarks>
    public ValidationResultDetail Validate() {
        var validator = new SaleProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Calculates the total value of this sale item.
    /// Also calculates the discount and the value with discount.
    /// </summary>
    public void CalculateTotal() {
        TotalValue = Product.Value * Quantity;
        Discount = TotalValue * GetDiscountRate();
        TotalWithDiscount = TotalValue - Discount;
    }

    /// <summary>
    /// Cancel the sale product.
    /// Changes the sale product's status to Canceled.
    /// </summary>
    public void Cancel() {
        Status = SaleProductStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Uncancel the sale product.
    /// Changes the sale product's status to Active.
    /// </summary>
    public void Uncancel() {
        Status = SaleProductStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Get's the discount rate of the product based on the <see cref="Quantity"/>
    ///  - Between 10 and 20: 20%
    ///  - More or equal 4: 10%
    ///  - Less then 4: 0%
    /// </summary>
    /// <returns> The discount rate </returns>
    public float GetDiscountRate() {
        if(Quantity >= 10) return 0.2f;
        else if(Quantity >= 4) return 0.1f;
        return 0.0f;
    }
}
