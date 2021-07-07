using System.ComponentModel.DataAnnotations;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

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
        public string addressNumber { get; set; }

        public string userId { get; set; }

        public ApplicationUser UserName { get; set; }

    }
}