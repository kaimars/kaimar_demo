using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacchus.Core.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
