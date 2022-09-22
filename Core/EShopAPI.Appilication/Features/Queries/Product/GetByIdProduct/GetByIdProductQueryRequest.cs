using MediatR;

namespace EShopAPI.Appilication.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryRequest:IRequest<GetByIdProductQueryResponse>
    {
        public string id { get; set; }
    }
}
