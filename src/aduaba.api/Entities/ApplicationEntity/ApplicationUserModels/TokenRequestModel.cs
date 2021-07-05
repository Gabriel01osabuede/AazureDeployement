using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Entities.ApplicationEntity.ApplicationUserModels
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}