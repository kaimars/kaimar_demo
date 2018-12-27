using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacchus.Core.Interfaces
{
    public interface IListRepository<T>
    {
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}