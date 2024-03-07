using Order.Application.Core.Messaging;
using Order.Domain.Entities;
using Shared.Core.Primitives.Result;
using Shared.Core.Repositories;

namespace Order.Application.Orders.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Address> _addressRepository;

        public CreateOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository, IRepository<Product> productRepository, IRepository<Address> addressRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Address.AddressLine, request.Address.City, request.Address.Country, request.Address.CityCode);
            await _addressRepository.CreateAsync(address);

            var product = new Product(request.Product.ImageUrl, request.Product.Name);
            await _productRepository.CreateAsync(product);

            var data = new Domain.Entities.Order(request.CustomerId, request.Quantity, request.Price, request.Status, product.Id, address.Id);
            await _orderRepository.CreateAsync(data);
            return Result<Guid>.Success(data.Id);
        }
    }
}
