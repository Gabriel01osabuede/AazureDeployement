using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class CartResponse : BaseResponse
    {
        public Cart cart { get; set; }
        
        
        public CartResponse(bool success, string message, Cart cart) : base(success,message)
        {
            this.cart = cart;
        }

        public CartResponse(Cart cart)  : this(true,string.Empty,cart){}
        public CartResponse(string message): this(false,message,null){}
    }
}