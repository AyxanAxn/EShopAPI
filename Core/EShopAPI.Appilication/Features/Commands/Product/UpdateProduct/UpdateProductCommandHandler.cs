using EShopAPI.Appilication.IRepositories;
using MediatR;
using P=EShopAPI.Domain.Entities;

namespace EShopAPI.Appilication.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public UpdateProductCommandHandler(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository)
        {
            this._productWriteRepository = productWriteRepository;
            this._productReadRepository = productReadRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle
            (UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            P::Product product = await _productReadRepository.FindByIdAsync(request.Id);
            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            await _productWriteRepository.SaveAsync();


            return new();
        }
    }
}
