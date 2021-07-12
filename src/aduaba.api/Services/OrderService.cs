using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Interface;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.Services
{
    public class OrderService : IOrderService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        
        public OrderService(ApplicationDbContext context,IMapper mapper, ICartService cart)
        {
            
            _context = context;
            
            _cartService = cart;
        }
        public async Task<Address> GetCustomerShippingAddress(string AddressId)
        {
            var shippingAddress = await _context.addresses.FindAsync(AddressId);
            return shippingAddress;
        }

        public async Task<Order> GetOrderItems(List<string> orderItemId, string customerId)
        {
            Order order = default;
            List<CartItems> cartItems = new List<CartItems>();
            decimal total = default;
            ApplicationUser customer = default;
            foreach (var item in orderItemId)
            {
                var foundItems = await _context.CartItems.FirstOrDefaultAsync(c => c.CartId == item);
                var productInFoundItem = await _context.Product.FirstOrDefaultAsync(c => c.Id == foundItems.ProductId);
                customer = await _context.Users.FirstOrDefaultAsync(c => c.Id == customerId);
                foundItems.Product = productInFoundItem;

                cartItems.Add(foundItems);
                
                    total += foundItems.CartItemTotalPrice;      
            }
            order = new Order
            {
                OrderItems = cartItems,
                OrderDate = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                User = customer,
                TotalAmountToPay = total,
                OrderReferenceNumber = GetOrderReference,
                OrderStatus = new OrderStatus
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = "Awaiting Payment",
                    PaymentStatus = false
                }
            };
            _context.Add(order);
            _context.SaveChanges();
            return order;         
        }


        public Task<List<Order>> OrderItems(string customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> OrderItems(string orderId, string customerId)
        {
            var orderItems = await _context.Order.Include(c => c.OrderStatus).Include(c=> c.Address).Include(c => c.User)
                .Include(c => c.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
            var shippingAddress = await _context.addresses.FirstOrDefaultAsync(a => a.UserId == customerId);

            orderItems.OrderType = "Payment on delivery";
            orderItems.Address = shippingAddress;
            orderItems.OrderReferenceNumber = GetOrderReference;
            var id = orderItems.OrderStatus.Id;
            var orderstaus = await _context.OrderStatus.FirstOrDefaultAsync(c => c.Id == id);
            orderstaus.Status = "Shipping in Progress";
            orderItems.OrderStatus = orderstaus;
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == customerId);
            
            foreach(var item in orderItems.OrderItems)
            {
                await _cartService.DeleteAsync(item.CartId);
            }
            // var message = "<h3>Order Place Successfully.</h3>" + $"<p>Thanks for shopping from aduaba. " +
            //     $"We wish to see you often.<br>To track your order go on your profile and click orders and you will be able to track your order</p>";
            // _emailSender.SendEmailAsync(user.Email, "Order Completed Successfully", message);

            await _context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<OrderStatus> TrackOrder(string trackingId)
        {
            OrderStatus orderStatus = default;
            var order = await _context.Order.Include(c => c.OrderStatus).FirstOrDefaultAsync(c => c.OrderReferenceNumber == trackingId);
            orderStatus = order.OrderStatus;
            return orderStatus;
        }
        public async Task ChangeOrderStatus(string orderId, string orderStatus)
        {
            var order = await _context.Order.Include(c => c.OrderStatus).FirstOrDefaultAsync(a => a.Id == orderId);
            var id = order.OrderStatus.Id;
            var orderstaus = await _context.OrderStatus.FirstOrDefaultAsync(c => c.Id == id);
            orderstaus.Status = orderStatus;
            order.OrderStatus = orderstaus;
            await _context.SaveChangesAsync();
        }

        private async Task<decimal> CalculateTotalBilling(List<string> cartItems)
        {
            decimal SubTotal = default;
            foreach (var items in cartItems)
            {
                var foundItems = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == items);
                var productInFoundItem = await _context.Product.FirstOrDefaultAsync(c => c.Id == foundItems.ProductId);
                foundItems.Product = productInFoundItem;
                decimal total = foundItems.Product.productAmount * foundItems.Quantity;
                SubTotal += total;
            }
            return SubTotal;
        }

        public async Task ChangePaymentStatus(string orderId)
        {
            var order = await _context.Order.FirstOrDefaultAsync(a => a.Id == orderId);
        }


        public string GetOrderReference
        {
            get
            {
                return RandomString(6);
            }

        }
        private static Random random = new Random();
        private static string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}