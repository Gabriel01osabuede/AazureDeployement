using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Resource;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Controllers
{
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
        [Route("/api/[controller]/AddCartItem")]
        public async Task<IEnumerable<ShowCartResource>> GetAllCartAsync()
        {
            var existingCart = await _cartService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Cart>, IEnumerable<ShowCartResource>>(existingCart);

            return resource;
        }

        [HttpPost]
        [Route("/api/[controller]/AddCartItem")]
        public async Task<IActionResult> AddItemToCart([FromQuery] string UserId, string ProductId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            //var existingProduct = await _context.Product.FindAsync(ProductId);
            var existingProduct = await _context.Product.FirstOrDefaultAsync(p => p.productId == ProductId);
            var existingUser = await _userManager.FindByIdAsync(UserId);
            if (existingProduct == null)
            {
                return BadRequest("Item Not Found");
            }
            else
            {

                Cart cartDetails = new Cart()
                {
                    cartId = Guid.NewGuid().ToString(),
                    productId = existingProduct.productId
                
                };
            
            var cartItem = _mapper.Map<Cart>(cartDetails);
            var result = await _cartService.SaveAsync(cartItem);

            if (!result.success)
                return BadRequest(result.message);

            var CartResource = _mapper.Map<ShowCartResource>(result.cart);
            return Ok(CartResource);
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