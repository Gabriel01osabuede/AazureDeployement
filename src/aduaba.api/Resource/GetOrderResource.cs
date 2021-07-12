namespace aduaba.api.Resource
{
    public class GetOrderResource
    {
        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public string ManufacturerName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string productAvailability { get; set; }
    }
}