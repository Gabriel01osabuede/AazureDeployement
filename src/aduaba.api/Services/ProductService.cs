using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using aduaba.data.AppDbContext;
using aduaba.data.Entities.ApplicationEntity;
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

            if (!(string.IsNullOrEmpty(product.ProductImageUrlPath)))
            {
                existingProduct.ProductImageUrlPath = product.ProductImageUrlPath;
            }
            if(!(string.IsNullOrEmpty(product.ProductName)))
            {
                existingProduct.ProductName = product.ProductName;
            }
            if (!product.ProductAmount.Equals(null))
            {
                existingProduct.ProductAmount = product.ProductAmount;
            }
            if(!(string.IsNullOrEmpty(product.ProductDescription)))
            {
                existingProduct.ProductDescription = product.ProductDescription;
            }

            existingProduct.ProductAvailabilty = product.ProductAvailabilty;

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