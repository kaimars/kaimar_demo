using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bacchus.Core.Interfaces
{
    // Based on  ASP.NET Core 2.1 reference application https://github.com/dotnet-architecture/eShopOnWeb
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool isPagingEnabled { get;}
    }
}
