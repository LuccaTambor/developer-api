using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController {
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public ProductsController(IMediator mediator) {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">The product creation command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request, CancellationToken cancellationToken) {
        var response = await _mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResult> {
            Success = true,
            Message = "Product created successfully",
            Data = response
        });
    }
}
