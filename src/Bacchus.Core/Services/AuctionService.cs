using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Bacchus.Core.Interfaces;
using Bacchus.Core.Entities;
using Bacchus.Core.Exceptions;

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

        /// <summary>
        /// Returns Auction referenced by productId.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Auction> GetAuctionByProductIdAsync(Guid productId)
        {
            var auctions = await this.ListRunningAuctionsAsync();
            var result = auctions.SingleOrDefault(x => x.ProductId == productId);
            if (result == null){
                throw new AuctionNotFoundException(productId);
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
            var auctions = await _auctionRepository.ListAllAsync();
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
            var auctions = await _auctionRepository.ListAllAsync();
            return auctions
                .Where(x => (String.IsNullOrEmpty(productCategoryFilter) || (x.ProductCategory == productCategoryFilter))
                    && (x.BiddingEndDate > DateTime.Now))
                .OrderBy(x => x.BiddingEndDate)
                .ToList()
                .AsReadOnly();
        }
        
        /// <summary>
        /// Registers new Bid based on given parameters.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="username"></param>
        /// <param name="price"></param>
        /// <returns>Created Bid entity or ApplicationException in case of business rules are not followed.</returns>
        public async Task<Bid> AddBidAsync(Guid productId, string username, decimal price)
        {
            var auction = await this.GetAuctionByProductIdAsync(productId);
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new BusinessRuleViolatedException("Missing username");
            }
            if (price <= 0){
                throw new BusinessRuleViolatedException("Price must be positive number");
            }
            if (auction.BiddingEndDate < DateTime.Now){
                throw new BusinessRuleViolatedException("Bidding time for this auction is over.");
            }
            return await _bidRepository.AddAsync(new Bid(username, productId, DateTime.Now, price));
        }
        
        /// <summary>
        /// Returns all Bids.
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<Bid>> ListAllBidsAsync()
        {
            return await _bidRepository.ListAllAsync();
        }
       
    }
}