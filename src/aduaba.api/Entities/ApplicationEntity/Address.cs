using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Address
    {
        [Key]
        public string addressId { get; set; }
        [Required]
        public string country { get; set; }

         [Required]
        public string state { get; set; }
        
         [Required]
        public string addressNo { get; set; }

        public string userId { get; set; }

        // public ApplicationUser UserName { get; set; }

        public string id { get; set; }
    }
}