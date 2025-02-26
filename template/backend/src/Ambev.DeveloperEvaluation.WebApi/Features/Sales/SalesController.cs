using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController {
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public SalesController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request, CancellationToken cancellationToken) {
        var response = await _mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResult> {
            Success = true,
            Message = "Sale created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Canceles a sale
    /// </summary>
    /// <param name="id">The identifier of the sale to be canceled</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The canceled sale details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [Route("cancel/{id:Guid}")]
    public async Task<IActionResult> CancelSale(Guid id, CancellationToken cancellationToken) {
        CancelSaleCommand request = new(id);
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<CancelSaleResult> {
            Success = true,
            Message = "Sale canceled successfully",
            Data = response
        });
    }

    /// <summary>
    /// Uncanceles a sale
    /// </summary>
    /// <param name="id">The identifier of the sale to be uncanceled</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The uncanceled sale details</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [Route("uncancel/{id:Guid}")]
    public async Task<IActionResult> UncancelSale(Guid id, CancellationToken cancellationToken) {
        UncancelSaleCommand request = new(id);
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<UncancelSaleResult> {
            Success = true,
            Message = "Sale uncanceled successfully",
            Data = response
        });
    }
}
