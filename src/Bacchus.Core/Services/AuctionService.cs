using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Bacchus.Core.Interfaces;
using Bacchus.Core.Entities;

namespace Bacchus.Core.Services
{
    public class AuctionService: IAuctionService
    {
        private readonly IListRepository<Auction> _auctionRepository;
        private readonly IAsyncRepository<Bid> _bidRepository;

        public AuctionService(IListRepository<Auction> auctionRepository, IAsyncRepository<Bid> bidRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }
        public Task<IReadOnlyList<Auction>> ListRunningAuctionsAsync()
        {
            return _auctionRepository.ListAllAsync();
        }
        public Task<Bid> AddBidAsync(Guid productId, string username)
        {
            throw new NotImplementedException();
        }
        
    }
}