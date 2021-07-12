using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Order
    {
        public string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual Address Address { get; set; }
        public string AddressId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public string OrderReferenceNumber { get; set; }
        public string OrderType { get; set; }
        public List<CartItems> OrderItems { get; set; } = new List<CartItems>();
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalAmountToPay { get; set; }
    }
}