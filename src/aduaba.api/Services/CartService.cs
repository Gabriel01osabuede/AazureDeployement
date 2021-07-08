using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddToCart(string ProductId, string UserId, int Quantity = 1)
        {
            List<CartItems> cartItems = new List<CartItems>();
            CartItems existingProduct = default;

            var userCart = await _context.cart.Include(p => p.CartItem)
                                .Where(p => p.UserId == UserId).ToListAsync();
            var product = await _context.Product.FirstOrDefaultAsync(c => c.productId == ProductId);
            if (userCart == null)
            {
                CartItems cartItem = new CartItems
                {
                    Quantity = Quantity,
                    ProductId = ProductId,
                    CartItemTotalPrice = product.productAmount * Quantity,
                };
                cartItems.Add(cartItem);
                Cart cart = new Cart
                {
                    CartItem = cartItems,
                    UserId = UserId
                };
                await _context.cart.AddAsync(cart);
            }
            else
            {
                foreach (var item in userCart)
                {
                    existingProduct = item.CartItem.FirstOrDefault(p => p.ProductId == ProductId);
                    if (existingProduct != null) break;
                }
                if (existingProduct == null)
                {
                    var cartAdded = new Cart
                    {
                        CartItem = cartItems,
                        UserId = UserId
                    };
                    CartItems cartItem = new CartItems
                    {
                        Quantity = Quantity,
                        ProductId = ProductId,
                        CartItemTotalPrice = product.productAmount * Quantity,
                    };
                    cartItems.Add(cartItem);

                    await _context.cart.AddAsync(cartAdded);
                }
                else
                {
                    existingProduct.Quantity += Quantity;
                    existingProduct.CartItemTotalPrice = product.productAmount * existingProduct.Quantity;
                }

            }
            await _unitOfWork.CompleteAsync();
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

        public async Task<List<CartItems>> GetCart(string UserId)
        {
            if (UserId == null) throw new ArgumentNullException(nameof(UserId));
            else
            {
                CartItems cartList = default;
                var productsInWishList = new List<CartItems>();
                var customerWishList = await _context.cart.Include(c => c.CartItem).Where(c => c.UserId == UserId).ToListAsync();
                if (customerWishList == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in customerWishList)
                    {
                        foreach (var cart in item.CartItem)
                        {
                            cart.Product = _context.Product.First(c => c.productId == cart.ProductId);
                            cartList = new CartItems
                            {
                                Id = cart.CartId,
                                Product = cart.Product,
                                Quantity = cart.Quantity,


                            };
                            productsInWishList.Add(cartList);
                        }
                    }
                    return productsInWishList;

                }
            }
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