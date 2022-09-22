using MediatR;
using Microsoft.AspNetCore.Http;

namespace EShopAPI.Appilication.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }

    }
}
