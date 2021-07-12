using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace aduaba.api.Services
{
    public class ProductService : IProductInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ProductResponse> DeleteAsync(string Id)
        {
            var existingProduct = await _context.Product.FindAsync(Id);

            if (existingProduct == null)
                return new ProductResponse("Product Not Found");

            try
            {
                _context.Product.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> ListAysnc()
        {
            return await _context.Product.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetListOfProductsByNameAsync(string ProductName)
        {
            var products = await ListAysnc();

            if (!string.IsNullOrEmpty(ProductName))
                products = products.Where(d => d.productName.Contains(ProductName)).ToList();

            return products;

        }

        public async Task<IEnumerable<Product>> ListProductByCategoryIdAsync(string CategoryId)
        {
            var products = await _context.Product
                                .Where(s => s.CategoryId == CategoryId)
                                .ToListAsync();

            return products;
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _context.Product.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred while saving the products : {ex.Message}");
            }
        }

        public async Task<Product> GetProductById(string ProductId)
        {
            var product = await _context.Product.FindAsync(ProductId);

            return product;
        }


    }
}