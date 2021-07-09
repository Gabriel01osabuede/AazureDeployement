using System;
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
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Controllers
{
    [ApiController]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _cartService = cartService;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }


        [HttpGet]
        [Authorize]
        [Route("/api/[controller]/GetCartItem")]
        public async Task<List<ShowCartResource>> GetAllCartAsync()
        {
            ShowCartResource view = default;
            List<ShowCartResource> cartItems = new List<ShowCartResource>();
            var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Customer = await _userManager.FindByEmailAsync(CustomerEmail);

            var existingCart = await _cartService.GetCart(Customer.Id);
            foreach (var item in existingCart)
            {

                // sum = item.Product.Amount * item.Quantity;
                view = new ShowCartResource
                {
                    productId = item.Product.productId,
                    cartId = item.Id,
                    productImageUrl = item.Product.productImageUrlPath,
                    productName = item.Product.productName,
                    productAmount = item.Product.productAmount,
                    productQuantityPurchased = item.Quantity,
                    productAvailability = item.Product.productAvailabilty
                };
                cartItems.Add(view);
            }
            //var resource = _mapper.Map<List<Cart>, List<ShowCartResource>>(cartItems);
            return cartItems;
            


        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/AddCartItem")]
        public async Task<IActionResult> AddItemToCart([FromQuery] string productId, int quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = await _userManager.FindByEmailAsync(CustomerEmail);
                await _cartService.AddToCart(productId, Customer.Id, quantity);

                return Ok("Item added Successfully.");
            }
        }

        [HttpPut]
        [Route("/api/[controller]/UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromQuery] string ProductId, [FromBody] UpdateCart model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cart = _mapper.Map<UpdateCart, Cart>(model);
            var result = await _cartService.UpdateAsync(ProductId, cart);

            if (!result.success)
                return BadRequest(result.message);

            var cartResource = _mapper.Map<Cart, ShowCartResource>(result.cart);
            return Ok(cartResource);
        }


        [HttpDelete]
        [Route("/api/[controller]/RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem([FromQuery] string ProductId)
        {
            var deleteCartItemAsync = await _cartService.DeleteAsync(ProductId);

            if (!deleteCartItemAsync.success)
                return BadRequest(deleteCartItemAsync.message);

            var cartResource = _mapper.Map<Cart, ShowCartResource>(deleteCartItemAsync.cart);
            return Ok(cartResource);
        }
    }
}