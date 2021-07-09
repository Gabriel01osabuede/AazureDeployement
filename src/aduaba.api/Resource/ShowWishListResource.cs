using System.ComponentModel.DataAnnotations.Schema;

namespace aduaba.api.Resource
{
    public class ShowWishListResource
    {
        
        public string Id { get; set; }
        public string productId { get; set; }
        public string productImageUrl { get; set; }
        public string productName { get; set; }
        public string manufacturerName { get; set; }
        [Column(TypeName = "Money")]
        public decimal productAmount { get; set; }
        public bool productAvailability { get; set; }
    }
}