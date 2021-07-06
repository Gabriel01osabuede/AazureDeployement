using System.ComponentModel.DataAnnotations.Schema;

namespace aduaba.api.Resource
{
    public class ShowCartResource
    {

        public string cartId { get; set; }
        public string productId { get; set; }
        public string productImageUrl { get; set; }
        public string productName { get; set; }
        public string manufacturerName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal productAmount { get; set; }
        public int productQuantityPurchased { get; set; }
        public bool productAvailability { get; set; }
    }
}