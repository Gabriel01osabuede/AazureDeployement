using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Interface;
using aduaba.api.Resource;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aduaba.api.Controllers
{
    [ApiController]
    [Authorize]
    public class CheckOutController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
      
        
        public CheckOutController(IOrderService orderService, IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            this._orderService = orderService;
            this._mapper = mapper;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("api/Checkout")]
        public async Task<ActionResult> Checkout([FromBody] orderItemsResource orderItems)
        {
 
            if (orderItems == null)
                return BadRequest();
            else
            {
                string InStock = default;
                List<GetOrderResource> orderResource = new List<GetOrderResource>();
                var CustomerEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = await _userManager.FindByEmailAsync(CustomerEmail);
                var Items = await _orderService.GetOrderItems(orderItems.orderItemsId, Customer.Id);
                foreach(var cartItem in Items.OrderItems)
                {
                    if (cartItem.Product.productAvailabilty == true) InStock = "In Stock";
                    else InStock = "Out of Stock";
                    var orderItem = new GetOrderResource
                    {
                        OrderId = Items.Id,
                        productAvailability = InStock,
                        ProductImage = cartItem.Product.productImageUrlPath,
                        ManufacturerName = cartItem.Product.ManufactureName,
                        ProductName = cartItem.Product.productName,
                        Quantity = cartItem.Quantity,
                        Total = cartItem.CartItemTotalPrice
                    };
                    orderResource.Add(orderItem);
                }
                return Ok(orderResource);
            }
        }

        [HttpGet]
        [Route("ShippingAddress")]
        public async Task<ActionResult> GetShippingAddress(string AddressId)
        {
            // var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shippingaddress =  await _orderService.GetCustomerShippingAddress(AddressId);
            if (shippingaddress == null)
                return Ok("No Shipping Address found for this User");
            else
            {
                var mappedShippingAddress = _mapper.Map<ShowAddressResource>(shippingaddress);
                return Ok(mappedShippingAddress);
            }
        }

        [HttpPost]
        [Route("PayOnDelivery")]
       public async Task<ActionResult> OrderItem([FromBody] OrderResource order)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var orderItemsId = CheckingOutItems;
            if(order.OrderType == "PayOnDelivery")
            {
                var customerOrder = await _orderService.OrderItems(order.OrderItemId, customerId);
                OrderSuccessfulResource sucessful = new OrderSuccessfulResource
                {
                    OrderId = customerOrder.OrderReferenceNumber
                };
                return Ok($"Your order is successful. you can track your order with this reference number {sucessful.OrderId}");
            }
            else
            {
                return Ok();
            }
            
        }

        [HttpPost]
        [Route("OrderStatus")]
        public async Task<ActionResult> OrderStatus([FromBody] OrderStatusResource orderStatus)
        {
            if (orderStatus == null) return BadRequest();
            else
            {
                await _orderService.ChangeOrderStatus(orderStatus.OrderItemId, orderStatus.OrderStatus);
                return Ok("OrderStatus Changed Successfully");
            }

        }

        [HttpGet]
        [Route("OrderStatus")]
        public async Task<ActionResult> OrderStatus([FromQuery] string orderReferenceNumber)
        {
            if (orderReferenceNumber == null) return BadRequest();
            else
            {
                var orderStatus =await _orderService.TrackOrder(orderReferenceNumber);
                if (orderStatus == null) return BadRequest("No order found with this reference Number");
                else return Ok(orderStatus);             
            }

        }


    }
}