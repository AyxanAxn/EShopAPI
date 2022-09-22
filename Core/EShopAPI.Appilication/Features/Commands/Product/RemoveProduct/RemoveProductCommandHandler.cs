using EShopAPI.Appilication.IRepositories;
using MediatR;

namespace EShopAPI.Appilication.Features.Commands.Product.RemoveProduct
{
    internal class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle
            (RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {

            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
