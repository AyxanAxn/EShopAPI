using EShopAPI.Appilication.Abstractions.Storage;
using EShopAPI.Appilication.Features.Commands.Product.CreateProduct;
using EShopAPI.Appilication.Features.Commands.Product.UpdateProduct;
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
            this._mediator=mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response=  await _mediator.Send(getAllProductQueryRequest);
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
            CraeteProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created); 
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
           
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);
            Product product = await _productReadRepository.FindByIdAsync(id);


            //The second version : 

            //foreach (var r in result)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {
            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}

            await _productImageWrite.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageWrite.SaveAsync();
            //var datas=await _storageService.UploadAsync("files",Request.Form.Files);

            //await _productImageWrite.AddRangeAsync(
            //    datas.Select(
            //        d => new ProductImageFile()
            //        {
            //            FileName = d.fileName,
            //            Path = d.pathOrContainerName,
            //            Storage=_storageService.StorageName
            //        }).ToList());
            //var finalData=await _productImageWrite.SaveAsync();
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
        public async Task<IActionResult> DeleteProductImage(string  id, string imageId)
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