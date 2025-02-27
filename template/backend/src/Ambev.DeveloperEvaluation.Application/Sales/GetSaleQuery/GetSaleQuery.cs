using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleQuery;

/// <summary>
/// Query for retrieving sales from the database
/// </summary>
public class GetSaleQuery : IRequest<IQueryable<Sale>> {
    /// <summary>
    /// The filter to be applied to the query
    /// </summary>
    public SaleFilter? Filter { get; set; }
    /// <summary>
    /// The property to sort the query by
    /// </summary>
    public string SortBy { get; set; } = string.Empty;
    /// <summary>
    /// If the query is to be sorted desceding
    /// </summary>
    public bool IsDescending { get; set; }
}
