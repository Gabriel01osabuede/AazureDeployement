using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Resource;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aduaba.api.Controllers
{
    [Authorize]
    [ApiController]
    public class WishListController : Controller
    {
        private readonly IWishListInterface _wishListService;
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public WishListController(IWishListInterface wishListService, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _wishListService = wishListService;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/GetWishListItem")]
        public async Task<List<ShowWishListResource>> GetAllCartAsync()
        {
            ShowWishListResource view = default;
            List<ShowWishListResource> wishListItems = new List<ShowWishListResource>();
            var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Customer = await _userManager.FindByEmailAsync(CustomerEmail);

            var existingCart = await _wishListService.GetWishList(Customer.Id);
            foreach (var item in existingCart)
            {

                view = new ShowWishListResource
                {
                    productId = item.Product.productId,
                    Id = item.Id,
                    productImageUrl = item.Product.productImageUrlPath,
                    productName = item.Product.productName,
                    productAmount = item.Product.productAmount,
                    productAvailability = item.Product.productAvailabilty
                };
                wishListItems.Add(view);
            }
            return wishListItems;
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/AddWishListItem")]
        public async Task<IActionResult> AddItemToWishList([FromQuery] string productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = await _userManager.FindByEmailAsync(CustomerEmail);
                await _wishListService.AddToWishList(productId, Customer.Id);

                return Ok("Item added Successfully.");
            }
        }

        [HttpDelete]
        [Route("/api/[controller]/RemoveWishListItem")]
        public async Task<IActionResult> RemoveCartItem([FromQuery] string ProductId)
        {
            var deleteWishListItemAsync = await _wishListService.DeleteAsync(ProductId);

            if (!deleteWishListItemAsync.success)
                return BadRequest(deleteWishListItemAsync.message);

            var cartResource = _mapper.Map<WishList, ShowWishListResource>(deleteWishListItemAsync.wishList);
            return Ok(cartResource);
        }
    }

}