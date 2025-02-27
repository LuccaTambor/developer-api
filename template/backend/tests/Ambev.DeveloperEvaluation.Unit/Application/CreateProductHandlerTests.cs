using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests {
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests() { 
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid product creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse() {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product {
            Id = Guid.NewGuid(),
            Description = command.Description,
            Value = command.Value,
            Cost = command.Cost.HasValue ? command.Cost.Value : 0f,
            Status = command.Status,
        };

        var result = new CreateProductResult {
            Id = product.Id,
        };


        _mapper.Map<Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid product creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException() {
        // Given
        var command = new CreateProductCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct() {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();
        var product = new Product {
            Id = Guid.NewGuid(),
            Description = command.Description,
            Value = command.Value,
            Cost = command.Cost.HasValue ? command.Cost.Value : 0f,
            Status = command.Status,
        };

        _mapper.Map<Product>(command).Returns(product);
        _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
            c.Description == command.Description &&
            c.Value == command.Value &&
            c.Status == command.Status));
    }

}
