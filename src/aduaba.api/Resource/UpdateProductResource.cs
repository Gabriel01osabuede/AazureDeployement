namespace aduaba.api.Resource
{
    public class UpdateProductResource
    {
        public string ProductImageFilePath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductAmount { get; set; }
        public string CategoryId { get; set; }
        public bool ProductAvailabilty { get; set; }
    }
}