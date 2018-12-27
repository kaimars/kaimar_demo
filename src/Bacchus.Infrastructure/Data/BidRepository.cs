using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bacchus.Core.Entities;
using Bacchus.Core.Interfaces;

namespace Bacchus.Infrastructure.Data
{
    public class BidRepository: IAsyncRepository<Bid>
    {
        public Task<IReadOnlyList<Bid>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Bid> AddAsync(Bid entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Bid entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Bid entity)
        {
            throw new NotImplementedException();
        }

        
    }
}