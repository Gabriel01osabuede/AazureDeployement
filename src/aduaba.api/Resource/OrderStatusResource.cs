using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Resource
{
    public class OrderStatusResource
    {
        [Required(ErrorMessage ="OrderItemId is required")]
        public string OrderItemId { get; set; }
        [Required(ErrorMessage = "OrderStatus is required")]
        public string OrderStatus { get; set; }
    }
}