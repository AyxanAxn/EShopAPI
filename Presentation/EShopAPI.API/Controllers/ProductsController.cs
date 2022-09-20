using EShopAPI.Appilication.Abstractions.Storage;
using EShopAPI.Appilication.Features.Commands.Product.CreateProduct;
using EShopAPI.Appilication.Features.Commands.Product.RemoveProduct;
using EShopAPI.Appilication.Features.Commands.Product.UpdateProduct;
using EShopAPI.Appilication.Features.Commands.ProductImageFile.UploadProductImage;
using EShopAPI.Appilication.Features.Queries.Product.GetAllProduct;
using EShopAPI.Appilication.Features.Queries.Product.GetByIdProduct;
using EShopAPI.Appilication.IRepositories;
using EShopAPI.Appilication.RequestParameters;
using EShopAPI.Appilication.ViewModels;
using EShopAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileReadRepository _fileRead;
        private readonly IFileWriteRepository _fileWrite;
        private readonly IProductImageFileReadRepository _productImageRead;
        private readonly IProductImageFileWriteRepository _productImageWrite;
        private readonly IInvoiceFileReadRepository _invoiceRead;
        private readonly IInvoiceFileWriteRepository _invoiceWrite;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;




        readonly IMediator _mediator;
        public ProductsController(

            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileReadRepository fileRead,
            IFileWriteRepository fileWrite,
            IProductImageFileReadRepository productImageRead,
            IProductImageFileWriteRepository productImageWrite,
            IInvoiceFileReadRepository invoiceRead,
            IInvoiceFileWriteRepository invoiceWrite,
            IStorageService storageService,
            IConfiguration configuration,
            IMediator mediator)
        {
            this._productWriteRepository = productWriteRepository;
            this._productReadRepository = productReadRepository;
            this._webHostEnvironment = webHostEnvironment;
            this._fileRead = fileRead;
            this._fileWrite = fileWrite;
            this._productImageRead = productImageRead;
            this._productImageWrite = productImageWrite;
            this._invoiceRead = invoiceRead;
            this._invoiceWrite = invoiceWrite;
            this._storageService = storageService;
            this._configuration = configuration;
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CraeteProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest id)
        {
           
        
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new
            {
                path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productForDelete = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImageFiles.Remove(productForDelete);
            await _productImageWrite.SaveAsync();

            return Ok();

        }
    }
}