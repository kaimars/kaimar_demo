using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bacchus.Core.Interfaces;
using Bacchus.Core.Entities;
using Bacchus.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Bacchus.CustomerWeb.Pages
{
    public class BidModel : PageModel
    {
        private readonly IAuctionService _auctionService;

        [BindProperty]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000)]
        public decimal Price {get; set;}

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public String Username {get; set;}

        [BindProperty]
        public Auction Auction { get; set; }

        public bool Completed { get; set; }

        public BidModel(IAuctionService auctionService)
        {
            this._auctionService = auctionService;
        }


        public async Task<IActionResult> OnGet(Guid productId)
        {
            this.Username = Request.Cookies["Username"];
            try
            {
                this.Auction = await this._auctionService.GetAuctionByProductIdAsync(productId);
            } 
            catch(AuctionNotFoundException)
            {
                return RedirectToPage("./AuctionNotAvailable");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            this.Completed = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var bid = await this._auctionService.AddBidAsync(this.Auction.ProductId, this.Username, this.Price);
                    this.Completed = true;
                }
                catch (AuctionNotFoundException)
                {
                    ModelState.AddModelError("", "Sorry, that auction is already closed for bidding.");
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // save username to session for later pre-filling 
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append("Username", this.Username, option);

            return Page();
        }
    }
}