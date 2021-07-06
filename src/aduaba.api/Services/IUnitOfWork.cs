using System.Threading.Tasks;
using aduaba.api.AppDbContext;
using aduaba.api.Interface;

namespace aduaba.api.Services
{
  public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}