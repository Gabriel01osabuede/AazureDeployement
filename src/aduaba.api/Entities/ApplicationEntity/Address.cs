using System;
using System.ComponentModel.DataAnnotations;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Address
    {
        [Key]
        public string AddressId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string AdditionalPhoneNumber { get; set; }
        [Required]
        public string UserAddress { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}