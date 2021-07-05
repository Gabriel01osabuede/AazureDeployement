using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Services
{
    public class CategoryService : ICategoryInterface
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryResponse> DeleteAsync(string Id)
        {
            var existingCategory = await _context.Categories.FindAsync(Id);

            if (existingCategory == null)
                return new CategoryResponse("Category Not Found");

            try
            {
                _context.Categories.Remove(existingCategory);
                await CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch(Exception ex)
            {
                return new CategoryResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse(ex.Message);
            }
        }

        public async Task<CategoryResponse> UpdateAsync(string Id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(Id);
            if (existingCategory == null)
                return new CategoryResponse("Category Not Found");
            if(!(string.IsNullOrEmpty(category.categoryName)))
            {
                existingCategory.categoryName = category.categoryName;
            }
            if(!(string.IsNullOrEmpty(category.categoryImage)))
            {
                existingCategory.categoryImage = category.categoryImage;
            }
            

            try
            {
                _context.Categories.Update(existingCategory);
                await CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch(Exception ex)
            {
                return new CategoryResponse(ex.Message);
            }
        }
    }
}