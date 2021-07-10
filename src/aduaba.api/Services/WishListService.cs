using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Interface;
using aduaba.api.Models.Communication;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Services
{
    public class WishListService : IWishListInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public WishListService(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task AddToWishList(string ProductId, string UserId)
        {
            List<WishListItem> wishListItems = new List<WishListItem>();
            WishListItem existingProduct = default;

            var userWishList = await _context.WishList.Include(p => p.WishListItems)
                                .Where(p => p.UserId == UserId).ToListAsync();
            var product = await _context.Product.FirstOrDefaultAsync(c => c.productId == ProductId);
            if (userWishList == null)
            {
                WishListItem wishListItem = new WishListItem
                {
                    ProductId = ProductId,
                  
                };
                wishListItems.Add(wishListItem);
                WishList wishList = new WishList
                {
                    WishListItems = wishListItems,
                    UserId = UserId
                };
                await _context.WishList.AddAsync(wishList);
            }
            else
            {
                foreach (var item in userWishList)
                {
                    existingProduct = item.WishListItems.FirstOrDefault(p => p.ProductId == ProductId);
                    if (existingProduct != null) break;
                }
                if (existingProduct == null)
                {
                    var wishListAdded = new WishList
                    {
                        WishListItems = wishListItems,
                        UserId = UserId
                    };
                    WishListItem wishListItem = new WishListItem
                    {
                        
                        ProductId = ProductId,
                    };
                    wishListItems.Add(wishListItem);

                    await _context.WishList.AddAsync(wishListAdded);
                }
                else
                {
                    existingProduct.ProductId = ProductId;
                }

            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task<WishListResponse> DeleteAsync(string Id)
        {
             var existingWishList = await _context.WishList.FindAsync(Id);
            if (existingWishList == null)
                return new WishListResponse("Item not Found In Cart");
            try
            {
                _context.WishList.Remove(existingWishList);
                await _unitOfWork.CompleteAsync();

                return new WishListResponse(existingWishList);
            }
            catch (Exception ex)
            {
                return new WishListResponse(ex.Message);
            }
        }

        public async Task<List<WishListItem>> GetWishList(string UserId)
        {
            if (UserId == null) throw new ArgumentNullException(nameof(UserId));
            else
            {
                WishListItem wishListItem = default;
                var productsInWishList = new List<WishListItem>();
                var customerWishList = await _context.WishList.Include(c => c.WishListItems).Where(c => c.UserId == UserId).ToListAsync();
                if (customerWishList == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in customerWishList)
                    {
                        foreach (var wishList in item.WishListItems)
                        {
                            wishList.Product = _context.Product.First(c => c.productId == wishList.ProductId);
                            wishListItem = new WishListItem
                            {
                                Id = wishList.Id,
                                Product = wishList.Product


                            };
                            productsInWishList.Add(wishListItem);
                        }
                    }
                    return productsInWishList;

                }
            }
        }
    }
}