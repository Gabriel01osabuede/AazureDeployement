using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Models.Communication;
using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Interface
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> ListAysnc();
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(string Id, Product product);
        Task<ProductResponse> DeleteAsync(string Id);
    }
}