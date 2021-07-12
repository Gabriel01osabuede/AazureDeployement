using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Interface
{
    public interface IOrderService
    {
        Task<OrderStatus> TrackOrder(string trackingId);
        Task ChangeOrderStatus(string orderId, string orderStatus);
        Task<Order> GetOrderItems(List<string> orderItemId, string customerId);
        Task<Address> GetCustomerShippingAddress(string customerId);
        
        Task<Order> OrderItems(string orderId, string customerId);
        Task<List<Order>> OrderItems(string customerId);
    }
}