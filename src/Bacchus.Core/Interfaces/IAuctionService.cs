using System;
using Bacchus.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacchus.Core.Interfaces
{
    public interface IAuctionService
    {
        /// <summary>
        /// Returns all running Auctions based on current time and optional productCategoryFilter.
        /// Returned items are sorted by auction end date (ending sooner first).
        /// </summary>
        /// <param name="productCategoryFilter">Optional filter for Auction.ProductCategory</param>
        /// <returns>List of found Auctions, may be empty.</returns>
        Task<IReadOnlyList<Auction>> ListRunningAuctionsAsync(String productCategoryFilter=null);
        
        /// <summary>
        /// Returns Auction referenced by productId.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Auction> GetAuctionByProductIdAsync(Guid productId);

        /// <summary>
        /// Returns all distinct product categories which have currently running auctions.
        /// Result is sorted alphabetically.
        /// </summary>
        /// <returns>List of Auction.ProductCategory, may be empty.</returns>
        Task<IReadOnlyList<String>> ListAvailableProductCategoriesAsync();

        Task<Bid> AddBidAsync(Guid productId, string username, decimal price);
        Task<IReadOnlyList<Bid>> ListAllBidsAsync();
   }
}