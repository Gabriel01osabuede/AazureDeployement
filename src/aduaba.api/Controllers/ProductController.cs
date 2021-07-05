using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Resource;
using aduaba.api.Services;
using aduaba.api.Entities.ApplicationEntity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aduaba.api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductInterface _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductInterface productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/[controller]/AddProduct")]
        public async Task<IActionResult> PostProductAsync([FromBody] AddProductResource addProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            string imagePath = addProduct.ProductImageFilePath;
            Product product = new Product()
            {

                ProductName = addProduct.ProductName,
                ProductAmount = addProduct.ProductAmount,
                ProductDescription = addProduct.ProductDescription,
                ProductImageUrlPath = ImageUpload.ImageUploads(imagePath),
                CategoryId = addProduct.CategoryId
            };

            var products = _mapper.Map<AddProductResource, Product>(addProduct);
            var result = await _productService.SaveAsync(product);
            

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            return Ok(productResource);
        }

        [HttpGet]
        [Route("/api/[controller]/GetProduct")]
        public async Task<IEnumerable<ProductResource>> GetAllProductAsync()
        {
            var product = await _productService.ListAysnc();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(product);

            return resources;
        }

        [HttpPut]
        [Route("/api/[controller]/UpdateProduct")]
        public async Task<IActionResult> UpdateProductById([FromQuery] string Id,[FromBody] UpdateProductResource responseBody)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            
            Product product = new Product()
            {

                ProductName = responseBody.ProductName,
                ProductAmount = responseBody.ProductAmount,
                ProductDescription = responseBody.ProductDescription,
                CategoryId = responseBody.CategoryId,
                ProductAvailabilty = responseBody.ProductAvailabilty
            };
            if (!(string.IsNullOrEmpty(responseBody.ProductImageFilePath)))
            {
                product.ProductImageUrlPath = ImageUpload.ImageUploads(responseBody.ProductImageFilePath);
            };
            var result = await _productService.UpdateAsync(Id, product);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, UpdateProductResource>(result.Product);

            return Ok(productResource);
        }

        [HttpDelete]
        [Route("/api/[controller]/RemoveProduct")]
        public async Task<IActionResult> RemoveProduct([FromQuery]string Id)
        {
            var deleteResult = await _productService.DeleteAsync(Id);

            if (!deleteResult.Success)
                return BadRequest(deleteResult.Message);

            var productResource = _mapper.Map<Product, ProductService>(deleteResult.Product);
            return Ok(productResource);

        }
    }
}