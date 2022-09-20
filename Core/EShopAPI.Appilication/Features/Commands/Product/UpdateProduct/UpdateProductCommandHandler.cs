using EShopAPI.Appilication.IRepositories;
using MediatR;
using P=EShopAPI.Domain.Entities;

namespace EShopAPI.Appilication.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly IProductWriteRepository productWriteRepository;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            this.productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle
            (UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            P::Product product = await _productReadRepository.FindByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();


            throw new NotImplementedException();
        }
    }
}
