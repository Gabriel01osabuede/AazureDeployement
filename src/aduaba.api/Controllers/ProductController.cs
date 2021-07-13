using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Resource;
using aduaba.api.Services;
using aduaba.api.Entities.ApplicationEntity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using aduaba.api.AppDbContext;

namespace aduaba.api.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductInterface _productService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IImageUpload _imageUpload;

        public ProductController(ApplicationDbContext context, IProductInterface productService, IMapper mapper, IImageUpload imageUpload)
        {
            _productService = productService;
            _mapper = mapper;
            _context = context;
            _imageUpload = imageUpload;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("/api/[controller]/AddProduct")]
        public async Task<IActionResult> PostProductAsync([FromBody] AddProductResource addProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            string imagePath = addProduct.productImageFilePath;
            var convertToBase64 = ImageUpload.GetBase64StringForImage(imagePath);

            Product product = new Product()
            {

                productName = addProduct.productName,
                productAmount = addProduct.productAmount,
                productDescription = addProduct.productDescription,
                ManufactureName = addProduct.ManufactureName,
                productImageUrlPath = _imageUpload.ImageUploads(convertToBase64),
                CategoryId = addProduct.categoryId
            };

            var result = await _productService.SaveAsync(product);

            if (!result.success)
                return BadRequest(result.message);

            var productResource = _mapper.Map<Product, ProductResource>(result.product);
            return Ok(productResource);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/GetAllProductsByCategory")]
        public async Task<IEnumerable<ProductResource>> GetProductsBYCategoryId([FromQuery] string CategoryId)
        {
            var allProduct = await _productService.ListProductByCategoryIdAsync(CategoryId);
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(allProduct);

            return resources;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/GetPoductById")]
        public async Task<ProductResource> GetProductById([FromQuery] string ProductId)
        {
            var product = await _productService.GetProductById(ProductId);
            var resource = _mapper.Map<Product, ProductResource>(product);

            return resource;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/GetPoductByName")]
        public async Task<IEnumerable<ProductResource>> GetProductByName([FromBody] GetProductByName model)
        {
            var product = await _productService.GetListOfProductsByNameAsync(model.productName);
            var resource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(product);

            return resource;

        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]/GetAllProducts")]
        public async Task<IEnumerable<ProductResource>> GetAllProductAsync()
        {
            var product = await _productService.ListAysnc();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(product);

            return resources;
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [Route("/api/[controller]/UpdateProduct")]
        public async Task<IActionResult> UpdateProductById([FromQuery] string Id, [FromBody] UpdateProductResource responseBody)
        {
            var existingProduct = await _context.Product.FindAsync(Id);
            if(existingProduct != null)
            {

                existingProduct.productName = responseBody.productName;
                existingProduct.productAmount = responseBody.productAmount;
                existingProduct.productDescription = responseBody.productDescription;
                existingProduct.ManufactureName = responseBody.ManufactureName;
                existingProduct.CategoryId = responseBody.categoryId;
                existingProduct.productAvailabilty = responseBody.productAvailabilty;

                if (!(string.IsNullOrEmpty(responseBody.productImageFilePath)))
                {
                    var convertToBase64 = ImageUpload.GetBase64StringForImage(responseBody.productImageFilePath);
                    existingProduct.productImageUrlPath = _imageUpload.ImageUploads(convertToBase64);
                };

                var update = _context.Product.Update(existingProduct);
                await _context.SaveChangesAsync();
                 return Ok(existingProduct);
            }
            else{
                return NotFound("Product Not Found");
            }

    }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("/api/[controller]/RemoveProduct")]
        public async Task<IActionResult> RemoveProduct([FromQuery] string Id)
        {
            var deleteResult = await _productService.DeleteAsync(Id);

            if (!deleteResult.success)
                return BadRequest(deleteResult.message);

            var productResource = _mapper.Map<Product, ProductService>(deleteResult.product);
            return Ok(productResource);

        }
    }
}