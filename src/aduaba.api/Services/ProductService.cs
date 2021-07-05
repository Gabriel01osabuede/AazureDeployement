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
    public class ProductService : IProductInterface
    {
        private readonly ApplicationDbContext _context;

        public ProductService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ProductResponse> DeleteAsync(string Id)
        {
            var existingProduct = await _context.Products.FindAsync(Id);

            if (existingProduct == null)
                return new ProductResponse("Product Not Found");

            try
            {
                _context.Products.Remove(existingProduct);
                await CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch(Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> ListAysnc()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await CompleteAsync();

                return new ProductResponse(product);
            }
            catch(Exception ex)
            {
                return new ProductResponse($"An error occurred while saving the products : {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateAsync(string Id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(Id);
            if (existingProduct == null)
                return new ProductResponse("Product Not Found");

            if (!(string.IsNullOrEmpty(product.productImageUrlPath)))
            {
                existingProduct.productImageUrlPath = product.productImageUrlPath;
            }
            if(!(string.IsNullOrEmpty(product.productName)))
            {
                existingProduct.productName = product.productName;
            }
            if (!product.productAmount.Equals(null))
            {
                existingProduct.productAmount = product.productAmount;
            }
            if(!(string.IsNullOrEmpty(product.productDescription)))
            {
                existingProduct.productDescription = product.productDescription;
            }

            existingProduct.productAvailabilty = product.productAvailabilty;

            try
            {
                _context.Products.Update(product);
                await CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch(Exception ex)
            {
                return new ProductResponse($"An error occurred when Updating the product : {ex.Message}");
            }
        }
    }
}