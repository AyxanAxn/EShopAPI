using EShopAPI.Appilication.IRepositories;
using P = EShopAPI.Domain.Entities;
using MediatR;

namespace EShopAPI.Appilication.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P::Product product = await _productReadRepository.FindByIdAsync(request.id, false);
            return new()
            {
                Name=product.Name,
                Price=product.Price,
                Stock=product.Stock
            };
        }
    }
}