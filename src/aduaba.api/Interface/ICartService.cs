using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Models.Communication;

namespace aduaba.api.Interface
{
    public interface ICartService
    {
       Task<IEnumerable<Cart>> ListAsync();
       Task<CartResponse> SaveAsync(Cart cart);
       Task<CartResponse> UpdateAsync(string Id, Cart cart);
       Task<CartResponse> DeleteAsync(string Id);
    }
}