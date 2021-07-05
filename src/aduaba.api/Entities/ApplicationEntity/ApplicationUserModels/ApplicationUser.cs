using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace aduaba.api.Entities.ApplicationEntity.ApplicationUserModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public List<RefreshUserToken> RefreshTokens { get; set; }
    }
}