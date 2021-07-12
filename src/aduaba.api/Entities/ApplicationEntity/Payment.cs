using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Payment
    {
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentReferenceNumber { get; set; }
        public DateTime PaymentDateTime { get; set; } = DateTime.Now;
        [Required]
        public string PaymentStatus { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual Order Order { get; set; }
        public string OrderId { get; set; }
    }
}