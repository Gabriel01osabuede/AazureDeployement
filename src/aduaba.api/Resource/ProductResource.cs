namespace aduaba.api.Resource
{
    public class ProductResource
    {
        public string ProductId { get; set; }
        public string ProductImageUrlPath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductAmount { get; set; }
        public string CategoryId { get; set; }
        public bool ProductAvailabilty { get; set; }
    }
}