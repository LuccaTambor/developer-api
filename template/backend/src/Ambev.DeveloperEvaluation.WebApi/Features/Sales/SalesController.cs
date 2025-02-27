using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleQuery;
using Ambev.DeveloperEvaluation.Application.Sales.UncancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations
/// </summary>
[ApiController]
[Route("api/sales")]
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
    /// Get's the sales paginated
    /// </summary>
    /// <param name="_page">The page to be returned</param>
    /// <param name="_size">The size of the pages</param>
    /// <param name="_filter">the filter to be applied to the query</param>
    /// <returns>The requested paginated sales</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<Sale>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaged([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _sort = "number", [FromQuery] bool isDescending = false, [FromQuery] SaleFilter? _filter = null!) {
        GetSaleQuery request = new() { Filter = _filter, SortBy = _sort, IsDescending = isDescending };

        var query = await _mediator.Send(request);

        var test = await PaginatedList<Sale>.CreateAsync(query, _page, _size);

        return OkPaginated(test);
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
