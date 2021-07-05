using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Resource
{
    public class AddProductResource
    {
        [Required]
        public string productImageFilePath { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal productAmount { get; set; }
        public string categoryId { get; set; }
        public bool productAvailabilty { get; set; }
    }
}