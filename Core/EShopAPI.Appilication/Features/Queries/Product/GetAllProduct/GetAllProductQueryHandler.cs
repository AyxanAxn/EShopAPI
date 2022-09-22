using EShopAPI.Appilication.IRepositories;
using MediatR;

namespace EShopAPI.Appilication.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, 
            CancellationToken cancellationToken)
        {
            var totalProductCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            })
            .Skip(request.Page * request.Size)
            .Take(request.Size)
            .ToList();

            return new()
            {
                Products=products,
                TotalCount=totalProductCount
            };
        }
    }
}
