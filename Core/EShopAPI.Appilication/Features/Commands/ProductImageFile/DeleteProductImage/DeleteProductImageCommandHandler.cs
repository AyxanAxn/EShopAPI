using MediatR;
using P = EShopAPI.Domain.Entities;
using EShopAPI.Appilication.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Appilication.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;


        public DeleteProductImageCommandHandler(
            IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task<DeleteProductImageCommandResponse> Handle
            (DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));

            P.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault
                (p => p.Id == Guid.Parse(request.imageId));

            if (productImageFile != null)
                product.ProductImageFiles.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();




            return new();
        }
    }
}
