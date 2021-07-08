using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartResponse> DeleteAsync(string Id)
        {

            var existingCart = await _context.cart.FindAsync(Id);
            if (existingCart == null)
                return new CartResponse("Item not Found In Cart");
            try
            {
                _context.cart.Remove(existingCart);
                await _unitOfWork.CompleteAsync();

                return new CartResponse(existingCart);
            }
            catch (Exception ex)
            {
                return new CartResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Cart>> ListAsync()
        {
            return await _context.cart.ToListAsync();
        }

        public async Task<CartResponse> SaveAsync(Cart cart)
        {
            try
            {
                await _context.cart.AddAsync(cart);
                await _unitOfWork.CompleteAsync();

                return new CartResponse(cart);
            }
            catch (Exception ex)
            {
                return new CartResponse(ex.Message);
            }
        }

        public async Task<CartResponse> UpdateAsync(string Id, Cart cart)
        {
            var existingCart = await _context.cart.FindAsync(Id);
            if (existingCart == null)
                return new CartResponse("Item Not Found.");

            //existingCart.productQuantityPurchased += cart.productQuantityPurchased;
            try
            {
                _context.cart.Update(existingCart);
                await _unitOfWork.CompleteAsync();

                return new CartResponse(existingCart);
            }
            catch (Exception ex)
            {
                return new CartResponse(ex.Message);
            }

        }
    }
}