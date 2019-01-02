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
    public class BidListModel : PageModel
    {
        private readonly IAuctionService _auctionService;
        public IReadOnlyList<Bid> Bids { get; private set; }

        public BidListModel(IAuctionService auctionService)
        {
            this._auctionService = auctionService;
        }


        public async void OnGet()
        {
            this.Bids = await _auctionService.ListAllBidsAsync();
        }
    }
}