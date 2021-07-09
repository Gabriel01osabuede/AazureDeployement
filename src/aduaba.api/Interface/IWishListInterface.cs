using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Models.Communication;

namespace aduaba.api.Interface
{
    public interface IWishListInterface
    {
         Task<WishListResponse> SaveAsync(WishList wishList);
         Task<WishListResponse> DeleteAsync(string Id);
        Task AddToWishList(string ProductId, string UserId);
        Task<List<WishListItem>> GetWishList(string UserId);

    }
}