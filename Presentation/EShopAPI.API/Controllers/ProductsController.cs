using EShopAPI.Appilication.IRepositories;
using EShopAPI.Appilication.RequestParameters;
using EShopAPI.Appilication.Services;
using EShopAPI.Appilication.ViewModels;
using EShopAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IFileReadRepository _fileRead;
        private readonly IFileWriteRepository _fileWrite;
        private readonly IProductImageFileReadRepository _productImageRead;
        private readonly IProductImageFileWriteRepository _productImageWrite;
        private readonly IInvoiceFileReadRepository _invoiceRead;
        private readonly IInvoiceFileWriteRepository _invoiceWrite;


        public ProductsController(

            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IFileReadRepository fileRead,
            IFileWriteRepository fileWrite,
            IProductImageFileReadRepository productImageRead,
            IProductImageFileWriteRepository productImageWrite,
            IInvoiceFileReadRepository invoiceRead,
            IInvoiceFileWriteRepository invoiceWrite)
        {
            this._productWriteRepository = productWriteRepository;
            this._productReadRepository = productReadRepository;
            this._webHostEnvironment = webHostEnvironment;
            this._fileService = fileService;
            this._fileRead = fileRead;
            this._fileWrite = fileWrite;
            this._productImageRead = productImageRead;
            this._productImageWrite = productImageWrite;
            this._invoiceRead = invoiceRead;
            this._invoiceWrite = invoiceWrite;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
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
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();

            return Ok(new
            {
                totalProductCount,
                products
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.FindByIdAsync(id, false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Products model)
        {
            if (ModelState.IsValid)
            {
                await _productWriteRepository.AddAsync(new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock
                });
                await _productWriteRepository.SaveAsync();
                return StatusCode((int)HttpStatusCode.Created);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.FindByIdAsync(model.Id);
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {
            var datas = await _fileService.UploadAsync("resourse/product-images", Request.Form.Files);
            //await _productImageWrite.AddRangeAsync(
            //    datas.Select(
            //        d=>new ProductImageFile() 
            //        { 
            //            FileName=d.fileName,
            //            Path=d.path

            //        }).ToList());
            //await _productImageWrite.SaveAsync();





            //await _invoiceWrite.AddRangeAsync(
            //  datas.Select(
            //      d => new InvoiceFile()
            //      {
            //          FileName = d.fileName,
            //          Path = d.path,
            //          Price = new Random().Next()

            //      }).ToList()); ; ;
            //await _productImageWrite.SaveAsync(); 





            //await _fileWrite.AddRangeAsync(
            //  datas.Select(
            //      d => new EShopAPI.Domain.Entities.File()
            //      {
            //          FileName = d.fileName,
            //          Path = d.path

            //      }).ToList()); ; ;
            //await _productImageWrite.SaveAsync();


           

            return Ok();
            return Ok();
            return Ok();
        }


    }
}