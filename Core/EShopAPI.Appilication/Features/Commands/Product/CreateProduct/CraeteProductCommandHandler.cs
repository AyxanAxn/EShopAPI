using EShopAPI.Appilication.IRepositories;
using MediatR;


namespace EShopAPI.Appilication.Features.Commands.Product.CreateProduct
{
    public class CraeteProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CraeteProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public CraeteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CraeteProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}