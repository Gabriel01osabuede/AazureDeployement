using System;
using System.ComponentModel.DataAnnotations;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Address
    {
        [Key]
        public string addressId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string country { get; set; }
        [Required]
        public string state { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string localGovernmentArea { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}