using System;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class WishListItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public WishList wishList { get; set; }
        public string wishListId { get; set; }        
        
    }
}