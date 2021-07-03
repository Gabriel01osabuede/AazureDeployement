using System.Collections.Generic;
using System.Threading.Tasks;
using aduaba.api.Models.Communication;
using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Interface
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<CategoryResponse> SaveAsync(Category category);
        Task<CategoryResponse> UpdateAsync(string Id, Category category);
        Task<CategoryResponse> DeleteAsync(string Id);
    }
}