using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Bacchus.Core.Interfaces;
using Bacchus.Core.Entities;
using System.Runtime.Caching;

namespace Bacchus.Core.Services
{
    /// <summary>
    /// This class is responsible for all Auction-related business services.
    /// </summary>
    public class AuctionService: IAuctionService
    {
        private readonly IListRepository<Auction> _auctionRepository;
        private readonly IAsyncRepository<Bid> _bidRepository;

        public AuctionService(IListRepository<Auction> auctionRepository, IAsyncRepository<Bid> bidRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }

        private async Task<IReadOnlyList<Auction>> ListRunningAuctionsAsync()
        {
            ObjectCache cache = MemoryCache.Default;
            var result = cache["ListRunningAuctionsAsync"] as IReadOnlyList<Auction>;
            if (result == null){
                result = await _auctionRepository.ListAllAsync();
                cache.Set("ListRunningAuctionsAsync", result, DateTime.Now.AddMinutes(1));
            }
            return result;
        }

        /// <summary>
        /// Returns all distinct product categories which have currently running auctions.
        /// Result is sorted alphabetically.
        /// </summary>
        /// <returns>List of Auction.ProductCategory, may be empty.</returns>
        public async Task<IReadOnlyList<String>> ListAvailableProductCategoriesAsync()
        {
            var auctions = await this.ListRunningAuctionsAsync();
            return auctions
                .Select(x => x.ProductCategory)
                .Distinct()
                .OrderBy(x => x)
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Returns all running Auctions based on current time and optional productCategoryFilter.
        /// Returned items are sorted by auction end date (ending sooner first).
        /// </summary>
        /// <param name="productCategoryFilter">Optional filter for Auction.ProductCategory</param>
        /// <returns>List of found Auctions, may be empty.</returns>
        public async Task<IReadOnlyList<Auction>> ListRunningAuctionsAsync(String productCategoryFilter=null)
        {
            var auctions = await this.ListRunningAuctionsAsync();
            return auctions
                .Where(x => (String.IsNullOrEmpty(productCategoryFilter) || (x.ProductCategory == productCategoryFilter))
                    && (x.BiddingEndDate > DateTime.Now))
                .OrderBy(x => x.BiddingEndDate)
                .ToList()
                .AsReadOnly();
        }
        public Task<Bid> AddBidAsync(Guid productId, string username)
        {
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<Bid>> ListAllBidsAsync()
        {
            throw new NotImplementedException();
        }
       
    }
}