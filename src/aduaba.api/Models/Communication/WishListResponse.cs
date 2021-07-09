using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class WishListResponse : BaseResponse
    {
        public WishList wishList { get; set; }
        
        
        public WishListResponse(bool success, string message, WishList wishList) : base(success,message)
        {
            this.wishList = wishList;
        }

        public WishListResponse(WishList wishList)  : this(true,string.Empty,wishList){}
        public WishListResponse(string message): this(false,message,null){}
    }
}