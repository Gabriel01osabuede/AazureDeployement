using System.ComponentModel.DataAnnotations;

namespace aduaba.data.Entities.ApplicationEntity
{
    public class Address
    {
        [Key]
        public string AddressId { get; set; }
        [Required]
        public string Country { get; set; }

         [Required]
        public string State { get; set; }
        
         [Required]
        public string AddressNo { get; set; }

        public string UserId { get; set; }

        // public ApplicationUser UserName { get; set; }

        public string Id { get; set; }
    }
}