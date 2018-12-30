using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bacchus.Core.Interfaces;
using Bacchus.Core.Entities;

namespace Bacchus.CustomerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public string SelectedCategory { get; private set;}
        public IReadOnlyList<Auction> Auctions {get; private set;}
        public IReadOnlyList<String> ProductCategories {get; private set;}

        public IndexModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        public async Task OnGet(string category)
        {
            this.SelectedCategory = category == null ? "" : category;
            this.ProductCategories = await _auctionService.ListAvailableProductCategoriesAsync();
            this.Auctions = await _auctionService.ListRunningAuctionsAsync(category);
        }
    }
}
