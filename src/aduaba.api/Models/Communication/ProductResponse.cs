using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product product { get; private set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            this.product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product) { }
        public ProductResponse(string message) : this(false, message, null) { }
    }
}