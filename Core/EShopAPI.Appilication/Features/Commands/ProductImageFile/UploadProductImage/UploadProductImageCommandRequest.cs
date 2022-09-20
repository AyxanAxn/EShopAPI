using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Appilication.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public string Id { get; set; }
        //public IFormFileCollection MyProperty { get; set; }

    }
}
