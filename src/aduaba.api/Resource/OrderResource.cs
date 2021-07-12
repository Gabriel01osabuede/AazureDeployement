using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Resource
{
    public class OrderResource
    {
        
        [Required(ErrorMessage ="OrderItemId is required")]
        public string OrderItemId { get; set; }
        [Required(ErrorMessage ="OrderType is Required")]
        public string OrderType { get; set; }
    }
}