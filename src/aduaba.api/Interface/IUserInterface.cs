using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;

namespace aduaba.api.Interface
{
    public interface IUserInterface
    {
        Task<string> RegisterAsync(RegisterUser model);
        Task<AuthenticationModel> UserLoginAsync(TokenRequestModel model);
        Task<AuthenticationModel> UpdateUserAsync(string Id, ApplicationUser model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        ApplicationUser GetById(string id);
        bool RevokeToken(string token);
    }
}