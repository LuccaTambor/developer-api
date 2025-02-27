using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult> {
    private ISaleRepository _saleRepository;

    public CancelSaleHandler(ISaleRepository saleRepository) { 
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    /// <param name="request">The CancelSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the cancel operation</returns>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken) {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(request.Id) 
            ?? throw new InvalidOperationException($"Sale with {request.Id} doesn't exist.");

        existingSale.Cancel();
        existingSale = await _saleRepository.UpdateAsync(existingSale);

        return new CancelSaleResult { Id = existingSale.Id };
    }
}
