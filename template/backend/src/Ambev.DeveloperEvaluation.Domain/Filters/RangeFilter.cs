namespace Ambev.DeveloperEvaluation.Domain.Filters;

/// <summary>
/// Class to be used as base to range filters
/// </summary>
/// <typeparam name="T">The type of the range filter</typeparam>
/// <param name="Min">The min value of the filter</param>
/// <param name="Max">The max value of the filter</param>
public record RangeFilter<T>(T Min, T Max);
