using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Cart
    {
        public string cartId { get; set; } = Guid.NewGuid().ToString();
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        
        public List<CartItems> CartItem { get; set; }


        //public Product products { get; set; }
        //public string productImageUrl { get; set; }
        //public string productName { get; set; }
        //public string manufacturerName { get; set; }
        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal productAmount { get; set; }
        //public int productQuantityPurchased { get; set; }
        //public bool productAvailability { get; set; }









    }
}