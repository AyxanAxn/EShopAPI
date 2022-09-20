using EShopAPI.Appilication.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P = EShopAPI.Domain.Entities;

namespace EShopAPI.Appilication.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;


        public GetProductImageQueryHandler(
            IProductReadRepository productReadRepository,
            IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }
        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {

            P.Product? product = await _productReadRepository.Table
               .Include(p => p.ProductImageFiles)
               .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));

            return product?.ProductImageFiles.Select(p => new GetProductImageQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();

            throw new NotImplementedException();
        }
    }
}
