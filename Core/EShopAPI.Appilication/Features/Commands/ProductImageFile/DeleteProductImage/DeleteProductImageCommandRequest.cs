using MediatR;

namespace EShopAPI.Appilication.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
    {
        public string id { get; set; }
        public string? imageId { get; set; }
    }
}