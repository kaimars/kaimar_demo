using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bacchus.Core.Interfaces;

namespace Bacchus.CustomerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAuctionService _auctionService;

        public IndexModel(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        public string SelectedCategory {get; set;}
        public async Task OnGet(string category)
        {
            this.SelectedCategory = category;
            var d = await _auctionService.ListRunningAuctionsAsync();
            this.SelectedCategory = d[0].ProductCategory;
        }
    }
}
