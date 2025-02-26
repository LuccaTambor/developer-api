namespace Ambev.DeveloperEvaluation.Domain.Filters;

public class SaleFilter {
    public string Number { get; set; } = string.Empty;
    public RangeFilter<float>? Total { get; set; } = null!;
    public RangeFilter<DateTime?>? CreatedAt { get; set; } = null!;

}
