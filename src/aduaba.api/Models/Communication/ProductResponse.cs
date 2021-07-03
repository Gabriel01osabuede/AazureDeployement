using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; private set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product) { }
        public ProductResponse(string message) : this(false, message, null) { }
    }
}