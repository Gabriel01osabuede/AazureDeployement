using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class CartItems
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Product Product { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public string CartId { get; set; }

        [Column(TypeName ="Money")]
        public decimal CartItemTotalPrice { get; set; }
        
        
          
    }
}