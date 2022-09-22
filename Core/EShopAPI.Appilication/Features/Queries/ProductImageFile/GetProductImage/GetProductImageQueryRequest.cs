using MediatR;

namespace EShopAPI.Appilication.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryRequest:IRequest<List<GetProductImageQueryResponse>>
    {
        public string id { get; set; }
    }
}
