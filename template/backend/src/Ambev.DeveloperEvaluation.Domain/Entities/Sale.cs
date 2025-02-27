using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale in the system.
/// </summary>
public class Sale : BaseEntity {
    /// <summary>
    /// Gets the sale's number.
    /// Must not be null or empty.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Get's the sale's customer
    /// Should always have a customer
    /// </summary>
    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public User Customer { get; set; } = null!;

    /// <summary>
    /// The sale's products list.
    /// </summary>
    public List<SaleProduct> Products { get; set; } = [];

    /// <summary>
    /// Get's the sale's total value.
    /// </summary>
    public float Total { get; set; }

    /// <summary>
    /// Get's the sale's total discount.
    /// </summary>
    public float TotalDiscount { get; set; }

    /// <summary>
    /// Get's the branch where the sale was created
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Get's the sale's status
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public Sale() {
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.NotCanceled;
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Number</list>
    /// <list type="bullet">CustomerId</list>
    /// <list type="bullet">Status</list>
    /// <list type="bullet">Products</list>
    /// </remarks>
    public ValidationResultDetail Validate() {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Calculates the total and total discount of the sale based on it's products
    /// </summary>
    public void CalculateTotal() {
        Total = Products.Where(sp => sp.Status == SaleProductStatus.Active).Sum(sp => sp.TotalWithDiscount);
        TotalDiscount = Products.Where(sp => sp.Status == SaleProductStatus.Active).Sum(sp => sp.Discount);
    }

    /// <summary>
    /// Cancel the sale.
    /// Changes the sale's status to Canceled.
    /// </summary>
    public void Cancel() {
        Status = SaleStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Uncancel the sale.
    /// Changes the sale's status to NotCanceled.
    /// </summary>
    public void Uncancel() {
        Status = SaleStatus.NotCanceled;
        UpdatedAt = DateTime.UtcNow;
    }

}
