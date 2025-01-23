using Microsoft.EntityFrameworkCore;

namespace RecordShop.Data
{
    public interface IDbContext
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

    }
}
