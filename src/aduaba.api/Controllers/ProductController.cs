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

            var imageUpload = new ImageUpload();

            string imagePath = addProduct.productImageFilePath;
            Product product = new Product()
            {

                productName = addProduct.productName,
                productAmount = addProduct.productAmount,
                productDescription = addProduct.productDescription,
                productImageUrlPath = imageUpload.ImageUploads(imagePath),
                categoryId = addProduct.categoryId
            };

            var result = await _productService.SaveAsync(product);

            if (!result.success)
                return BadRequest(result.message);

            var productResource = _mapper.Map<Product, ProductResource>(result.product);
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

            var imageUpload = new ImageUpload();

            Product product = new Product()
            {

                productName = responseBody.productName,
                productAmount = responseBody.productAmount,
                productDescription = responseBody.productDescription,
                categoryId = responseBody.categoryId,
                productAvailabilty = responseBody.productAvailabilty
            };
            if (!(string.IsNullOrEmpty(responseBody.productImageFilePath)))
            {
                product.productImageUrlPath = imageUpload.ImageUploads(responseBody.productImageFilePath);
            };
            var result = await _productService.UpdateAsync(Id, product);

            if (!result.success)
                return BadRequest(result.message);

            var productResource = _mapper.Map<Product, UpdateProductResource>(result.product);

            return Ok(productResource);
        }

        [HttpDelete]
        [Route("/api/[controller]/RemoveProduct")]
        public async Task<IActionResult> RemoveProduct([FromQuery]string Id)
        {
            var deleteResult = await _productService.DeleteAsync(Id);

            if (!deleteResult.success)
                return BadRequest(deleteResult.message);

            var productResource = _mapper.Map<Product, ProductService>(deleteResult.product);
            return Ok(productResource);

        }
    }
}