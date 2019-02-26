using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacchus.Core.Interfaces
{
    // Based on  ASP.NET Core 2.1 reference application https://github.com/dotnet-architecture/eShopOnWeb
    public interface IAsyncRepository<T>
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
