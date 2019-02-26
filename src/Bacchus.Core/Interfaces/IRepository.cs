using System.Collections.Generic;

namespace Bacchus.Core.Interfaces
{
    // Based on  ASP.NET Core 2.1 reference application https://github.com/dotnet-architecture/eShopOnWeb
    public interface IRepository<T> 
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(ISpecification<T> spec);
    }
}
