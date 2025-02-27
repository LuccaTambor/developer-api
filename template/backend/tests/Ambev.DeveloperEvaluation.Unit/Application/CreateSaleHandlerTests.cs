using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests {
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests() { 
        _saleRepository = Substitute.For<ISaleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new(_saleRepository, _userRepository, _productRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse() {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale {
            Id = Guid.NewGuid(),
            Number = command.Number,
            Branch = command.Branch,
            CustomerId = command.CustomerId,
            Status = command.Status,
            Products = command.Products.Select(_ => new SaleProduct { ProductId = _.ProductId, Quantity = _.Quantity, Status = _.Status}).ToList(),
        };

        var result = new CreateSaleResult {
            Id = sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new User { Id = command.CustomerId, Role = DeveloperEvaluation.Domain.Enums.UserRole.Customer });

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Product { Id = command.Products.First().ProductId });

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }


    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException() {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that a valid sale creation request has a invalid customer id
    /// </summary>
    [Fact(DisplayName = "Given sale data When creating sale with invalid customerId should throw InvalidOperation exception")]
    public async Task Handle_RequestWithInvalidCustomerId_ThrowsInvalidOperationException() {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale {
            Id = Guid.NewGuid(),
            Number = command.Number,
            Branch = command.Branch,
            CustomerId = command.CustomerId,
            Status = command.Status,
            Products = command.Products.Select(_ => new SaleProduct { ProductId = _.ProductId, Quantity = _.Quantity, Status = _.Status }).ToList(),
        };

        var result = new CreateSaleResult {
            Id = sale.Id,
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns((User)null!);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct() {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale {
            Id = Guid.NewGuid(),
            Number = command.Number,
            Branch = command.Branch,
            CustomerId = command.CustomerId,
            Status = command.Status,
            Products = command.Products.Select(_ => new SaleProduct { ProductId = _.ProductId, Quantity = _.Quantity, Status = _.Status }).ToList(),
        };

        _mapper.Map<Sale>(command).Returns(sale);

        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new User { Id = command.CustomerId, Role = DeveloperEvaluation.Domain.Enums.UserRole.Customer });

        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Product { Id = command.Products.First().ProductId });

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.Number == command.Number &&
            c.Branch == command.Branch &&
            c.Status == command.Status));
    }
}
