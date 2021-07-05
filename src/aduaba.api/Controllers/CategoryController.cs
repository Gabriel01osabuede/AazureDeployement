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
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryInterface categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("/api/[controller]/GetCategories")]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
        }

        [HttpPost]
        [Route("/api/[controller]/PostCategory")]
        public async Task<IActionResult> PostAsync(AddCategoryResource addresource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            // var Category = new Category() {

            //     CategoryName = addresource.CategoryName,
            //     CategoryImage = ImageUpload.ImageUploads(addresource.CategoryImageFilePath)
            // };
            addresource.categoryImageFilePath = ImageUpload.ImageUploads(addresource.categoryImageFilePath);
            var category = _mapper.Map<AddCategoryResource, Category>(addresource);
            var result = await _categoryService.SaveAsync(category);

            if (!result.success)
                return BadRequest(result.message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.category);
            return Ok(categoryResource);

        }

        [HttpPut]
        [Route("/api/[controller]/UpdateCategory")]
        public async Task<IActionResult> PutAsync([FromQuery] string Id,[FromBody] AddCategoryResource putResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Category = new Category()
            {
                categoryName = putResource.categoryName,
                categoryImage = ImageUpload.ImageUploads(putResource.categoryImageFilePath)
            };
            var result = await _categoryService.UpdateAsync(Id, Category);

            if (!result.success)
                return BadRequest(result.message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.category);
            return Ok(categoryResource);
        }

        [HttpDelete]
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