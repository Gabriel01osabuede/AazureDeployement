using System;
using System.Collections.Generic;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class WishList
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<WishListItem> WishListItems { get; set; }
        
        
        
        
        
        
        
        
    }
}