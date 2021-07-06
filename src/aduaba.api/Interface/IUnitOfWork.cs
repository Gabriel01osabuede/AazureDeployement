using System.Threading.Tasks;

namespace aduaba.api.Interface
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}