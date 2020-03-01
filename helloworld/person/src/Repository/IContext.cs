using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public interface IContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}



