using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleQuery;

/// <summary>
/// Handler for processing GetSaleQuery requests
/// </summary>
public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, IQueryable<Sale>> {
    private ISaleRepository _saleRepository;

    public GetSaleQueryHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the GetSaleQuery request
    /// </summary>
    /// <param name="request">The GetSaleQuery request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sales query</returns>
    public Task<IQueryable<Sale>> Handle(GetSaleQuery request, CancellationToken cancellationToken) {
        var query  = _saleRepository.GetQuery(request.SortBy, request.IsDescending, request.Filter);
        return Task.FromResult(query);
    }
}
