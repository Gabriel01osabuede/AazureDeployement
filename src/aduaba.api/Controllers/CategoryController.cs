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

namespace aduaba.api.Controllers
{
    [Authorize]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryService;
        private readonly IImageUpload _imageUpload;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryInterface categoryService, IMapper mapper,IImageUpload imageUpload)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _imageUpload = imageUpload;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]/GetCategories")]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
        }



        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        [Route("/api/[controller]/PostCategory")]
        public async Task<IActionResult> PostAsync([FromBody] AddCategoryResource addresource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = new Category()
            {
                categoryName = addresource.categoryName,
            };
            var convertToBase64 = ImageUpload.GetBase64StringForImage(addresource.categoryImageFilePath);
            category.categoryImage = _imageUpload.ImageUploads(convertToBase64);
            // addresource.categoryImageFilePath = ImageUpload.ImageUploads(addresource.categoryImageFilePath);
            // var category = _mapper.Map<AddCategoryResource, Category>(addresource);
            var result = await _categoryService.SaveAsync(category);

            if (!result.success)
                return BadRequest(result.message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.category);
            return Ok(categoryResource);

        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        [Route("/api/[controller]/UpdateCategory")]
        public async Task<IActionResult> PutAsync([FromQuery] string Id, [FromBody] AddCategoryResource putResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Category = new Category()
            {
                categoryName = putResource.categoryName,

            };

            var convertToBase64 = ImageUpload.GetBase64StringForImage(putResource.categoryImageFilePath);
            Category.categoryImage = _imageUpload.ImageUploads(convertToBase64);

            var result = await _categoryService.UpdateAsync(Id, Category);

            if (!result.success)
                return BadRequest(result.message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.category);
            return Ok(categoryResource);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("/api/[controller]/RemoveCategory")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string Id)
        {
            var deleteResult = await _categoryService.DeleteAsync(Id);

            if (!deleteResult.success)
                return BadRequest(deleteResult.message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(deleteResult.category);
            return Ok(categoryResource);
        }
    }
}