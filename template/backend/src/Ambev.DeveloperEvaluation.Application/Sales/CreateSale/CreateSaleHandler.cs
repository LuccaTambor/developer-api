using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Data;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult> {
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IUserRepository userRepository,
        IProductRepository productRepository, 
        IMapper mapper
    ) {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken) {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingCustomer = await _userRepository.GetByIdAsync(command.CustomerId);
        if(existingCustomer == null || existingCustomer.Role != Domain.Enums.UserRole.Customer)
            throw new InvalidOperationException($"User with id {command.CustomerId} doesn't exists");

        var sale = _mapper.Map<Sale>(command);

        foreach(var saleProduct in sale.Products) {
            var existingProduct = await _productRepository.GetByIdAsync(saleProduct.ProductId);
            if(existingProduct == null)
                throw new InvalidOperationException($"Product with id {saleProduct.ProductId} doesn't exists");

            saleProduct.Product = existingProduct;
            saleProduct.CalculateTotal();
        }

        sale.CalculateTotal();
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(sale);
        return result;
    }
}
