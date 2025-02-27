using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;

/// <summary>
/// Handler for processing UncancelSaleCommand requests
/// </summary>
public class UncancelSaleHandler : IRequestHandler<UncancelSaleCommand, UncancelSaleResult> {
    private ISaleRepository _saleRepository;

    public UncancelSaleHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the UncancelSaleCommand request
    /// </summary>
    /// <param name="request">The UncancelSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the uncancel operation</returns>
    public async Task<UncancelSaleResult> Handle(UncancelSaleCommand request, CancellationToken cancellationToken) {
        var validator = new UncancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(request.Id)
            ?? throw new InvalidOperationException($"Sale with {request.Id} doesn't exist.");

        existingSale.Uncancel();
        existingSale = await _saleRepository.UpdateAsync(existingSale);

        return new UncancelSaleResult { Id = existingSale.Id };
    }
}
