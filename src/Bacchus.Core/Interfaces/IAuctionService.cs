using System;
using Bacchus.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacchus.Core.Interfaces
{
    public interface IAuctionService
    {
        Task<IReadOnlyList<Auction>> ListRunningAuctionsAsync();
        Task<Bid> AddBidAsync(Guid productId, string username);
    }
}